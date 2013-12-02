using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	public float maxDist = 5;
	public Vector3 startPos = Vector3.zero;
	public bool gotStart = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!gotStart) {
			startPos = transform.position;
			gotStart = true;
		}

		if (Vector3.Distance(transform.position, startPos) > maxDist) {
			Destroy(this.gameObject);
		}

		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x+0.1f, pos.y, pos.z);
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("projectile collided");
		// destroy enemies
		if (col.collider.tag == "Enemy") {
			Destroy(col.gameObject);
			Destroy(this.gameObject);
		}
		// ignore collisions with the Player
		else if (col.collider.tag == "Player") {
			Physics.IgnoreCollision(col.collider, collider);
		}
	}
}
