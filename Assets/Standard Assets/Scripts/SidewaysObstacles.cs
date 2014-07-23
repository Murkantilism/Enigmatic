using UnityEngine;
using System.Collections;

public class SidewaysObstacles : MonoBehaviour {
	Vector3 origin;
	// How long should we wait for the obstacle to trigger?
	public float waitTime = 3.0f;
	
	public bool obstacleMoving = false;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(ObstacleFall());
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(obstacleMoving == true){
			transform.position -= new Vector3(0.6f, 0, 0);
		}
		
		if(obstacleMoving == false){
			Reset();
			StartCoroutine(ObstacleFall());
		}
	}
	
	public IEnumerator ObstacleFall(){
		// Wait to trigger the obstacle fall
		yield return new WaitForSeconds(waitTime);
		obstacleMoving = true;
		
		// Wait double the waitTime to reset
		yield return new WaitForSeconds((waitTime + 1) * 2);
		obstacleMoving = false;
	}
	
	public void Reset(){
		// Reset to the side of the level area
		transform.position = new Vector3(origin.x + 2, origin.y, origin.z);
	}
}
