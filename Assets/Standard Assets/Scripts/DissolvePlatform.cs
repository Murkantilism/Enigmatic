using UnityEngine;
using System.Collections;

// NOTE: This script is programmatically assigned at runtime by function OnControllerColliderHit
// in the Player.cs script when the player collides with certain platforms.
public class DissolvePlatform : MonoBehaviour {
	bool destroy = false;
	public tk2dBaseSprite platformSprite;
	public BoxCollider platformCollider;

	// Alpha value used to fade out platform
	float alphaFadeValue = 1;

	public tk2dBaseSprite childSprite;

	// Use this for initialization
	void Start () {
		// Get the sprite attached to this gameObject
		platformSprite = GetComponent<tk2dBaseSprite>();
		platformCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		// Start dissolving the platform when the player touches it
		if(destroy == false){
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 0.5f);

			platformSprite.color = new Color (255, 255, 255, alphaFadeValue);

			Debug.Log(platformSprite.color.a);

			// If this sprite has children, dissolve them too
			if(transform.childCount > 0){
				foreach (Transform child in transform)
				{
					// Get the sprite
					childSprite = child.GetComponent<tk2dBaseSprite>();
					//
					childSprite.color = new Color(255, 255, 255, alphaFadeValue);
				}
			}
		}

		// If the platform is finished dissolving, destroy it
		if(platformSprite.color.a <= 0){
			destroy = true;
			platformCollider.enabled = false;
			Debug.Log("Platform Destroyed: " + platformSprite.color.a);
		}
	}

	public void Reset(){
		platformSprite.color = new Color(255, 255, 255, 1);
		platformCollider.enabled = true;

		// If this sprite has children, reset their color too
		if(transform.childCount > 0){
			foreach(Transform child in transform){
				childSprite = child.GetComponent<tk2dBaseSprite>();
				childSprite.color = new Color(255, 255, 255, 1);
			}
		}

		destroy = true;
		GameObject.DestroyImmediate(this);
	}
}
