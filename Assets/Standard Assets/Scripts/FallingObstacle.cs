using UnityEngine;
using System.Collections;

// NOTE: This script is essentially identical to DropPlatform.cs but is instead
// assigned in the editor rather than at runtime by Player.cs.
public class FallingObstacle : MonoBehaviour {
	Vector3 origin;
	bool destroy = false;
	// How long should we wait for the obstacle to fall?
	public int waitTime = 3;
	
	public bool obstacleFalling = false;
	
	// Use this for initialization
	void Start () {
		ObstacleFall();
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!destroy && obstacleFalling == true){
			transform.position -= new Vector3(0, 0.04f, 0);
		}
	}
	
	public IEnumerator ObstacleFall(){
		Debug.Log("black");
		// Wait to trigger the obstacle fall
		yield return new WaitForSeconds(waitTime);
		obstacleFalling = true;
		// Call Reset
		Reset();
	}
	
	public IEnumerator Reset(){
		Debug.Log("white");
		// Wait double the wait time to reset
		yield return new WaitForSeconds(waitTime * 2);
		// Reset
		transform.position = origin;
		destroy = true;
		GameObject.DestroyImmediate(this);
		Debug.Log("negro");
	}
}
