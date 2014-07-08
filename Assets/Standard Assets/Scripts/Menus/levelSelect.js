#pragma strict
// levelSelect.js - Last Updated 01/04/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions
public class levelSelect extends MonoBehaviour{
	var levelSelectedText;
	
	var moreLevels = false;
	var mainCamera : GameObject;
	
	//var levelSelectKeyboard : GameObject;
	
	function Start(){
		mainCamera = GameObject.FindWithTag("MainCamera");

		//levelSelectKeyboard = GameObject.Find("levelSelectKeyboardSupport"); // First level selection screen
	}

	function OnMouseEnter(){
		// Change the color of the text
		renderer.material.color = Color.blue;
		//levelSelectKeyboard.SendMessage("mouseOver", true);
	}

	function OnMouseExit(){
		// Change the color of the text back to the original color (white)
		renderer.material.color = Color.white;
		//levelSelectKeyboard.SendMessage("mouseOver", false);
	}

	function OnMouseUp(){
		// Save the text value of the level selected
		levelSelectedText = GetComponent(TextMesh).text;
	
		// Load the level selected
		Application.LoadLevel("Riddle" + levelSelectedText);
	}

	function OnGUI() {
		GUI.backgroundColor = Color.magenta;
		
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}
	}
}