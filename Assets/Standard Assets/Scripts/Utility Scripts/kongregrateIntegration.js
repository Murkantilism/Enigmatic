// This script is attached the the RiddleText gameObject in order to be preserved throughout each scene.
#pragma strict

var isKongregate = false;
var userId = 0;
var username = "Guest";
var gameAuthToken = "";

function Awake(){

}

function Start () {
	// Begin the API loading process if it is available
	Application.ExternalEval(
	  "if(typeof(kongregateUnitySupport) != 'undefined'){" +
	  " kongregateUnitySupport.initAPI('MyUnityObject', 'OnKongregateAPILoaded');" +
	  "};"
	);
}

// Called by the kongregateUnitySupport object once the API completes loading
function OnKongregateAPILoaded(userInfoString : String){
  // We now know we're on Kongregate
  isKongregate = true;

  // Split the user info up into tokens
  var params = userInfoString.Split("|"[0]);
  userId = parseInt(params[0]);
  username = params[1];
  gameAuthToken = params[2];
}

// Used to submit where deaths occur. Called by RiddleScript.cs's PlayerDeath function
function SubmitDeath(sceneIndex : int){
	Debug.Log("Death logged");
	Application.ExternalCall("kongregate.stats.submit","Died in level: ",sceneIndex);
}

// Used to submit final score at the end of the game
function SubmitScore(deathCount : int){
	Debug.Log("Score logged");
	Application.ExternalCall("kongregate.stats.submit","Final Score: ",deathCount);
}