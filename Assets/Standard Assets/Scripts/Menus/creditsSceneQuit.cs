using UnityEngine;
using System.Collections;

public class creditsSceneQuit : MonoBehaviour {

	public GUISkin guiSkin;

	GameObject mainCamera;

	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
	}

	void Update(){
		// If the backspace or espace key is hit, go back to main menu
		if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)){
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}
	}

	void OnGUI() {	
		GUI.skin = guiSkin;
		
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Main Menu", buttonStyle)){
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}
	}
}
