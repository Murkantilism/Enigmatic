using UnityEngine;
using System.Collections;

public class EndScene : MonoBehaviour {
	GUIText thanksText;
	GUIText gratsText;

	float textAlphaValueA = 0;
	float textAlphaValueB = 1;

	// Should we fade in this text?
	bool fadeInTextp = false;
	// Should we fade out this text?
	bool fadeOutTextp = false;

	ScrollingText scrollText;

	// Use this for initialization
	void Start () {
		// Find & Assign text
		gratsText = GameObject.Find("GratsText").guiText;
		thanksText = GameObject.Find("ThanksText").guiText;

		// Assign text strings
		gratsText.text = "Congratulations on solving all of my riddles, young one. \nThough you are not yet wise & old enough to replace me,\nthere will come a day where you must.";
		thanksText.text = "Thank you \nfor playing!";

		// Hide thanks text
		thanksText.color = new Color(255, 255, 255, 0);

		scrollText = GameObject.Find("Credits").GetComponent<ScrollingText>();

		StartCoroutine(EndSceneText());
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeInTextp == true){
			FadeInText();
		}
		if(fadeOutTextp == true){
			FadeOutText();
		}
	}

	IEnumerator EndSceneText(){
		// Wait for grats text to be read
		yield return new WaitForSeconds(10);
		// Fade in Thanks text
		fadeInTextp = true;
		// Fade out Grats text
		fadeOutTextp = true;
		// Trigger scrolling credits
		scrollText.SendMessage("TriggerScrollingText");
		// Wait for thanks text to be read, then load Credits
		yield return new WaitForSeconds(35);
		Application.Quit();
	}

	// Fade in text for the final riddle instructions
	void FadeInText(){
		Debug.Log("Fading in...");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValueA += Mathf.Clamp01(Time.deltaTime / 8);
		
		thanksText.color = new Color(255, 255, 255, textAlphaValueA);

		if(textAlphaValueA >= 1){
			fadeInTextp = false; // Stop fading in
		}
	}
	
	// Fade out text for the final riddle instructions
	void FadeOutText(){
		Debug.Log("FADING OUT!");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValueB -= Mathf.Clamp01(Time.deltaTime / 5);
		
		gratsText.color = new Color(255, 255, 255, textAlphaValueB);

		if(textAlphaValueB <= 0){
			fadeOutTextp = false; // Stop fading out
		}
	}
}
