using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {
	Vector3 origin;
	bool destroy = false;
	// How long should we wait for the obstacle to fall?
	public int waitTime = 3;
	
	public bool obstacleFalling = false;

	void Awake(){
		Start ();
	}

	// Use this for initialization
	void Start () {
		ObstacleFall();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator ObstacleFall(){
		Debug.Log("black");
		// Wait to trigger the obstacle fall
		yield return new WaitForSeconds(waitTime);
		obstacleFalling = true;
		// Call Reset
		Reset();
	}

	public void Reset(){
		Debug.Log("white");
	}
}
