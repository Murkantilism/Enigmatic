using UnityEngine;
using System.Collections;

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
