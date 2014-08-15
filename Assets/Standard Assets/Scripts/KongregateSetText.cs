using UnityEngine;
using System.Collections;

public class KongregateSetText : MonoBehaviour {

	GUIText kText;

	// Use this for initialization
	void Start () {
		kText = gameObject.GetComponent<GUIText>().guiText;

		kText.text = "Please consider supporting \nthe developer by \npurchasing the full \nversion, available here:";
	}
}
