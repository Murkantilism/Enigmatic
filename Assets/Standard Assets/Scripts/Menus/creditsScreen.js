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

var defaultURL = "https://www.facebook.com/incendiaryindustries";

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

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
	cameraChild.audio.Play(); // Play the mouse over sound fx
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
}

// If a credit is clicked, open the coressponding URL
function OnMouseUp(){
	if (deniz){
		Application.ExternalEval("window.open('http://www.ccs.neu.edu/home/ozkaynak/','_blank')");
		//Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/");
	}else if(adam){
		Application.ExternalEval("window.open('http://adamgressen.com/','_blank')");
		//Application.OpenURL("http://adamgressen.com/");
	}else if(casper){
		Application.ExternalEval("window.open('ttp://www.northeastern.edu/camd/artdesign/people/casper-harteveld/','_blank')");
		//Application.OpenURL("http://www.northeastern.edu/camd/artdesign/people/casper-harteveld/");
	}else if(kenny){
		Application.ExternalEval("window.open('http://kenney.itch.io/kenney-donation','_blank')");
		//Application.OpenURL("http://kenney.itch.io/kenney-donation");
	}else if(chris){
		Application.ExternalEval("window.open('https://www.behance.net/gallery/Portfolio/15502353','_blank')");
		//Application.OpenURL("https://www.behance.net/gallery/Portfolio/15502353");
	}else if(jen){
		Application.ExternalEval("window.open('http://jentella.com/wp/?page_id=211','_blank')");
		//Application.OpenURL("http://jentella.com/wp/?page_id=211");
	}else if(alex){
		Application.ExternalEval("window.open('https://www.facebook.com/incendiaryindustries','_blank')");
		//Application.OpenURL(defaultURL);
	}else if(ronny){
		Application.ExternalEval("window.open('https://soundcloud.com/ronnnymrazmusic','_blank')");
		//Application.OpenURL("https://soundcloud.com/ronnnymrazmusic");
	}else if(efe){
		Application.ExternalEval("window.open('https://soundcloud.com/falanca','_blank')");
		//Application.OpenURL("https://soundcloud.com/falanca");
	}
}