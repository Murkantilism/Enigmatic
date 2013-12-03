using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public RiddleScript ridScript;
	public DeathCounter deathScript;
	public GameObject spawn;
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUIText riddleText;
	public GUISkin guiSkin;
	public AudioSource thudAudioSource;
	public AudioClip thudAudioClip;
	public GameObject projectile;
	public AudioClip deathAudioClip;
	
	// Use this for initialization
	void Start () {
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();
		deathScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
		spawn = GameObject.Find("Spawn");
		blackPauseTexture = GameObject.Find("blackPauseTexture").GetComponent<GUITexture>();
		riddleText = GameObject.Find("RiddleText").GetComponent<GUIText>();
		// On start, set pause texture invisible
		blackPauseTexture.color = new Color(0, 0, 0, 0);
		
		thudAudioSource = (AudioSource)gameObject.AddComponent("AudioSource");
		thudAudioClip = (AudioClip)Resources.Load("thud", typeof(AudioClip));
		deathAudioClip = (AudioClip)Resources.Load("death", typeof(AudioClip));
	}
	
	// Update is called once per frame
	void Update () {
		// respawn if dead
		if (Dead())
			Respawn();
		
		// check for pause
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = true;
			Time.timeScale = 0;
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
		deathScript.deathCount++;
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
		
		if (paused) {
			// If paused, set black pause GUI texture & riddle GUI text
			// visisble, set Riddle Script's paused boolean true.
			blackPauseTexture.color = new Color(0, 0, 0, 1);
			riddleText.color = new Color(255, 255, 255, 1);
			ridScript.paused = true;
			
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Resume")){
				// If unpaused, set black pause GUI texture & riddle GUI text
				// invisisble, set Riddle Script's paused boolean false.
				blackPauseTexture.color = new Color(0, 0, 0, 0);
				riddleText.color = new Color(255, 255, 255, 0);
				ridScript.paused = false;
				paused = false;
				Time.timeScale = 1;
			}
		}
    }

	// handle collisions with enemies
	void OnControllerColliderHit(ControllerColliderHit hit) {
		//Debug.Log("player collided with "+hit.collider.gameObject.name);
		if (hit.collider.tag == "Enemy") {
			Respawn();
		}
		// drop platform if necessary
		else if (hit.collider.tag == "DropPlatform" && !hit.gameObject.GetComponent<DropPlatform>()) {
			hit.gameObject.AddComponent<DropPlatform>();
		}
	}
}