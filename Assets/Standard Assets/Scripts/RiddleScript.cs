using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiddleScript : MonoBehaviour {
	// Riddle timer (seconds)
	public int riddleTimer = 3;
	// The riddle GUIText
	public GUIText riddleText;
	// The alpha value for the riddle text
	float riddleAlphaValue;
	// Big sphinx sprite
	public tk2dSprite bigSphinxSprite;
	// The main camera object
	private Camera myCamera;
	
	// Black texture used for pausing and re-displaying riddle
	public GUITexture blackPauseTexture;
	
	// Background music (refernced for DontDestoryOnLoad)
	public GameObject backgroundMusic;
	// Death counter (refernced for DontDestoryOnLoad)
	public GameObject deathCounter;
	
	// Has a level or riddle scene been completed?
	public bool levelCompleteP;
	public bool riddleCompleteP;
	// The current scene index
	public int sceneIndex;
	public Riddle currentRiddle;
	// Is this scene paused?
	public bool paused = false;
	
	// A list of riddles
	public List<Riddle> riddles = new List<Riddle>();
	
	// The audio clip for this riddle
	public AudioClip audioClip;
	public AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
		levelCompleteP = false;
		riddleCompleteP = false;
		
		// Set text invisisble & white at beginning
		riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
		
		// Set the big sphinx visible at beginning
		bigSphinxSprite.color = new Color(255, 255, 255, 1);
		
		// Get the scene index last loaded
		int sceneIndex = Application.loadedLevel;
		
		// Build the list of riddles on start
		RiddleList ();
		
		audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
	}
	
	// Update is called once per frame
	void Update () {
		isRiddleCompleted(); // Check if this level has been completed
		FadeInText();       // Fade in riddle text if current scene is riddle
		RiddleMaster();     // Handles scene loading logic
		SetRiddleText();    // Sets the riddle text based on scene index
		BigSphinxPostion(); // Sets the position of the Big Sphinx to the lower right
	}
	
	// Fades in text over 5 seconds, sets riddleCompleteP to 
	// true after riddle timer expires
	void FadeInText(){
		// If the scene index is even, it is a riddle
		if (sceneIndex % 2 == 0 && paused == false){
			// Dividing by 5 makes fade lasts 5 secs
			riddleAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
			
			riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
			
			if (Time.timeSinceLevelLoad > riddleTimer){
				riddleCompleteP = true;
			}
			
			// Set the big sphinx visible
			bigSphinxSprite.color = new Color(255, 255, 255, 1);
		}
		// If the scene index is odd, it is a level
		if (sceneIndex % 2 == 1 && paused == false){
			// Set riddle text invisible
			riddleAlphaValue = 0;
			riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
			
			// Set the big sphinx invisible
			bigSphinxSprite.color = new Color(255, 255, 255, 0);
		}
	}
	
	// Checks if level is completed (Spacebar input)
	void isRiddleCompleted(){
		// **PLACEHOLDER CODE** - Spacebar will complete level
		// ToDo: Replace this with an actual level completion
		// ToDo: check, likely OnCollisionEnter() method.
		
		// If the scene index is odd, it is a level
		if (sceneIndex % 2 == 0 && Input.GetKeyDown(KeyCode.Escape)){
			riddleCompleteP = true;
			//levelCompleteP = true;
		}
	}
	
	public void completeLevel() {
		levelCompleteP = true;
	}
	
	// Master fucntion, handles scene loading logic
	void RiddleMaster(){
		if (levelCompleteP){
			//Load next riddle
			LoadNext();
			// Reset the boolean value
			levelCompleteP = false;
		}else if(riddleCompleteP){
			//Load next level
			LoadNext();
			// Reset the boolean value
			riddleCompleteP = false;
		}
	}
	
	// Loads the next scene
	void LoadNext(){
		// Keep the impotant game objects to allow cross-scene scripting
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(bigSphinxSprite);
		DontDestroyOnLoad(backgroundMusic);
		DontDestroyOnLoad(deathCounter);
		DontDestroyOnLoad(blackPauseTexture);
		// Load the next scene
		Application.LoadLevel(sceneIndex += 1);
	}
	
	// Create the list of riddles
	void RiddleList(){
		// Format: Riddle(string_RiddleText, KeyCode Inputs, int ExpectedSceneIndex)
		
		// Elephant - Move
		riddles.Add(new Riddle("What is the only mammal that cannot jump?", KeyCode.E, new MoveAction(), 0, audioClip));
		// Map - Jump
		riddles.Add(new Riddle("What has rivers with no water, \nforests but no trees and \ncities with no buildings?", KeyCode.M, new JumpAction(), 2, audioClip));
		// Owl - Shoot
		riddles.Add(new Riddle("What asks but never answers?", KeyCode.O, new ShootAction(), 4, audioClip));
	}
	
	// Set the GUIText to the correct riddle based on the scene index
	void SetRiddleText(){
		// For each riddle in the list of riddles
		foreach(Riddle riddle in riddles) {
			// If expected index of this riddle == to the scene's index
			if (riddle.expectedSceneIndex == sceneIndex) {
				// Set the GUIText (riddleText) to this riddle's text
				riddleText.text = riddle.riddleText;
				currentRiddle = riddle;
				
				// Set the audio clip to the expected audio clip
				currentRiddle.audioClip = (AudioClip)Resources.Load(sceneIndex.ToString(), typeof(AudioClip));
			}
		}
	}
	
	// Correctly positions Big Sphinx regardless of resolution changes
	void BigSphinxPostion(){
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		bigSphinxSprite.transform.position = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / -5f, 30));
	}
}