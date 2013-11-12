using UnityEngine;
using System.Collections;

public class DeathCounter : MonoBehaviour {
	public int deathCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition();
		DeathString();
	}
	
	// Set the position of Score GUI to top center
	void SetPosition(){
		guiText.pixelOffset = new Vector2(Screen.width / 500, (float)Screen.height / 2.5f);
	}
	
	// Recieves a message from the Player script with new death count
	void DeathString(){
		guiText.text = deathCount.ToString();
	}
}
