using UnityEngine;
using System.Collections;

// DissolvePlatform.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

// NOTE: This script is programmatically assigned at runtime by function OnControllerColliderHit
// in the Player.cs script when the player collides with certain platforms.
public class DissolvePlatform : MonoBehaviour {
	bool destroy = false;
	public tk2dBaseSprite platformSprite;
	public BoxCollider platformCollider;

	// Alpha value used to fade out platform
	float alphaFadeValue = 1;

	public tk2dBaseSprite childSprite;

	GameObject myCamera;
	
	GameObject cameraChild;

	bool fxPlaying = false;

	// Use this for initialization
	void Start () {
		// Get the sprite attached to this gameObject
		platformSprite = GetComponent<tk2dBaseSprite>();
		platformCollider = GetComponent<BoxCollider>();

		// Assign the main camera
		myCamera = GameObject.Find("Main Camera");
		// Instantiate an empty gameobject
		cameraChild = new GameObject();
		// Parent the empty gameobject to the camera
		cameraChild.transform.parent = myCamera.transform;
		// Set the position of the child close to parent
		cameraChild.transform.position = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, myCamera.transform.position.z - 5);
		// Attach an audio source to the child
		cameraChild.AddComponent<AudioSource>();
		// Load the enemy death sound effect
		cameraChild.audio.clip = (AudioClip)Resources.Load("AmbientFX/platform dissolve", typeof(AudioClip));
	}
	
	// Update is called once per frame
	void Update () {
		// If the sound effect isn't already playing
		if(fxPlaying == false){
			// Play the sound effect
			cameraChild.audio.Play();
			fxPlaying = true;
		}

		// Start dissolving the platform when the player touches it
		if(destroy == false){
			alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 0.25f);

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
		fxPlaying = false;
		Destroy(cameraChild);
		GameObject.DestroyImmediate(this);
	}
}
