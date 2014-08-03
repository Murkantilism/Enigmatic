using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public float maxDist = 5;
	public Vector3 startPos = Vector3.zero;
	public bool gotStart = false;

	GameObject myCamera;

	GameObject cameraChild;

	// Use this for initialization
	void Start () {
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
		cameraChild.audio.clip = (AudioClip)Resources.Load("AmbientFX/enemy dying", typeof(AudioClip));
	}
	
	// Update is called once per frame
	void Update () {
		if (!gotStart) {
			startPos = transform.position;
			gotStart = true;
		}

		// If the projectile hits nothing, destroy this projectile and the camera child
		if (Vector3.Distance(transform.position, startPos) > maxDist) {
			Destroy(this.gameObject);
			Destroy(cameraChild);
		}

		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x+0.1f, pos.y, pos.z);
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("projectile collided");
		// destroy enemies
		if (col.collider.tag == "Enemy") {
			cameraChild.audio.Play(); // Play the enemy death sound effect
			Debug.Log("Enemy died!");
			// Destroy the enemy, the child, and this script
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
		// ignore collisions with the Player
		else if (col.collider.tag == "Player") {
			Physics.IgnoreCollision(col.collider, collider);
		}
	}
}
