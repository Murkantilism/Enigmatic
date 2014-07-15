using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinalRiddle1 : MonoBehaviour {

	int correctKeyCnt = 0; // The correct key counter
	int correctKeyThresh = 6; // The threshold for when all correct keys have been pressed

	// A list of correct keys
	List<KeyCode> CorrectKeys = new List<KeyCode>();
	// The GUIText objects for each correct key
	public GUIText correctKey_S;
	public GUIText correctKey_O;
	public GUIText correctKey_C1;
	public GUIText correctKey_C2;
	public GUIText correctKey_E;
	public GUIText correctKey_R;

	// Grab the death script to increment counter
	public DeathCounter deathCntScript;


	// Use this for initialization
	void Start () {
		// Populate the list of correct keys
		CorrectKeys.Add(KeyCode.S);
		CorrectKeys.Add(KeyCode.O);
		CorrectKeys.Add(KeyCode.C);
		CorrectKeys.Add(KeyCode.C);
		CorrectKeys.Add(KeyCode.E);
		CorrectKeys.Add(KeyCode.R);

		// Hide all of the correct keys
		correctKey_S.color = new Color(255, 255, 255, 0);
		correctKey_O.color = new Color(255, 255, 255, 0);
		correctKey_C1.color = new Color(255, 255, 255, 0);
		correctKey_C2.color = new Color(255, 255, 255, 0);
		correctKey_E.color = new Color(255, 255, 255, 0);
		correctKey_R.color = new Color(255, 255, 255, 0);

		// Find & assign the DeathCounter script
		deathCntScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
	}
	
	// Update is called once per frame
	void Update (){
		// If the first correct key is pressed
		if(Input.GetKeyUp (CorrectKeys[0])){
			// Pop the key from the list
			CorrectKeys.RemoveAt(0);
			// Increment the correct key counter
			correctKeyCnt++;

			// FIXME: Decrement the death counter - this is to mitigate the increment of the death counter
			// that occurs for an unknown reason when the correct key is pressed. This decrement simply keeps
			// the death count at the same number it was previously when the correct key is pressed.
			deathCntScript.deathCount--;
		}
		/*
		// If any key is pressed, if it's the correct key, do nothing. Else, increment death counter.
		if(Input.anyKeyDown){
			if(Input.GetKeyUp (CorrectKeys[0])){
				return; // FIXME: Apparently this doesn't prevent correct keys from incrememnting death count for some reason
			}else{
				deathCntScript.deathCount++;
			}*/

			// FIXME: Apparently the below method doesn't work either???

			// If any key is pressed, and it isn't the current correct key, increment death count
			if(Input.anyKeyDown && !(Input.GetKeyUp(CorrectKeys[0]))){
				deathCntScript.deathCount++;
			}
		//}

		// Once enough correct keys have been pressed, load the next riddle
		if(correctKeyCnt >= correctKeyThresh){
			Application.LoadLevel("FinalRiddle2_Intro");
		}

		RevealCorrectKey();
		//Debug.Log(correctKeyCnt);
	}

	// Once a correct key is pressed, show it
	void RevealCorrectKey(){
		if(correctKeyCnt == 1){
			correctKey_S.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 2){
			correctKey_O.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 3){
			correctKey_C1.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 4){
			correctKey_C2.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 5){
			correctKey_E.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 6){
			correctKey_R.color = new Color(255, 255, 255, 1);
		}
	}
}
