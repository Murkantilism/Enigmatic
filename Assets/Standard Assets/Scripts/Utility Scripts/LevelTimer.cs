using UnityEngine;
using System.Collections;

// LevelTimer.cs - Last Updated 07/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class LevelTimer : MonoBehaviour {	
	// Update is called once per frame
	void Update () {
		SetPosition();
		TimerString();
	}
	
	// Set the position of Score GUI to top center
	void SetPosition(){
		guiText.pixelOffset = new Vector2(Screen.width / 800, (float)Screen.height / 2.5f);
	}
	
	// Recieves a message from the Player script with new death count
	void TimerString(){
		guiText.text = "Time: " + Time.timeSinceLevelLoad.ToString("F1");
	}
}
