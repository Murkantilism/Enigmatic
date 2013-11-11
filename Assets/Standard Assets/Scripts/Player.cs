using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public RiddleScript ridScript;
	public DeathCounter deathScript;
	public GameObject spawn;
	
	// Use this for initialization
	void Start () {
		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();
		deathScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
		spawn = GameObject.Find("Spawn");
	}
	
	// Update is called once per frame
	void Update () {
		if (Dead())
			Respawn();
		
		ridScript.currentRiddle.action.Passive();
		
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
	
	void Respawn() {
		deathScript.deathCount++;
		gameObject.transform.position = spawn.transform.position;
	}
}

