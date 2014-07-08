using UnityEngine;
using System.Collections;

public class MovingEnemies : MonoBehaviour {
	public float speed = 2.0f;
	int direction = -1;
	public Vector3 movement;
	public int Yrotation = 0;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SwitchDirection", 1, 1);
	}
	
	// Negate the direction every 2 seconds
	void SwitchDirection(){
		direction = direction * -1;
		// Also switch the direction of the transform by compounding 180 degrees
		Yrotation += 180;
		Yrotation = Yrotation * -1;
		transform.rotation = Quaternion.Euler(0, Yrotation, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// Calculate the movement vector to include direction, speed, and time
		movement = Vector3.right * direction * speed * Time.deltaTime;
		// Move the platform based on the calculated movement vector
		transform.Translate(movement);
	}
}
