using UnityEngine;
using System.Collections;

public class PositionIntro : MonoBehaviour {
	public GUIText introText;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition();
	}

	// Set the position of Score GUI to top center
	void SetPosition(){
		introText.text = "Instructions: Solve the riddles to \nunlock the secrects to gameplay!";
		introText.pixelOffset = new Vector2(Screen.width / 500, Screen.height / 6);
	}
	
	void LoadNextLevel(){
		
	}
}
