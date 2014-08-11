using UnityEngine;
using System.Collections;

// SidewaysObstacles.cs - Last Updated 07/28/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class SidewaysObstacles : MonoBehaviour {
	Vector3 origin;
	// How long should we wait for the obstacle to trigger?
	public float waitTime = 3.0f;
	
	public bool obstacleMoving = false;

	public float direction = 1;

	// Get the player gameObject and script
	GameObject player_go;
	Player playerScript;

	// Spear collider
	BoxCollider spearCollider;
	
	// Use this for initialization
	void Start () {
		// Find the player gameObject
		player_go = GameObject.Find("Player");
		// Assign the player script
		playerScript = player_go.GetComponent<Player>();

		// Assign this spear's collider
		spearCollider = gameObject.GetComponent<BoxCollider>();

		// Start the obstacle coroutine
		StartCoroutine(ObstacleTrigger());
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(obstacleMoving == true){
			// Multiply by the direction float to dictate if the spear goes left or right
			transform.position -= new Vector3(0.3f * direction, 0, 0);
		}
		
		if(obstacleMoving == false){
			Reset();
			StartCoroutine(ObstacleTrigger());
		}

		// If the player reference is ever lost, reassign it
		if(player_go == null){
			// Find the player gameObject
			player_go = GameObject.Find("Player");
			// Assign the player script
			playerScript = player_go.GetComponent<Player>();
		}
	}
	
	public IEnumerator ObstacleTrigger(){
		// Wait to trigger the obstacle fall
		yield return new WaitForSeconds(waitTime);
		obstacleMoving = true;
		
		// Wait double the waitTime to reset
		yield return new WaitForSeconds((waitTime + 1) * 2);
		obstacleMoving = false;
	}
	
	public void Reset(){
		// Reset to the side of the level area
		transform.position = new Vector3(origin.x, origin.y, origin.z);

		// Re-enable the spear's collider
		spearCollider.collider.enabled = true;
	}

	// Detect if this spear hits the Player
	public void OnTriggerEnter(Collider col){
		// If the spear hits the player object
		if(col.tag == "Player"){
			Debug.Log("Spear Hit!");
			// Disable the spear collider temporarily (to prevent multiple deaths for 1 hit)
			spearCollider.collider.enabled = false; // Note: this is reset via the Reset() function
			// Respawn the player
			playerScript.Respawn();
		}
	}
}
