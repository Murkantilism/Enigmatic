using UnityEngine;
using System.Collections;

public class FinalRiddlesInstructions : MonoBehaviour {
	public GUIText FinalRiddleIntroText;
	float textAlphaValue;

	// Should we fade in this text?
	bool fadeInTextp = false;
	// Should we fade out this text?
	bool fadeOutTextp = false;
	
	// Is the fade out complete?
	bool fadeOutComplete = false;

	int cnt = 0; // A counter used for changing text

	string instructions1 = "There are 3 Final Riddles for you to solve.\nYou must use only your wit and mind. Good luck.";
	string instructions2 = "Final Riddle 1";

	// Use this for initialization
	void Start () {
		FinalRiddleIntroText.text = "Congratulations on solving all 20 riddles and\nsurviving their associated levels!\nNow your final challenge begins...";
		StartCoroutine(FinalRiddleInstructions());
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeInTextp == true){
			FadeInText();
		}
		if(fadeOutTextp == true){
			FadeOutText();
		}

		WhenFadeOutIsComplete();
		ChangeInstructions();
	}

	// A coroutine used to display the instructions for the Final Riddles
	IEnumerator FinalRiddleInstructions(){
		// Wait for 1st set of instructions to be read
		yield return new WaitForSeconds(8);
		// Fade out text
		fadeOutTextp = true;

		// Wait for the 2nd set of instructions to be read
		yield return new WaitForSeconds(14);
		fadeOutTextp = true;

		// Wait for the 'Final Riddle 1' text to show, then load Final Riddle 1
		yield return new WaitForSeconds(10);
		Application.LoadLevel("FinalRiddle1");
	}

	// When the counter is incrememted by FadeOutText(), set the string value of
	// the FinalRiddleText to the next instruction string.
	void ChangeInstructions(){
		if(cnt == 1){
			FinalRiddleIntroText.text = instructions1;
		}else if(cnt == 2){
			FinalRiddleIntroText.text = instructions2;
		}
	}

	// Fade in text for the final riddle instructions
	void FadeInText(){
		Debug.Log("Fading in...");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
		
		FinalRiddleIntroText.color = new Color(255, 255, 255, textAlphaValue);

		if(textAlphaValue >= 1){
			fadeInTextp = false; // Stop fading in
		}
	}

	// Fade out text for the final riddle instructions
	void FadeOutText(){
		Debug.Log("FADING OUT!");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValue -= Mathf.Clamp01(Time.deltaTime / 5);

		FinalRiddleIntroText.color = new Color(255, 255, 255, textAlphaValue);

		if(textAlphaValue <= 0){
			fadeOutComplete = true; // Fade out is complete
			fadeOutTextp = false; // Stop fading out
			cnt++; // Increment counter
		}
	}

	// When the fade out is complete, start fading the text back in
	void WhenFadeOutIsComplete(){
		if(fadeOutComplete == true){
			fadeInTextp = true;
			fadeOutComplete = false; // Reset the flag
		}
	}
}
