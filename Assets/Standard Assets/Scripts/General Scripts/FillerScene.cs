using UnityEngine;
using System.Collections;

// This scene is used to keep the scene indicies at the correct number, in between MainMenu and Riddle1 scene.
// This scene should never be loaded, but somehow LevelSelect was loaded once before. This script
// is to ensure that the game continues even if this scene is somehow loaded.
public class FillerScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Application.LoadLevel("Riddle1");
	}
}
