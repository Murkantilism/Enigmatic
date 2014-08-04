#pragma strict

var mouseOverp : boolean = false;

public var playButton : TextMesh;
public var creditsButton : TextMesh;
public var quitButton : TextMesh;
public var shopButton : TextMesh;

var quitButtonp = false;
var creditsButtonp = false;
var playButtonp = false;
//var shopButtonp = false;

var menuIndex : int = 0;

var myCamera : GameObject;
var cameraChild : GameObject;

function Start(){
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

function mouseOver(bool : boolean){
		mouseOverp = bool;
}

function Update () {

	if(Input.GetKeyDown(KeyCode.UpArrow)){
		if(menuIndex >= 1){
			menuIndex--;
			cameraChild.audio.Play();
		}
	}
	
	if(Input.GetKeyDown(KeyCode.DownArrow)){
		if(menuIndex <= 1){
			menuIndex++;
			cameraChild.audio.Play();
		}
	}
	
	// If the mouse isn't already navigating the menu,
	// Change the color of the renderer and boolean value based on index
	if(mouseOverp == false){
		if(menuIndex == 0){
			playButton.renderer.material.color = Color.blue;
			playButtonp = true;
		}else{
			playButton.renderer.material.color = Color.white;
			playButtonp = false;
		}
		
		if(menuIndex == 1){
			creditsButton.renderer.material.color = Color.blue;
			creditsButtonp = true;
		}else{
			creditsButton.renderer.material.color = Color.white;
			creditsButtonp = false;
		}
		
		if(menuIndex == 2){
			quitButton.renderer.material.color = Color.blue;
			quitButtonp = true;
		}else{
			quitButton.renderer.material.color = Color.white;
			quitButtonp = false;
		}
	}
		
	// If the user hits Enter, Spacebar, or Keypad Enter, load the currently selected menu component
	if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
		if(quitButtonp){
		//If the quit button is clicked, quit the game
		Application.Quit();
		}else if(creditsButtonp){
			//If the Credits button is clicked, open the Credits screen
			Application.LoadLevel("CreditsScreen");
		}else if(playButtonp){
			//If the play button is clicked, and there is a saved game, load that
			Application.LoadLevel("IntroScene");
		}
	}
}