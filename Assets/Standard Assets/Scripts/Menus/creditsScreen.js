#pragma strict

// Programming
var deniz = false;
var adam = false;
// Special thanks
var casper = false;
var kenny = false;
// Art
var chris = false;
var jen = false;
// Music & Sound
var alex = false;
var ronny = false;
var efe = false;

var mainCamera : GameObject;

var defaultURL = "https://www.facebook.com/incendiaryindustries";

function Start () {
	mainCamera = GameObject.FindWithTag("MainCamera");
}

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
}

function OnMouseUp(){
	if (deniz){
		Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/");
	}else if(adam){
		Application.OpenURL("http://adamgressen.com/");
	}else if(casper){
		Application.OpenURL("http://www.northeastern.edu/camd/artdesign/people/casper-harteveld/");
	}else if(kenny){
		Application.OpenURL("http://kenney.itch.io/kenney-donation");
	}else if(chris){
		Application.OpenURL("https://www.behance.net/gallery/Portfolio/15502353");
	}else if(jen){
		Application.OpenURL("http://jentella.com/wp/?page_id=211");
	}else if(alex){
		Application.OpenURL(defaultURL);
	}else if(ronny){
		Application.OpenURL("https://soundcloud.com/ronnnymrazmusic");
	}else if(efe){
		Application.OpenURL("https://soundcloud.com/falanca");
	}
}

function OnGUI() {
	GUI.backgroundColor = Color.magenta;
	
	var buttonStyle = new GUIStyle("button");
	buttonStyle.fontSize = 25;
	
	if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Main Menu", buttonStyle)){
		Destroy(mainCamera);
		Application.LoadLevel("MainMenu");
	}
}

function Update(){
	// If the backspace or espace key is hit, go back to main menu
	if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)){
		Destroy(mainCamera);
		Application.LoadLevel("MainMenu");
	}
}