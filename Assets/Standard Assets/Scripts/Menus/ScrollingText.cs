using UnityEngine;
using System.Collections;

// ScrollingText.cs - Last Updated 07/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class ScrollingText : MonoBehaviour {
	GameObject credits;

	bool triggerScrollingText = false;

	float scrollSpeed = 10;
	float scrollY = 0;

	// Use this for initialization
	void Start () {
		credits = GameObject.Find("Credits");
	}
	
	// Update is called once per frame
	void Update () {
		if(triggerScrollingText == true){
			ScrollTextUp();
		}
	}

	void ScrollTextUp(){
		// Dividing by 5 makes fade lasts 5 secs
		scrollY += Mathf.Clamp01(Time.deltaTime / scrollSpeed);

		credits.transform.position = new Vector3(0, scrollY, 0);
	}

	void TriggerScrollingText(){
		triggerScrollingText = true;
	}
}
