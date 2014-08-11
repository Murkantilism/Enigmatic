using UnityEngine;
using System.Collections;

// FinalRiddle1.cs - Last Updated 06/30/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class EndGameTrigger : MonoBehaviour
{
	public RiddleScript ridScript;
	
	void Start() {
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.tag == "Player") { //Checks if the Player is inside the trigger
	    	ridScript.completeLevel();
	    }
	}
}

