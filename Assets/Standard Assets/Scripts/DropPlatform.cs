using UnityEngine;
using System.Collections;

// DropPlatform.cs - Last Updated 07/12/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

// NOTE: This script is programmatically assigned at runtime by function OnControllerColliderHit
// in the Player.cs script when the player collides with certain platforms.
public class DropPlatform : MonoBehaviour {
	Vector3 origin;
	bool destroy = false;

	// Use this for initialization
	void Start () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!destroy)
			transform.position -= new Vector3(0, 0.04f, 0);
	}

	public void Reset() {
		transform.position = origin;
		destroy = true;
		GameObject.DestroyImmediate(this);
	}
}
