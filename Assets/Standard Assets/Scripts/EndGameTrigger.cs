using UnityEngine;
using System.Collections;

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

