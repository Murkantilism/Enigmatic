using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
	private Camera myCamera;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition();
	}
	
	// Set the position of the music track equal to the position
	// of the current camera
	void SetPosition(){
		// Get current camera
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		// Set postion of this object to camera's positon
		gameObject.transform.position = myCamera.transform.position;
	}
}
