using UnityEngine;
using System.Collections;

public class YOnRailsPlatformY : MonoBehaviour {
	float speed = 2.0f;
	int direction = 1;
	public int waitTime = 2;
	public int repeatTime = 2;
	Vector3 movement;
	
	// Use this for initialization
	void Start () {
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
		//Debug.Log(movement);
	}
}
