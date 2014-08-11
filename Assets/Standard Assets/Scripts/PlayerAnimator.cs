using UnityEngine;
using System.Collections;

// FinalRiddle1.cs - Last Updated 07/14/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

public class PlayerAnimator : MonoBehaviour {
	public tk2dSpriteAnimator anim;
	
	// Use this for initialization
	void Start () {
		anim = GameObject.Find("PlayerAnimatedSprite").GetComponent<tk2dSpriteAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
