using UnityEngine;
using System.Collections;

public class YOnRailsPlatformY : MonoBehaviour {
	float speed = 2.0f;
	int direction = 1;
	public int waitTime = 2;
	public int repeatTime = 2;
	Vector3 movement;

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
		}
	}
}
