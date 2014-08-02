using UnityEngine;
using System.Collections;

public class IntroductionScene : MonoBehaviour {
	public GUIText IntroText;
	public GUIText ESCText;
	public float textAlphaValue;
	
	// Should we fade in this text?
	bool fadeInTextp = false;
	// Should we fade out this text?
	bool fadeOutTextp = false;

	// Is the fade out complete?
	bool fadeOutComplete = false;

	// Has the intro been skipped?
	bool introSkipped = false;

	public int cnt = 0; // A counter used for changing text

	public tk2dSprite bigSphinxSprite;
	public tk2dSprite littleSphinxSprite;
	public tk2dSprite thebesSprite;

	// Is the player ready to continue?
	bool readyToContinue = false;

	string intro1 = "Years before the Greek King Oedipus is due to \nfulfill his prophecy, the ancient and powerful \nSphinx forsees an end coming to her guard \nover the ancient city of Thebes.";
	string intro2 = "In a bout of worry, The Sphinx creates \nand trains a young new Sphinx in her \nown image, to ultimately succeed her \nin the event of her own demise.";
	string intro3 = "Through many trials and tribulations the young \nSphinx must learn the complex riddles of \nThe Sphinx in order to stump any potential \noutsider attempting to enter Thebes.";
	string intro4 = "Instructions: Solve the riddles or trivia \nto unlock the secrects to gameplay! \nThe first letter of a riddle's answer \nis the letter key used to play.\nPress spacebar to continue!";

	// Use this for initialization
	void Start () {
		// Find & assign the intro text gameObject
		IntroText = GameObject.Find("IntroText").guiText;
		// Assign the text itself
		IntroText.text = intro1;

		// Find & assign the ESC text gameObject
		ESCText = GameObject.Find("ESCText").guiText;

		// Made sure text is hidden at first
		IntroText.color = new Color(255, 255, 255, 0);
		ESCText.color = new Color(255, 255, 255, 0);

		// Kick-off the coroutine used to display the text intro
		StartCoroutine(Introduction());
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
		ChangeIntroductionText();

		// If the user hits escape to skip intro, skip to instructions
		if(Input.GetKeyUp(KeyCode.Escape)){
			// Set counter to 4
			cnt = 4;
			// Set flag to true
			introSkipped = true;
			// Set instructions to full alpha
			IntroText.color = new Color(255, 255, 255, 1);
			// Start an IntroSkipped coroutine
			StartCoroutine(IntroSkipped());
		}

		// If the intro has been skipped, disabling fading
		if(introSkipped == true){
			fadeInTextp = false;
			fadeOutTextp = false;
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			Application.LoadLevel("Riddle1");
		}
	}

	// A coroutine used to display the instructions for the Final Riddles
	IEnumerator Introduction(){
		// Fade out text
		fadeOutTextp = true;

		// Wait for 1st set of intro text to be read
		yield return new WaitForSeconds(8);
		// Fade out text
		fadeOutTextp = true;
		
		// Wait for 2nd set of intro text to be read
		yield return new WaitForSeconds(16);
		// Fade out text
		fadeOutTextp = true;

		// Wait for 3rd set of intro text to be read
		yield return new WaitForSeconds(16);
		// Fade out text
		fadeOutTextp = true;

		// Wait for 4th set of intro text to be read
		yield return new WaitForSeconds(16);
		// Fade out text
		fadeOutTextp = true;
	}

	// If the intro has been skipped, wait for instructions to be read, load first riddle
	IEnumerator IntroSkipped(){
		yield return new WaitForSeconds(1);
	}

	// When the counter is incrememted by FadeOutText(), set the string value of
	// the IntroText to the next instruction string.
	void ChangeIntroductionText(){
		if(cnt == 1){
			IntroText.text = intro1;
		}else if(cnt == 2){
			IntroText.text = intro2;
		}else if(cnt == 3){
			IntroText.text = intro3;
		}else if(cnt == 4){
			IntroText.text = intro4;
		}
	}

	// Fade in text for the final riddle instructions
	void FadeInText(){
		Debug.Log("Fading in...");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValue += Mathf.Clamp01(Time.deltaTime / 5);
		
		IntroText.color = new Color(255, 255, 255, textAlphaValue);

		// If we are showing the 1st set of instructions, fade in big sphinx
		if(cnt == 1){
			Color c = bigSphinxSprite.color;
			c.a = textAlphaValue;
			bigSphinxSprite.color = c;
		}
		// If we are showing the 2nd set of instructions, fade in little sphinx and ESC Text tip
		if(cnt == 2){
			Color c = littleSphinxSprite.color;
			c.a = textAlphaValue;
			littleSphinxSprite.color = c;

			ESCText.color = new Color(255, 255, 255, textAlphaValue);
		}
		// If we are showing the 3rd set of instructions, fade in Thebes sprite
		if(cnt == 3){
			Color c = thebesSprite.color;
			c.a = textAlphaValue;
			thebesSprite.color = c;
		}		
		
		if(textAlphaValue >= 1){
			fadeInTextp = false; // Stop fading in at full alpha
		}
	}
	
	// Fade out text for the final riddle instructions
	void FadeOutText(){
		Debug.Log("FADING OUT!");
		// Dividing by 5 makes fade lasts 5 secs
		textAlphaValue -= Mathf.Clamp01(Time.deltaTime / 5);
		
		IntroText.color = new Color(255, 255, 255, textAlphaValue);

		// If we are fading the 1st set of instructions, fade out big sphinx
		if(cnt == 1){
			Color c = bigSphinxSprite.color;
			if(textAlphaValue >= 0.2f){ // Stop fading out at an alpha of 0.2f
				c.a = textAlphaValue;
			}
			bigSphinxSprite.color = c;
		}
		// If we are fading the 2nd set of instructions, fade out little sphinx
		if(cnt == 2){
			Color c = littleSphinxSprite.color;
			if(textAlphaValue >= 0.2f){ // Stop fading out at an alpha of 0.2f
				c.a = textAlphaValue;
			}
			littleSphinxSprite.color = c;
		}
		// If we are fading the 3rd set of instructions, fade out Thebes sprite
		if(cnt == 3){
			Color c = thebesSprite.color;
			if(textAlphaValue >= 0.2f){ // Stop fading out at an alpha of 0.2f
				c.a = textAlphaValue;
			}
			thebesSprite.color = c;
		}

		if(textAlphaValue <= 0){
			fadeOutComplete = true; // Fade out is complete
			fadeOutTextp = false; // Stop fading out at zero alpha
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
