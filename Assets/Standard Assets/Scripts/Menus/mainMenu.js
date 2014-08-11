#pragma strict
// mainMenu.js - Last Updated 08/04/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

var quitButtonp = false;
var creditsButtonp = false;
var playButtonp = false;
var feedbackButtonp = false;

public var playButton : TextMesh;
public var creditsButton : TextMesh;
public var quitButton : TextMesh;
public var feedbackButton : TextMesh;

var mainMenuKeyboard : GameObject;

var myCamera : GameObject;
var cameraChild : GameObject;

function Start(){
	mainMenuKeyboard = GameObject.Find("mainMeunKeyboardSupport");
	
	// Assign the main camera
	myCamera = GameObject.Find("Main Camera");
	// Instantiate an empty gameobject
	cameraChild = new GameObject();
	// Parent the empty gameobject to the camera
	cameraChild.transform.parent = myCamera.transform;
	// Set the position of the child close to parent
	cameraChild.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, myCamera.transform.position.z - 5);
	// Attach an audio source to the child
	cameraChild.AddComponent(AudioSource);
	// Load the sound effect
	cameraChild.audio.clip = Resources.Load("AmbientFX/menu sound", typeof(AudioClip));
}

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
	mainMenuKeyboard.SendMessage("mouseOver", true);
	cameraChild.audio.Play();
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
	mainMenuKeyboard.SendMessage("mouseOver", false);
}

function OnMouseUp(){
	if(quitButtonp){
		//If the quit button is clicked, quit the game
		Application.Quit();
	}else if(creditsButtonp){
		//If the Credits button is clicked, open the Credits screen
		Application.LoadLevel("CreditsScreen");
	}else if(playButtonp){
		//If the play button is clicked, and there is a saved game, load that
		Application.LoadLevel("IntroScene");
	}else if(feedbackButtonp){
		Application.OpenURL("https://docs.google.com/forms/d/1bLdp9mbcCEF96fePM-MC1uiJyfGQUX8McQItUuRfb2Q/viewform");
	}
}

// Keyboard support for main menu
function Update(){
}