using UnityEngine;
using System.Collections;

public class CycleBigSphinxAnim : MonoBehaviour {
	// Link to the animated sprite
	private tk2dSpriteAnimator anim;

	// Use this for initialization
	void Start () {
		// This script must be attached to the sprite to work.
		anim = GetComponent<tk2dSpriteAnimator>();

		// Invoke the 3 different animations on a repeating cycle, changing once every 2 seconds
		InvokeRepeating("CycleAnimation_Slow", 0, 6);
		InvokeRepeating("CycleAnimation_Med", 2, 6);
		InvokeRepeating("CycleAnimation_Fast", 4, 6);
	}

	void CycleAnimation_Slow(){
		anim.Play("BigSphinxTalking_Slow");
	}

	void CycleAnimation_Med(){
		anim.Play("BigSphinxTalking_Med");
	}

	void CycleAnimation_Fast(){
		anim.Play("BigSphinxTalking_Fast");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
