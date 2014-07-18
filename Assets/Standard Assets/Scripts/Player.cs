using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public RiddleScript ridScript;
	public DeathCounter deathCntScript;
	public GameObject spawn;
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUIText riddleText;
	public GUISkin guiSkin;
	public AudioSource thudAudioSource;
	public AudioClip thudAudioClip;
	public GameObject projectile;
	public AudioClip deathAudioClip;
	float gravity = 5.0f;
	CharacterController controller;
	Vector3 moveDirection  = Vector3.zero;


	// Moving platform support vars
	private Transform activePlatform;
	private Vector3 activeLocalPlatformPoint;
	private Vector3 activeGlobalPlatformPoint;
	private Vector3 lastPlatformVelocity;

	// Rotating platform support vars
	private Quaternion activeLocalPlatformRotation;
	private Quaternion activeGlobalPlatformRotation;
	
	// Use this for initialization
	void Start () {
		// Find and assign all relevant variables
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();
		deathCntScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
		spawn = GameObject.Find("Spawn");
		blackPauseTexture = GameObject.Find("blackPauseTexture").GetComponent<GUITexture>();
		riddleText = GameObject.Find("RiddleText").GetComponent<GUIText>();
		// On start, set pause texture invisible
		blackPauseTexture.color = new Color(0, 0, 0, 0);
		
		thudAudioSource = (AudioSource)gameObject.AddComponent("AudioSource");
		thudAudioClip = (AudioClip)Resources.Load("thud", typeof(AudioClip));
		deathAudioClip = (AudioClip)Resources.Load("death", typeof(AudioClip));

		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		// Moving platform support
		if (activePlatform != null){
			Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint(activeLocalPlatformPoint);
			Vector3 moveDistance = (newGlobalPlatformPoint - activeLocalPlatformPoint);
			
			if(moveDistance != Vector3.zero){
				controller.Move(moveDistance);
			}
			lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
			
			// Moving platform rotation support
			Quaternion newGlobalPlatformRotation = activePlatform.rotation * activeLocalPlatformRotation;
			Quaternion rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);
			// Prevent rotation of the local up vector
			rotationDiff = Quaternion.FromToRotation(rotationDiff * transform.up, transform.up) * rotationDiff;
			
			transform.rotation = rotationDiff * transform.rotation;
		}else{
			lastPlatformVelocity = Vector3.zero;
		}
		
		activePlatform = null;



		// respawn if dead
		if (Dead())
			Respawn();
		
		// check for pause
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = true;
			Time.timeScale = 0;
		}

		if(!controller.isGrounded && !Input.GetKey(ridScript.currentRiddle.inputs)){
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
		
		// execute passive player action
		ridScript.currentRiddle.action.Passive();
		
		// if proper input is pressed, execute player action
		if (Input.GetKey(ridScript.currentRiddle.inputs)) {
			ridScript.currentRiddle.action.Action();
		// If any key is pressed and it is the wrong input (besides ESC and mouse clicks), 
		// kill player and play thud sound effect
		} else if (Input.anyKeyDown && !(Input.GetKeyDown(ridScript.currentRiddle.inputs)) && 
				   !(Input.GetKeyDown(KeyCode.Escape)) && !(Input.GetMouseButton(0)) && !(Input.GetMouseButton(1))){
			thudAudioSource.PlayOneShot(thudAudioClip);
			Respawn();
		}
		
		// If the proper key is pressed once, play the audio
		// clip for this riddle once.
		if (Input.GetKeyDown(ridScript.currentRiddle.inputs)){
			ridScript.audioSource.PlayOneShot(ridScript.currentRiddle.audioClip);
		}

		// Moving platform support
		if (activePlatform != null){
			activeGlobalPlatformPoint = transform.position;
			activeLocalPlatformPoint = activePlatform.InverseTransformPoint(transform.position);

			// Moving platform rotation support
			activeGlobalPlatformRotation = transform.rotation;
			activeLocalPlatformRotation = Quaternion.Inverse(activePlatform.rotation) * transform.rotation;
		}

		// Move the character controller by zero. This is a "cheat" used to trigger
		// collision detections with OnControllerColliderHit, which normally doesn't
		// detect collisions when the player isn't moving.
		controller.SimpleMove(Vector3.zero);
	}
	
	// are we dead yet?
	bool Dead() {
		// If the player has fallen off the platforms
		if (gameObject.transform.position.y < -8){
			return true;
		}
		return false;
	}
	
	// respawn the player
	void Respawn() {
		thudAudioSource.PlayOneShot(deathAudioClip);
		deathCntScript.deathCount++;
		gameObject.transform.position = spawn.transform.position;

		GameObject[] platforms = GameObject.FindGameObjectsWithTag("DropPlatform");
		foreach (GameObject platform in platforms) {
			DropPlatform dp = platform.GetComponent<DropPlatform>();
			if (dp)
				dp.Reset();
			/*else
				Debug.Log("no drop platform --what?");*/
		}
	}
	
	// show menu when paused
	void OnGUI() {
		GUI.skin = guiSkin;
		//GUI.backgroundColor = Color.magenta; 

		GUIStyle quitStyle = new GUIStyle("button");
		quitStyle.fontSize = 40;
		
		if (paused) {
			// If paused, set black pause GUI texture & riddle GUI text
			// visisble, set Riddle Script's paused boolean true.
			blackPauseTexture.color = new Color(0, 0, 0, 1);
			riddleText.color = new Color(255, 255, 255, 1);
			ridScript.paused = true;


			// Resume Button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Resume")){
				// If unpaused, set black pause GUI texture & riddle GUI text
				// invisisble, set Riddle Script's paused boolean false.
				blackPauseTexture.color = new Color(0, 0, 0, 0);
				riddleText.color = new Color(255, 255, 255, 0);
				ridScript.paused = false;
				paused = false;
				Time.timeScale = 1;
			}

			// Quit button
			if (GUI.Button(new Rect(Screen.width/2 - Screen.height/4, Screen.height/2 + Screen.height/4, 200, 100), "Quit", quitStyle)){
				Application.LoadLevel("MainMenu");
				blackPauseTexture.color = new Color(0, 0, 0, 0);
				riddleText.color = new Color(255, 255, 255, 0);

				ridScript.paused = false; // Set paused to false
				ridScript.sceneIndex = 0; // Reset the scene index on quit
				// Destory the hint gameObjects on quit (to avoid duplicate gameObjects)
				Destroy(ridScript.smallHintText);
				Destroy(ridScript.bigHintText);
				Destroy(ridScript.backgroundMusic);

				deathCntScript.deathCount = 0; // Reset death counter on quit

				// Unpause on quit
				paused = false;
				Time.timeScale = 1;
			}
		}
    }

	// Handle all player collisions
	void OnControllerColliderHit(ControllerColliderHit hit) {
		// If the player collides with an enemy, respawn
		if (hit.collider.tag == "Enemy"){
			Respawn();
		}else if(hit.collider.tag == "FallingObstacle"){
			Debug.Log("Falling Obs Hit");
			Respawn();
		}
		// If the player touches a DropPlatform, attach the corresponding script
		else if (hit.collider.tag == "DropPlatform" && !hit.gameObject.GetComponent<DropPlatform>()) {
			hit.gameObject.AddComponent<DropPlatform>();
		// If the player touches a DissolvingPlatform, attach the corresponding script
		}else if (hit.collider.tag == "DissolvingPlatform" && !hit.gameObject.GetComponent<DissolvePlatform>()){
			hit.gameObject.AddComponent<DissolvePlatform>();
		}else if(hit.collider.tag == "YOnRailsPlatformY" && hit.moveDirection.y < -0.9f && hit.normal.y > 0.5f){
			activePlatform = hit.collider.transform;
			Debug.Log(hit.moveDirection.y);
			Debug.Log(hit.normal.y);
			Debug.Log(transform.position);
		}
		/*}else if (hit.collider.tag == "XOnRailsPlatformX" && !hit.gameObject.GetComponent<XOnRailsPlatformX>()){
			hit.gameObject.AddComponent<XOnRailsPlatformX>();
		// If the player touches a YOnRailsPlatformY,
		}else if (hit.collider.tag == "YOnRailsPlatformY" && !hit.gameObject.GetComponent<XOnRailsPlatformX>()){
			// And set the player character to be the platform's child temporarily
			//transform.parent = hit.transform;
		}*/
	}
}