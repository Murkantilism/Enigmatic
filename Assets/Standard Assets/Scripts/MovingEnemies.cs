using UnityEngine;
using System.Collections;

public class MovingEnemies : MonoBehaviour {
	public float speed = 2.0f;
	int direction = -1;
	public Vector3 movement;
	public int Yrotation = 0;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("SwitchDirection", 1, Random.Range(1, 3));
	}
	
	// Negate the direction every 2 seconds
	void SwitchDirection(){
		direction = direction * -1;
	}
	
	// Update is called once per frame
	void Update () {
		// Calculate the movement vector to include direction, speed, and time
		movement = Vector3.right * direction * speed * Time.deltaTime;
		// Move the platform based on the calculated movement vector
		transform.Translate(movement);
	}
}
