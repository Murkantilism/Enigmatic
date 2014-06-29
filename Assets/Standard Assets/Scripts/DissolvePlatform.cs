using UnityEngine;
using System.Collections;

// NOTE: This script is programmatically assigned at runtime by function OnControllerColliderHit
// in the Player.cs script when the player collides with certain platforms.
public class DissolvePlatform : MonoBehaviour {
	bool destroy = false;
	public tk2dBaseSprite platformSprite;

	// Alpha value used to fade out platform
	float alphaFadeValue = 1;

	// Use this for initialization
	void Start () {
		// Get the sprite attached to this gameObject
		platformSprite = GetComponent<tk2dBaseSprite>();
	}
	
	// Update is called once per frame
	void Update () {
		// Start dissolving the platform when the player touches it
		if(destroy == false){
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 0.5f);

			platformSprite.color = new Color (255, 255, 255, alphaFadeValue);

			Debug.Log(platformSprite.color.a);
		}

		// If the platform is finished dissolving, destroy it
		if(platformSprite.color.a <= 0){
			destroy = true;
			Debug.Log("Platform Destroyed: " + platformSprite.color.a);
			Destroy(gameObject);
		}
	}
}
