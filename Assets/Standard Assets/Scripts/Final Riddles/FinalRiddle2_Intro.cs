using UnityEngine;
using System.Collections;

// FinalRiddle2_Intro.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class FinalRiddle2_Intro : MonoBehaviour {
	public GUIText FinalRiddleIntroText;
	float textAlphaValue;

	// Use this for initialization
	void Start () {
		FinalRiddleIntroText.text = "Final Riddle 2";
		FinalRiddleIntroText.color = new Color(255, 255, 255, 0);
	}
	
	// Update is called once per frame
	void Update () {
		FadeInText();
	}

	// Fade in text for the final riddle instructions
	void FadeInText(){
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
		
		FinalRiddleIntroText.color = new Color(255, 255, 255, textAlphaValue);

		// Load next riddle once at full alpha
		if(textAlphaValue >= 1){
			Application.LoadLevel("FinalRiddle2");
		}
	}
}
