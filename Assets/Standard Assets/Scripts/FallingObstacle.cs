using UnityEngine;
using System.Collections;

public class FallingObstacle : MonoBehaviour {
	Vector3 origin;
	// How long should we wait for the obstacle to fall?
	public float waitTime = 3.0f;
	
	public bool obstacleFalling = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(ObstacleFall());
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(obstacleFalling == true){
			transform.position -= new Vector3(0, 0.06f, 0);
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
		yield return new WaitForSeconds((waitTime + 1) * 2);
		obstacleFalling = false;
	}
	
	public void Reset(){
		// Reset above the level area
		transform.position = new Vector3(origin.x, origin.y + 2, origin.z);
	}
}
