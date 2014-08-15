using UnityEngine;
using System.Collections;

public class KongregateSetText2 : MonoBehaviour {

	GUIText kText2;

	// Use this for initialization
	void Start () {
		kText2 = gameObject.GetComponent<GUIText>().guiText;
		
		kText2.text = "The full version includes \n10 more levels plus\n3 final riddle challenges!";
	}
}
