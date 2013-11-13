using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public RiddleScript ridScript;
	public DeathCounter deathScript;
	public GameObject spawn;
	public bool paused = false;
	
	// Use this for initialization
	void Start () {
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();
		deathScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
		spawn = GameObject.Find("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
		// respawn if dead
		if (Dead())
			Respawn();
		
		// check for pause
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = true;
			Time.timeScale = 0;
		}
		
		// execute passive player action
		ridScript.currentRiddle.action.Passive();
		
		// if proper input is pressed, execute player action
		if (Input.GetKey(ridScript.currentRiddle.inputs)) {
			ridScript.currentRiddle.action.Action();
		}
	}
	
	// are we dead yet?
	bool Dead() {
		if (gameObject.transform.position.y < -8)
			return true;
		return false;
	}
	
	// respawn the player
	void Respawn() {
		deathScript.deathCount++;
		gameObject.transform.position = spawn.transform.position;
	}
	
	// show menu when paused
	void OnGUI() {
		if (paused) {
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			
        	if (GUI.Button(new Rect(Screen.width/2-75, Screen.height/2-75, 150, 150), "Resume")) {
            	paused = false;
				Time.timeScale = 1;
			}
		}
        
    }
}

