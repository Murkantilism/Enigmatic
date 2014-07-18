using UnityEngine;
using System.Collections;

// NOTE: This script must be attached to the BackgroundMusic gameObject in order to function properly.
// This object can be found in the Riddle1 scene, and also in the Prefabs folder.
public class BackgroundMusic : MonoBehaviour {
	private Camera myCamera;

	public RiddleScript ridScript;

	// Scene index
	public int sceneIndex;

	// Scene ID
	public string sceneID;

	// Has the track been successfully loaded?
	public bool trackLoaded = false;

	// Use this for initialization
	void Start () {
		// Find the ridScript GameObject
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();

		// Get the scene index
		sceneIndex = ridScript.sceneIndex;

		// Assign the scene ID
		sceneID = ridScript.sceneID;
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition();
		BackgroundMusicSwitch();

		sceneIndex = ridScript.sceneIndex;
		sceneID = ridScript.sceneID;
	}
	
	// Set the position of the music track equal to the position
	// of the current camera
	void SetPosition(){
		// Get current camera
		myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		// Set postion of this object to camera's positon
		gameObject.transform.position = myCamera.transform.position;
	}

	void BackgroundMusicSwitch(){
		// If the scene index is even and non-zero, it is a riddle
		if (sceneIndex % 2 == 0 && sceneIndex != 0){
			// If the track hasn't already been loaded (this is to prevent dozens of calls to Resources.Load())
			if(trackLoaded == false){
				trackLoaded = true;
				// Set the audio clip to the new music track
				audio.clip = (AudioClip)Resources.Load("MusicTracks/" + (sceneIndex - 2).ToString(), typeof(AudioClip));
				audio.Play();
			}
		}

		// If the scene index is odd, it is a level
		if (sceneIndex % 1 == 0 && sceneID == "Level"){
			trackLoaded = false; // Reset trackLoaded boolean
		}
	}
}