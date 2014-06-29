﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiddleScript : MonoBehaviour {
	// Riddle timer (seconds)
	public int riddleTimer = 10;
	// Riddle timer for first scene (longer b/c instructions)
	public int firstRiddleTimer = 20;	
	// Small hint timer (seconds)
	public int smallHintTimer = 15;
	// Big hint timer (seconds)
	public int bigHintTimer = 25;
	// The riddle GUIText
	public GUIText riddleText;
	// The alpha value for the riddle text
	float riddleAlphaValue;
	// The alpha values for the hints
	float smallHintAlpha;
	float bigHintAlpha;
	
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
	
	// The GUIText objects for the hints
	public GUIText smallHintText;
	public GUIText bigHintText;
	
	// Has a level or riddle scene been completed?
	public bool levelCompleteP;
	public bool riddleCompleteP;

	// The sceneIdentifier go
	GameObject sceneIdentifier;
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
		
		// Set the pause texture invisible at beginning
		blackPauseTexture.color = new Color(0, 0, 0, 0);
		
		// Set the hints invisisble at beginning
		smallHintText.color = new Color(255, 255, 255, 0);
		bigHintText.color = new Color(255, 255, 255, 0);

		// Find the sceneIdentifier GameObject
		sceneIdentifier = GameObject.Find("sceneIdentifier");

		// Get the scene index
		sceneIndex = sceneIdentifier.GetComponent<SceneIdentifier>().sceneIndex;
		//int sceneIndex = Application.loadedLevel;
		
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
		HintSystem();

		sceneIdentifier = GameObject.Find("sceneIdentifier");
		// If the scene identifier object doesn't exist, find it!
		if(sceneIdentifier == null){
			sceneIdentifier = GameObject.Find("sceneIdentifier");
		}
	}
	
	// Fades in text over 5 seconds, sets riddleCompleteP to 
	// true after riddle timer expires
	void FadeInText(){
		// If the scene index is even, it is a riddle
		if (sceneIndex % 2 == 0 && paused == false){

			if(sceneIdentifier == null){
				sceneIdentifier = GameObject.Find("sceneIdentifier");
			}

			// Confirm this scene is in fact a riddle by checking scene ID
			if(sceneIdentifier.GetComponent<SceneIdentifier>().sceneID == "Riddle"){

				// If this is the first riddle, wait 10 seconds while player reads
				// instructions, then invoke fade text.
				if (sceneIndex == 2 && paused == false){
					// Set the big sphinx visible
					bigSphinxSprite.color = new Color(255, 255, 255, 1);
					Invoke("FirstSceneFadeInText", 10);
					
				// Otherwise, fade in riddle text normally
				}else{
					// Dividing by 5 makes fade lasts 5 secs
					riddleAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
					
					riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
					
					if (Time.timeSinceLevelLoad > riddleTimer){
						riddleCompleteP = true;
					}
					
					// Set the big sphinx visible
					bigSphinxSprite.color = new Color(255, 255, 255, 1);
				}
			}
		}

		// If the scene index is odd, it is a level
		if (sceneIndex % 2 == 1 && paused == false){

			if(sceneIdentifier == null){
				sceneIdentifier = GameObject.Find("sceneIdentifier");
			}

			// Confirm this scene is in fact a level by checking scene ID
			if(sceneIdentifier.GetComponent<SceneIdentifier>().sceneID == "Level"){

				// Set riddle text invisible
				riddleAlphaValue = 0;
				riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
				
				// Set the big sphinx invisible
				bigSphinxSprite.color = new Color(255, 255, 255, 0);
			}
		}
	}
	
	// Fade in text for the first scene (works exactly like fade in for any
	// other scene, but is a seperate function invoked after 10 seconds).
	void FirstSceneFadeInText(){
		// Dividing by 5 makes fade lasts 5 secs
		riddleAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
		
		riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
		
		if (Time.timeSinceLevelLoad > firstRiddleTimer){
			riddleCompleteP = true;
		}
	}
	
	// Checks if riddle is skipped (ESC input)
	void isRiddleCompleted(){
		// If the scene index is even, it is a riddle - ESC skips riddle
		if (sceneIndex % 2 == 0 && Input.GetKeyDown(KeyCode.Escape)){
			riddleCompleteP = true;
		}
	}
	
	public void completeLevel() {
		levelCompleteP = true;
	}
	
	// Master fucntion, handles scene loading logic
	void RiddleMaster(){
		// When the last level is reached, destory all the things!
		if (sceneIndex == 25)
			Destroy(this.gameObject);

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
		DontDestroyOnLoad(smallHintText);
		DontDestroyOnLoad(bigHintText);
		//DontDestroyOnLoad(sceneIdentifier);
		Debug.Log("Scene Index: " + sceneIndex);
		Debug.Log(Application.loadedLevelName);

		// Load the next scene - If it is the first riddle scene, increment the
		// sceneIndex by 3 instead, to  compensate for the MainMenu and 
		// LevelSelect throwing of the indexing.
		if(sceneIndex == 0){
			Application.LoadLevel(sceneIndex += 3);
		}else{
			Application.LoadLevel(sceneIndex += 1);
		}
	}
	
	// Create the list of riddles
	void RiddleList(){
		// Format: riddles.Add(new Riddle(string RiddleText, KeyCode Inputs, new ActionFunction(), int ExpectedSceneIndex, audioClip, string smallHintText, string bigHintText)

		// Riddle 0: Elephant - Move actively
		riddles.Add(new Riddle("What is the only mammal that cannot jump?", KeyCode.E, new MoveAction(), 0, audioClip, "It has big ears", "It's the largest living land mammal too"));

		// Riddle 1: Map - Jump actively, Move passively
		riddles.Add(new Riddle("What has rivers with no water, \nforests but no trees and \ncities with no buildings?", KeyCode.M, new JumpAction(), 4, audioClip, "It's partner is a compass", "Explorers use these"));

		// Riddle 2: Owl - Jump actively, Move passively
		riddles.Add(new Riddle("What asks but never answers?", KeyCode.O, new JumpAction(), 6, audioClip, "Who? Who?", "They're a real hoot!"));

		// Riddle 3: Shoe - Jump actively, Move passively
		riddles.Add(new Riddle("What has a tongue, cannot walk, \nbut gets worn around a lot?", KeyCode.S, new JumpAction(), 8, audioClip,"You wear two every day", "You wear them on your feet"));

		// Riddle 4: Candle - Shoot actively, Move passively
		riddles.Add(new Riddle("What gets shorter as it gets older?", KeyCode.C, new ShootAction(), 10, audioClip,"It lights the darkness","Made of wax"));

		// Riddle 5: Towel - Shoot actively, Move passively
		riddles.Add(new Riddle("What gets wetter the more you dry?", KeyCode.T, new ShootAction(), 12, audioClip,"Don't forget to bring one!","You dry off with it"));

		// Riddle 6: River - Shoot actively, Move passively
		riddles.Add(new Riddle("What has a mouth but cannot talk \nand runs but never walks?", KeyCode.R, new ShootAction(), 14, audioClip,"Water runs through it","The Nile is a famous example"));
		
		// Riddle 7: Man - Jump actively, Move and Shoot passively
		riddles.Add(new Riddle("What goes on 4 legs in the morning, \n3 legs in the afternoon, \nand 2 legs at night?", KeyCode.M, new JumpShootAction(), 16, audioClip,"The worlds dominant species","A boys grows up into a..."));

		// Riddle 8: Eggs - Jump actively, Move passively
		riddles.Add(new Riddle("A casket with no hinges nor lid, \nand yet inside golden treasure is hid.", KeyCode.E, new JumpAction(), 18, audioClip,"You crack them","Eat them for breakfast"));

		// Riddle 9: Fart - Jump actively, Move passively
		riddles.Add(new Riddle("Fatherless, motherless and born without skin, \nI speak when I come into the world, \nbut never speak again. What am I?", KeyCode.F, new JumpAction(), 20, audioClip,"Toot!","Comes out your butt"));
		
		// Riddle 10: Parachute - Jump actively, Move and Shoot passively
		riddles.Add(new Riddle("2 men both have packs on, 1 is dead. \nThe man who is alive has his pack open, \nthe man who is dead has his pack closed. \nWhat is in the pack?", KeyCode.P, new JumpShootAction(), 22, audioClip, "Delpoyed in an emergency", "For falling from the sky"));
		
		// Riddle 11: Red paint - Jump actively, Move passively
		riddles.Add(new Riddle("What's red and smells like blue paint?", KeyCode.R, new JumpAction(), 24, audioClip, "It's just like blue paint, only...", "It's Red Paint you dunce, \nhow'd you get this far?"));
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
				
				// Set the hints to this riddle's hints
				bigHintText.text = riddle.bigHint;
				smallHintText.text = riddle.smallHint;
				
				// Set the audio clip to the expected audio clip
				currentRiddle.audioClip = (AudioClip)Resources.Load(sceneIndex.ToString(), typeof(AudioClip));
			}
		}
	}
	
	void HintSystem(){
		// If the scene index if even, it is a riddle
		if (sceneIndex % 2 == 0 && paused == false){
			// Set hints to invisible
			smallHintText.color = new Color(255, 255, 255, 0);
			bigHintText.color = new Color(255, 255, 255, 0);
		}
		
		// If the scene index is odd, it is a level
		if (sceneIndex % 2 == 1 && paused == false){
			// If the level is played for longer than the small hint timer, and not
			// longer than the big hint timer, reveal the hint for this riddle.
			if (Time.timeSinceLevelLoad > smallHintTimer && !(Time.timeSinceLevelLoad > bigHintTimer)){
				// Dividing by 7 makes fade lasts 7 secs
				smallHintAlpha += Mathf.Clamp01(Time.deltaTime / 7);
				smallHintText.color = new Color(255, 255, 255, smallHintAlpha);
			// Otherwise set hint invisible
			}else{
				smallHintText.color = new Color(255, 255, 255, 0);
			}
			
			// If the level is played for longer than the big hint timer,
			// reveal the big hint for this riddle
			if (Time.timeSinceLevelLoad > bigHintTimer){
				// Dividing by 7 makes fade lasts 7 secs
				bigHintAlpha += Mathf.Clamp01(Time.deltaTime / 7);
				bigHintText.color = new Color(255, 255, 255, bigHintAlpha);
			}else{
				bigHintText.color = new Color(255, 255, 255, 0);
			}
		}
	}
	
	// Correctly positions Big Sphinx regardless of resolution changes
	void BigSphinxPostion(){
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		bigSphinxSprite.transform.position = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / -5f, 30));
	}
}