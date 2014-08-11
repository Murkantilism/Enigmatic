using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// RiddleScript.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class RiddleScript : MonoBehaviour {
	// Riddle timer (seconds)
	public int riddleTimer = 10;
	// Riddle timer for first scene (longer b/c instructions)
	public int firstRiddleTimer = 20;	
	// Small hint timer (seconds)
	public int smallHintTimer = 30;
	// Big hint timer (seconds)
	public int bigHintTimer = 60;
	// The riddle GUIText
	public GUIText riddleText;
	// The alpha value for the riddle text
	float riddleAlphaValue;
	// The alpha values for the hints
	float smallHintAlpha;
	float bigHintAlpha;
	// Has the player requested a hint?
	public bool firstHintRequestedP = false;
	public bool secondHintRequestedP = false;

	// Have the hints been shown?
	public bool firstHintShown = false;
	public bool secondHintShown = false;

	// The GUIText object for "Press spacebar to continue!"
	GUIText spaceBarText;

	// Big sphinx sprite
	public tk2dSprite bigSphinxSprite;
	public tk2dSpriteAnimator bigSphinxSpriteAnim;
	public GameObject bigSphinxSprite_go;
	// The main camera object
	private Camera myCamera;
	
	// Black texture used for pausing and re-displaying riddle
	public GUITexture blackPauseTexture;
	public GameObject blackPauseTexture_go;
	
	// Background music (refernced for DontDestoryOnLoad)
	public GameObject backgroundMusic;
	// Death counter (referenced for DontDestoryOnLoad)
	public GameObject deathCounter;
	// Timer (referenced for DontDestoryOnLoad)
	public GameObject timer;
	
	// The GUIText objects for the hints
	public GUIText smallHintText;
	public GameObject smallHintText_go;
	public GUIText bigHintText;
	public GameObject bigHintText_go;

	// Has a level or riddle scene been completed?
	public bool levelCompleteP;
	public bool riddleCompleteP;

	// The sceneIdentifier go
	GameObject sceneIdentifier;
	// The current scene index
	public int sceneIndex;
	// The current scene ID
	public string sceneID;

	public Riddle currentRiddle;
	// Is this scene paused?
	public bool paused = false;
	
	// A list of riddles
	public List<Riddle> riddles = new List<Riddle>();
	
	// The audio clip for this riddle
	public AudioClip audioClip;
	public AudioSource audioSource;

	// Used to find any duplicates of this gameObject
	public GameObject RiddleTextDups;

	GameObject mainCamera;
	// A child object attached to the camera used to play sound fx
	GameObject cameraChild;
	// Have the sound fx been assigned?
	bool fxAssigned = false;

	// Has the sound fx already been played?
	bool fxPlayed = false;

	// A child object used to play hint sound fx
	GameObject hintFX;

	// On Awake() check if there are duplicate RiddleText objects, and if so, destroy them
	public void Awake(){
		DontDestroyOnLoad(this);

		if(FindObjectsOfType(GetType()).Length > 1){
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		levelCompleteP = false;
		riddleCompleteP = false;

		// Find and assign all the neccessary gameObjects
		smallHintText_go = GameObject.Find ("Hint_small");
		smallHintText = smallHintText_go.GetComponent<GUIText>();
		
		bigHintText_go = GameObject.Find ("Hint_BIG");
		bigHintText = bigHintText_go.GetComponent<GUIText>();

		bigSphinxSprite_go = GameObject.Find("BigSphinxSprite");
		bigSphinxSprite = bigSphinxSprite_go.GetComponent<tk2dSprite>();
		bigSphinxSprite.SetSprite("BigSphinx_pixel_01");

		spaceBarText = GameObject.Find("PressSpaceBarText").guiText;

		blackPauseTexture_go = GameObject.Find("blackPauseTexture");
		blackPauseTexture = blackPauseTexture_go.GetComponent<GUITexture>();

		backgroundMusic = GameObject.Find("BackgroundMusic");
		deathCounter = GameObject.Find ("DeathCounter");
		timer = GameObject.Find("Timer");
		
		// Set text invisisble & white at beginning
		riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
		
		// Set the big sphinx visible at beginning
		bigSphinxSprite.color = new Color(255, 255, 255, 1);

		// Set the pause texture invisible at beginning
		blackPauseTexture.color = new Color(0, 0, 0, 0);
		
		// Set the hints invisisble at beginning
		smallHintText.color = new Color(255, 255, 255, 0);
		bigHintText.color = new Color(255, 255, 255, 0);

		// Set the spacebar text invisible at beginning
		spaceBarText.color = new Color(255, 255, 255, 0);

		// Find the sceneIdentifier GameObject
		sceneIdentifier = GameObject.Find("sceneIdentifier");


		// Get the scene index
		sceneIndex = sceneIdentifier.GetComponent<SceneIdentifier>().sceneIndex;

		// Assign the scene ID
		sceneID = sceneIdentifier.GetComponent<SceneIdentifier>().sceneID;

		// Build the list of riddles on start
		RiddleList ();
		
		audioSource = (AudioSource)gameObject.AddComponent("AudioSource");
	}
	
	// Update is called once per frame
	void Update () {
		// If we are in the first riddle scene, assign all of the potentially missing important gameObjects.
		// This is to prevent any UnassignedReferenceExceptions if the player quits and plays again.
		if(sceneIndex == 0){
			smallHintText_go = GameObject.Find ("Hint_small");
			smallHintText = smallHintText_go.GetComponent<GUIText>();
			
			bigHintText_go = GameObject.Find ("Hint_BIG");
			bigHintText = bigHintText_go.GetComponent<GUIText>();

			spaceBarText = GameObject.Find("PressSpaceBarText").guiText;

			// Refresh the background music object
			backgroundMusic = GameObject.Find("BackgroundMusic");

			sceneIdentifier = GameObject.Find("sceneIdentifier");

			timer = GameObject.Find("Timer");
		}

		isRiddleCompleted(); // Check if this level has been completed
		FadeInText();       // Fade in riddle text if current scene is riddle
		RiddleMaster();     // Handles scene loading logic
		SetRiddleText();    // Sets the riddle text based on scene index
		BigSphinxPostion(); // Sets the position of the Big Sphinx to the lower right
		HintSystem();       // Controls the hints
		LevelSoundFX();     // Assigns & plays level sound fx

		// If the scene identifier object doesn't exist, find it!
		if(sceneIdentifier == null){
			sceneIdentifier = GameObject.Find("sceneIdentifier");
		}

		// Refresh the sceneID
		sceneID = sceneIdentifier.GetComponent<SceneIdentifier>().sceneID;
	}

	// Load and assign the sound fx for each level
	void LevelSoundFX(){
		// If the scene is a level, and not paused, and the sound fx hasn't already been assigned, assign it
		if (sceneID == "Level" && paused == false && fxAssigned == false){
			// Assign the main camera
			mainCamera = GameObject.Find("Main Camera");
			// Instantiate an empty gameobject
			cameraChild = new GameObject();
			// Parent the empty gameobject to the riddle script (this script)
			cameraChild.transform.parent = transform;
			// Set the position of the child close to main camera
			cameraChild.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 5);
			
			// Attach an audio source to the child
			cameraChild.AddComponent<AudioSource>();
			// Load the sound effect
			cameraChild.audio.clip = (AudioClip)Resources.Load("AmbientFX/Finished Level", typeof(AudioClip));

			fxAssigned = true;
		}
	}
	
	// Fades in text over 5 seconds, sets riddleCompleteP to true after riddle timer expires
	void FadeInText(){
		// If the scene index is even, it is a riddle
		if (sceneIndex % 2 == 0 && paused == false){

			if(sceneIdentifier == null){
				sceneIdentifier = GameObject.Find("sceneIdentifier");
			}


			// Confirm this scene is in fact a riddle by checking scene ID
			if(sceneIdentifier.GetComponent<SceneIdentifier>().sceneID == "Riddle"){
				// Show sphinx talking animation
				bigSphinxSpriteAnim.renderer.enabled = true;

				// Dividing by 5 makes fade lasts 5 secs
				riddleAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
				
				riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
				if(!(sceneIndex > 41)){
					spaceBarText.color = new Color(255, 255, 255, riddleAlphaValue);
				}else{
					spaceBarText.color = new Color(255, 255, 255, 0);
				}
				
				// Set the big sphinx visible
				bigSphinxSprite.color = new Color(255, 255, 255, 1);
			}
		}

		// If the scene index is odd, it is a level
		if (sceneIndex % 2 == 1 && paused == false){

			if(sceneIdentifier == null){
				sceneIdentifier = GameObject.Find("sceneIdentifier");
			}

			// Confirm this scene is in fact a level by checking scene ID
			if(sceneIdentifier.GetComponent<SceneIdentifier>().sceneID == "Level"){
				// Hide big sphinx talking animation
				bigSphinxSpriteAnim.renderer.enabled = false;
				
				// Set riddle text invisible
				riddleAlphaValue = 0;
				riddleText.color = new Color(255, 255, 255, riddleAlphaValue);
				spaceBarText.color = new Color(255, 255, 255, riddleAlphaValue);
				
				// Set the big sphinx invisible
				bigSphinxSprite.color = new Color(255, 255, 255, 0);
			}
		}
	}
	
	// Checks if riddle is skipped (ESC input)
	void isRiddleCompleted(){
		// If the scene index is even, it is a riddle - spacebar skips riddle
		if (sceneIndex % 2 == 0 && Input.GetKeyDown(KeyCode.Space)){
			riddleCompleteP = true;
		}
	}

	// Used to play sound fx when the level is completed
	public void completeLevel() {
		cameraChild.audio.Play(); // Play the finished level sound effect
		levelCompleteP = true;
	}
	
	// Master fucntion, handles scene loading logic
	void RiddleMaster(){
		// When the Final Riddle is reached, hide the riddle text
		if(sceneIndex >= 42){
			riddleText.color = new Color(255, 255, 255, 0);
		}
		// When the last riddle is reached, destory all the things!
		if (sceneIndex == 46)
			Destroy(this.gameObject);

		// If the level has been completed and we aren't on final riddles
		if (levelCompleteP && !(sceneIndex > 41)){
			//Load next riddle
			LoadNext();
			// Reset the boolean value
			levelCompleteP = false;
		// If the riddle has been completed, we aren't on final riddles, and the player has hit spacebar
		}else if(riddleCompleteP && !(sceneIndex > 41) && Input.GetKeyUp(KeyCode.Space)){
			//Load next level
			LoadNext();
			// Reset the boolean value
			riddleCompleteP = false;
		}
		// If scene is a riddle, hide timer & death count. If scene is a level, show them.
		if(sceneID == "Riddle"){
			timer.guiText.enabled = false;
			// Only disable death counter if this isn't a final riddle
			if(!(sceneIndex >= 41)){
				deathCounter.guiText.enabled = false;
			}
		}else if(sceneID == "Level"){
			timer.guiText.enabled = true;
			if(!paused){
				deathCounter.guiText.enabled = true;
			}else{
				deathCounter.guiText.enabled = false;
			}
		}else{
			// Otherwise hide the timer
			timer.guiText.enabled = false;
		}
		// If paused, hide timer
		if(paused == true){
			timer.guiText.enabled = false;
		}
	}
	
	// Loads the next scene
	void LoadNext(){
		// Keep the impotant game objects to allow cross-scene scripting
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(bigSphinxSprite);
		DontDestroyOnLoad(bigSphinxSpriteAnim);
		DontDestroyOnLoad(backgroundMusic);
		DontDestroyOnLoad(deathCounter);
		DontDestroyOnLoad(timer);
		DontDestroyOnLoad(blackPauseTexture);
		DontDestroyOnLoad(smallHintText);
		DontDestroyOnLoad(bigHintText);
		DontDestroyOnLoad(spaceBarText.gameObject);
		//DontDestroyOnLoad(sceneIdentifier);
		Debug.Log("Scene Index: " + sceneIndex);
		Debug.Log(Application.loadedLevelName);

		// Load the next scene - If it is the first riddle scene, increment the
		// sceneIndex by 3 instead, to  compensate for the MainMenu and 
		// LevelSelect throwing off the indexing.
		if(sceneIndex == 0){
			Application.LoadLevel(sceneIndex += 3);
		}else{
			Application.LoadLevel(sceneIndex += 1);
		}

		// Reset hint variables
		firstHintRequestedP = false;
		secondHintRequestedP = false;
		firstHintShown = false;
		secondHintShown = false;
		smallHintAlpha = 0;
		bigHintAlpha = 0;

		// Reset sound fx variable
		fxPlayed = false;
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

		// Riddle 5: Onion - Shoot actively, Move passively
		riddles.Add(new Riddle("Go ahead, take off my skin. \nI won't shout or feel pain,\nbut you might cry.", KeyCode.O, new ShootAction(), 12, audioClip,"They posses a strong odor","They can make a meal delicious"));

		// Riddle 6: River - Jump actively, Move passively
		riddles.Add(new Riddle("What has a mouth but cannot talk \nand runs but never walks?", KeyCode.R, new JumpAction(), 14, audioClip,"Water runs through it","The Nile is a famous example"));
		
		// Riddle 7: Man - Jump actively, Move and Shoot passively
		riddles.Add(new Riddle("What goes on 4 legs in the morning, \n3 legs in the afternoon, \nand 2 legs at night?", KeyCode.M, new JumpShootAction(), 16, audioClip,"The worlds dominant species","A boys grows up into a..."));

		// Riddle 8: Venus - Move actively, Shoot passively
		riddles.Add(new Riddle("I'm yellow, cloudy and super hot. \nLook low in the sky, I'm easy to spot.", KeyCode.V, new MoveShootAction(), 18, audioClip,"From planet Earth, I'm not very far","People call me the “Evening Star”"));

		// Riddle 9: Fart - Jump actively, Move passively
		riddles.Add(new Riddle("Fatherless, motherless and born without skin, \nI speak when I come into the world, \nbut never speak again. What am I?", KeyCode.F, new JumpAction(), 20, audioClip,"Toot!","Comes out your butt"));
		
		// Riddle 10: Umbrella - Jump actively, Move and Shoot passively
		riddles.Add(new Riddle("What goes up when the rain comes down?", KeyCode.U, new ShootAction(), 22, audioClip, "A hit song by Rhianna", "Strong winds turn them inside out"));
		
		// Riddle 11: Dice - Jump actively, Move passively
		riddles.Add(new Riddle("What has block spots and a white face, \nis not fat nor thin, can help you win,\nbut tumbles all over the place?", KeyCode.D, new JumpAction(), 24, audioClip, "Gambler's tool", "Craps, Backgammon, Boggle, Risk"));

		// Riddle 12: Candle - Move actively
		riddles.Add(new Riddle("I was carried into a dark room and set on fire.\nI wept and soon after my head was cut off.\nWhat am I?", KeyCode.C, new MoveAction(), 26, audioClip, "It lights the darkness", "A previous answer for level 5"));

		// Riddle 13: Windows - Move actively
		riddles.Add(new Riddle("An ancient invention still used today, \nthat allows people to see through walls, \nand can be bowed or bayed.", KeyCode.W, new MoveAction(), 28, audioClip, "Often provides a nice view", "Clear, made of glass"));

		// Riddle 14: Tree - Jump actively, Move passively
		riddles.Add(new Riddle("In spring I am gay in handsome array;\nin summer more clothing I wear.\nWhen colder it grows I fling off my clothes,\nand in winter quite naked I appear.", KeyCode.T, new JumpAction(), 30, audioClip, "The subject of a Shel Silverstein classic", "A mighty oak, a weeping willow, and a handsome spruce"));

		// Riddle 15: Rug - Jump actively, Move passively
		riddles.Add(new Riddle("I am colored red, blue, and yellow and \nevery other hue of the rainbow. \nI am thick and thin, short and tall. \nI can eat over a hundred sheep in a row. \nWhat am I?", KeyCode.R, new JumpAction(), 32, audioClip, "Hand woven", "Middle Eastern & Persian"));

		// Riddle 16: Ice - Jump actively, Move passively
		riddles.Add(new Riddle("I am powerful enough to smash ships and \ncrush roofs, yet I still \nfear the Sun. What am I?", KeyCode.I, new JumpAction(), 34, audioClip, "It's cold", "Water at freezing temperatures"));

		// Riddle 17: Waterfall - Jump actively, Move passively
		riddles.Add (new Riddle("I can run like a river without moving at all.\nI have no lungs nor a throat, but I can still\nshout a mighty roaring call. What am I?", KeyCode.W, new JumpAction(), 36, audioClip, "Kayaking through these is rough", "Falling water"));

		// Riddle 18: Neptune - Jump actively, Move passively
		riddles.Add(new Riddle("I had a black spot that was a huge storm.\nNow it's all gone, but I'll still never be warm.", KeyCode.N, new JumpAction(), 38, audioClip, "Look past Saturn and Uranus for me", "Named for the Roman god of the Sea"));

		// Riddle 19: Needle - Jump actively, Move passively
		riddles.Add(new Riddle("I'm an iron horse with an eye but no head,\nwith a flaxen tail that gets shorter the more I run.", KeyCode.N, new JumpAction(), 40, audioClip, "Often used to stitch", "Its companion is a thread"));
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
				
				// Set the riddle's correct answer audio clip to the expected audio clip
				currentRiddle.audioClip = (AudioClip)Resources.Load("SoundFX/" + (sceneIndex - 2).ToString(), typeof(AudioClip));
			}
		}
	}

	// Assigns and displays the hints for each level
	void HintSystem(){
		// If the scene index if even, it is a riddle
		if (sceneIndex % 2 == 0 && paused == false){
			// Set hints to invisible
			smallHintText.color = new Color(255, 255, 255, 0);
			bigHintText.color = new Color(255, 255, 255, 0);
		}
		
		// If the scene index is odd, it is a level. Make sure we aren't paused or at a final riddle.
		if (sceneIndex % 2 == 1 && paused == false && !(sceneIndex > 41)){
			// If the user hits spacebar, and the first hint is available to be requested, request it!
			if(sceneID == "Level" && Input.GetKeyUp(KeyCode.Space) && Time.timeSinceLevelLoad > smallHintTimer && firstHintShown == false){
				firstHintRequestedP = true;

				if(fxPlayed == false){
					// Assign the main camera
					mainCamera = GameObject.Find("Main Camera");
					// Instantiate an empty gameobject
					hintFX = new GameObject();

					// Parent the empty gameobject to the riddle script (this script)
					hintFX.transform.parent = transform;

					// Set the position of the child close to main camera
					hintFX.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z - 5);

					// Attach an audio source to the child
					hintFX.AddComponent<AudioSource>();

					// Load the sound effect
					hintFX.audio.clip = (AudioClip)Resources.Load("AmbientFX/Hint", typeof(AudioClip));

					hintFX.audio.Play();
					fxPlayed = true;
				}

			// If the user hits spacebar, and the 1st hint has been shown, and 2nd hint is available to be requested, request it!
			}else if(sceneID == "Level" && Input.GetKeyUp(KeyCode.Space) && firstHintShown == true && Time.timeSinceLevelLoad > bigHintTimer){
				secondHintRequestedP = true;

				if(fxPlayed == false){
					hintFX.audio.Play();
					fxPlayed = true;
				}
			}

			// If the player has requested the first hint, show it
			if(firstHintRequestedP == true){
				// Dividing by 7 makes fade lasts 7 secs
				smallHintAlpha += Mathf.Clamp01(Time.deltaTime / 7);
				try{
					smallHintText.color = new Color(255, 255, 255, smallHintAlpha);
				}catch(MissingReferenceException e){
					Debug.Log(e.ToString());
					smallHintText_go = GameObject.Find ("Hint_small");
					smallHintText = smallHintText_go.GetComponent<GUIText>();
					Debug.Log("Missing Reference resolved, " + smallHintText_go + " successfully assigned");
				}
				// Once the hint is at full alpha, it has been shown
				if(smallHintText.color.a >= 1){
					firstHintShown = true;
					fxPlayed = false; // Reset boolean flag
				}
			}
			// If the second hint has been requested, hide 1st hint
			if (secondHintRequestedP == true){
				try{
					smallHintText.color = new Color(255, 255, 255, 0);
				}catch(MissingReferenceException e){
					Debug.Log(e.ToString());
					smallHintText_go = GameObject.Find ("Hint_small");
					smallHintText = smallHintText_go.GetComponent<GUIText>();
					Debug.Log("Missing Reference resolved, " + smallHintText_go + " successfully assigned");
				}
			}

			// If the 2nd hint has been requested, show it
			if(secondHintRequestedP == true){
				// Dividing by 7 makes fade lasts 7 secs
				bigHintAlpha += Mathf.Clamp01(Time.deltaTime / 7);
				try{
					bigHintText.color = new Color(255, 255, 255, bigHintAlpha);
				}catch(MissingReferenceException e){
					Debug.Log(e.ToString());
					bigHintText_go = GameObject.Find ("Hint_BIG");
					bigHintText = bigHintText_go.GetComponent<GUIText>();
					Debug.Log("Missing Reference resolved, " + smallHintText_go + " successfully assigned");
				}
				// Once the hint is at full alpha, it has been shown
				if(bigHintText.color.a >= 1){
					secondHintShown = true;
				}

				}else if(secondHintShown == false){
					try{
						bigHintText.color = new Color(255, 255, 255, 0);
					}catch(MissingReferenceException e){
						Debug.Log(e.ToString());
						bigHintText_go = GameObject.Find ("Hint_BIG");
						bigHintText = bigHintText_go.GetComponent<GUIText>();
						Debug.Log("Missing Reference resolved, " + bigHintText_go + " successfully assigned");
					}
				}
			}

		// If the scene is a level and it has been paused, hide the hints
		if(sceneIndex % 2 == 1 && paused == true){
			// Set hints to invisible
			smallHintText.color = new Color(255, 255, 255, 0);
			bigHintText.color = new Color(255, 255, 255, 0);
		}
	}
	
	// Correctly positions Big Sphinx regardless of resolution changes
	void BigSphinxPostion(){
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		bigSphinxSprite.transform.position = myCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / -5f, 30));
	}

	// Called when Player.cs sends a message on death
	void PlayerDeath(){
		// Forward the message to SubmitDeath in kongregateIntegration.js along with the scene index
		gameObject.SendMessage("SubmitDeath", sceneIndex);
	}

	// Called when FinalRiddle3.cs sends a message on game complete
	public void GameComplete(){
		// Forward the message to SubmitScore in kongregateIntegration.js along with the death count
		gameObject.SendMessage("SubmitScore", deathCounter.GetComponent<DeathCounter>().deathCount);
	}

	// Called by Player.cs when the player quits
	public void Quit(){
		// Hide the timer
		//timer.guiText.enabled = false;
		Destroy(timer);
		// Hide the big sphinx talking animation
		bigSphinxSpriteAnim.renderer.enabled = false;
		// Reset sound fx variable
		fxPlayed = false;
	}
}