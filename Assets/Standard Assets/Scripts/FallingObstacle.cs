﻿using UnityEngine;
using System.Collections;

// NOTE: This script is essentially identical to DropPlatform.cs but is instead
// assigned in the editor rather than at runtime by Player.cs.
public class FallingObstacle : MonoBehaviour {
	Vector3 origin;
	// How long should we wait for the obstacle to fall?
	public int waitTime = 3;
	
	public bool obstacleFalling = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(ObstacleFall());
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(obstacleFalling == true){
			transform.position -= new Vector3(0, 0.04f, 0);
		}

		if(obstacleFalling == false){
			Reset();
			StartCoroutine(ObstacleFall());
		}
	}
	
	public IEnumerator ObstacleFall(){
		// Wait to trigger the obstacle fall
		yield return new WaitForSeconds(waitTime);
		obstacleFalling = true;

		// Wait double the waitTime to reset
		yield return new WaitForSeconds(waitTime * 2);
		obstacleFalling = false;
	}
	
	public void Reset(){
		// Reset above the level area
		transform.position = new Vector3(origin.x, origin.y + 2, origin.z);
	}
}
