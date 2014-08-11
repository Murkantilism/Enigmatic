using UnityEngine;
using System.Collections;

// FinalRiddleText.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class FinalRiddleText : MonoBehaviour {
	public GUIText FinalRiddleGUIText;

	// Use this for initialization
	void Start () {
		// Assign the GUI Text, script must be attached to same gameObject
		FinalRiddleGUIText = gameObject.guiText;

		// Assign the riddle text based on which final riddle we are on.
		if(Application.loadedLevelName == "FinalRiddle1"){
			FinalRiddleGUIText.text = "A game to some, a dream for others, and a job for a lucky few.\nBillions are spent, millions have wept.";
		}else if(Application.loadedLevelName == "FinalRiddle2"){
			FinalRiddleGUIText.text = "Many things, from the mightest battleship to the simplest\nbattery will gradually fall victim to my appetite.";
		}else if(Application.loadedLevelName == "FinalRiddle3"){
			FinalRiddleGUIText.text = "By your side, can make you laugh but sometimes cry.\nThere when in need, and held in high regard indeed.\nA source of stregth and trust, but casual\n enough to fart in front of when you must.";
		}
	}
}