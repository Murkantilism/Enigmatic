using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// FinalRiddle3.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class FinalRiddle3 : MonoBehaviour {
	
	int correctKeyCnt = 0; // The correct key counter
	int correctKeyThresh = 6; // The threshold for when all correct keys have been pressed
	
	// A list of correct keys
	List<KeyCode> CorrectKeys = new List<KeyCode>();
	// The GUIText objects for each correct key
	public GUIText correctKey_F;
	public GUIText correctKey_R;
	public GUIText correctKey_I;
	public GUIText correctKey_E;
	public GUIText correctKey_N;
	public GUIText correctKey_D;
	
	// Grab the death script to increment counter
	public DeathCounter deathCntScript;

	// Hint system variables
	public GUIText hintText;
	GameObject hintText_go;
	
	float hintAlpha;
	
	int hintTimer1 = 60;
	int hintTimer2 = 120;
	int hintTimer3 = 180;
	
	string hint1 = "To give an answer to a Final Riddle,\nspell out the letters of the\nanswer on the keyboard.";
	string hint2 = "Each letter of the answer to this riddle\ncoressponds to the 1st letter of \nprevious riddle's answers.\nSee word bank below.";
	string hint3 = "Not all words in the bank are \npart of the Final Riddle's answer.";

	public RiddleScript ridScript;
	
	
	// Use this for initialization
	void Start () {
		// Populate the list of correct keys
		CorrectKeys.Add(KeyCode.F);
		CorrectKeys.Add(KeyCode.R);
		CorrectKeys.Add(KeyCode.I);
		CorrectKeys.Add(KeyCode.E);
		CorrectKeys.Add(KeyCode.N);
		CorrectKeys.Add(KeyCode.D);
		
		// Hide all of the correct keys
		correctKey_F.color = new Color(255, 255, 255, 0);
		correctKey_R.color = new Color(255, 255, 255, 0);
		correctKey_I.color = new Color(255, 255, 255, 0);
		correctKey_E.color = new Color(255, 255, 255, 0);
		correctKey_N.color = new Color(255, 255, 255, 0);
		correctKey_D.color = new Color(255, 255, 255, 0);

		ridScript = GameObject.Find("RiddleText").GetComponent<RiddleScript>();

		// Find & assign the DeathCounter script
		deathCntScript = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();

		hintText.color = new Color(255, 255, 255, 0);
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
		
		// If any key is pressed, and it isn't the current correct key, increment death count (ignore spacebar, ESC, mouse input)
		if(Input.anyKeyDown && !(Input.GetKeyUp(CorrectKeys[0])) && 
		   !(Input.GetKeyDown(KeyCode.Space)) && !(Input.GetKeyDown(KeyCode.Escape)) && !(Input.GetMouseButton(0)) && !(Input.GetMouseButton(1)) && !(Input.GetMouseButton(2))){
			deathCntScript.deathCount++;
		}
		
		// Once enough correct keys have been pressed, load the EndScene and call the riddleScript's GameComplete function
		if(correctKeyCnt >= correctKeyThresh){
			ridScript.GameComplete();
			Application.LoadLevel("EndScene");

		}
		
		RevealCorrectKey();

		FR_HintSystem();
	}
	
	// Once a correct key is pressed, show it
	void RevealCorrectKey(){
		if(correctKeyCnt == 1){
			correctKey_F.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 2){
			correctKey_R.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 3){
			correctKey_I.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 4){
			correctKey_E.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 5){
			correctKey_N.color = new Color(255, 255, 255, 1);
		}else if(correctKeyCnt == 6){
			correctKey_D.color = new Color(255, 255, 255, 1);
		}
	}

	void FR_HintSystem(){
		// If the level is played for longer than the first hint timer, reveal the instruction hint
		if (Time.timeSinceLevelLoad > hintTimer1 && !(Time.timeSinceLevelLoad > hintTimer2)){
			hintText.text = hint1;
			// Dividing by 7 makes fade lasts 7 secs
			hintAlpha += Mathf.Clamp01(Time.deltaTime / 7);
			try{
				hintText.color = new Color(255, 255, 255, hintAlpha);
			}catch(MissingReferenceException e){
				Debug.Log(e.ToString());
				hintText_go = GameObject.Find ("HintText");
				hintText = hintText_go.GetComponent<GUIText>();
				Debug.Log("Missing Reference resolved, " + hintText_go + " successfully assigned");
			}
		}
		
		// If the level is played for longer than the second hint timer, reveal the next instruction hint
		if (Time.timeSinceLevelLoad > hintTimer2 && !(Time.timeSinceLevelLoad > hintTimer3)){
			hintText.text = hint2;
		}
		
		// If the level is played for longer than the third hint timer, reveal the next instruction hint
		if (Time.timeSinceLevelLoad > hintTimer3){
			hintText.text = hint3;
		}
	}
}
