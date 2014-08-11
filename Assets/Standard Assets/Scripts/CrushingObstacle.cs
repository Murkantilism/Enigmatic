using UnityEngine;
using System.Collections;

// CrushingObstacle.cs - Last Updated 07/14/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class CrushingObstacle : MonoBehaviour {
	float speed = 2.0f;
	int direction = 1;
	public int waitTime = 2;
	public int repeatTime = 2;
	Vector3 movement;

	GameObject myCamera;
	
	GameObject cameraChild;

	// Get the player gameObject and script
	GameObject player_go;
	Player playerScript;
	
	// Use this for initialization
	void Start () {
		// Find the player gameObject
		player_go = GameObject.Find("Player");
		// Assign the player script
		playerScript = player_go.GetComponent<Player>();

		InvokeRepeating("SwitchDirection", waitTime, repeatTime);

		// Assign the main camera
		myCamera = GameObject.Find("Main Camera");
		// Instantiate an empty gameobject
		cameraChild = new GameObject();
		// Parent the empty gameobject to the camera
		cameraChild.transform.parent = myCamera.transform;
		// Set the position of the child close to parent
		cameraChild.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, myCamera.transform.position.z - 5);
		// Attach an audio source to the child
		cameraChild.AddComponent<AudioSource>();
		// Load the enemy death sound effect
		cameraChild.audio.clip = (AudioClip)Resources.Load("AmbientFX/Falling Stone", typeof(AudioClip));
	}
	
	// Negate the direction every 2 seconds
	void SwitchDirection(){
		direction = direction * -1;
	}
	
	// Update is called once per frame
	void Update () {
		// Calculate the movement vector to include direction, speed, and time
		movement = Vector3.up * direction * speed * Time.deltaTime;
		// Move the platform based on the calculated movement vector
		transform.Translate(movement);

		// If the player reference is ever lost, reassign it
		if(player_go == null){
			// Find the player gameObject
			player_go = GameObject.Find("Player");
			// Assign the player script
			playerScript = player_go.GetComponent<Player>();
		}
	}
	
	// Detect if this crushing platform hits the Player
	public void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			Debug.Log("Player crushed!");
			// Respawn the player
			playerScript.Respawn();
		}else if(col.tag == "CrushPlatformTrigger"){
			// If the sound effect isn't already playing
			if(cameraChild.audio.isPlaying == false){
				// Play the sound effect
				cameraChild.audio.Play();
			}
		}
	}
}
