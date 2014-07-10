using UnityEngine;
using System.Collections;

public class FinalRiddleText : MonoBehaviour {
	public GUIText FinalRiddle1Text;


	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName == "FinalRiddle1"){
			FinalRiddle1Text.text = "A game to some, a dream for others, and a job for a lucky few.\nBillions are spent, millions have wept.";
		}else if(Application.loadedLevelName == "FinalRiddle2"){
			FinalRiddle1Text.text = "Many things, from the mightest battleship to the simplest\nbattery will gradually fall victim to my appetite.";
		}else if(Application.loadedLevelName == "FinalRiddle3"){
			FinalRiddle1Text.text = "By your side, can make you laugh but sometimes cry.\nThere when in need, and held in high regard indeed.\nA source of stregth and trust, but\ncasual enough to fart in front of when you must.";
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
