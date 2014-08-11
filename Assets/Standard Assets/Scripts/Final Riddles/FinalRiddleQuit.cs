using UnityEngine;
using System.Collections;

// FinalRiddleQuit.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class FinalRiddleQuit : MonoBehaviour {
	bool escPressed = false; // Has the ESC key been pressed?
	public GUISkin guiSkin;

	public DeathCounter deathCntScript; // A reference to DeathCounter.cs
	
	// Use this for initialization
	void Start () {
		deathCntScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
	}
	
	// Update is called once per frame
	void Update () {
		// Check if player wants to quit, if ESC is hit again hide the Quit button
		if (Input.GetKeyDown(KeyCode.Escape)) {
			escPressed = !escPressed;
		}
	}

	void OnGUI(){
		GUI.skin = guiSkin;
		
		GUIStyle quitStyle = new GUIStyle("button");
		quitStyle.fontSize = 40;

		if(escPressed == true){
			// Quit button
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/2.3f, Screen.height/2 + Screen.height/4, 200, 100), "Quit", quitStyle)){
				Application.LoadLevel("MainMenu");
				
				deathCntScript.deathCount = 0; // Reset death counter on quit
			}
		}
	}
}
