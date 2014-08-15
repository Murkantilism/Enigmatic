using UnityEngine;
using System.Collections;

public class KongregateDesuraLink : MonoBehaviour {

	TextMesh desuraLink;
	
	GameObject myCamera;
	GameObject cameraChild;
	
	// Use this for initialization
	void Start () {
		desuraLink = gameObject.GetComponent<TextMesh>();
		
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
		// Load the sound effect
		cameraChild.audio.clip = (AudioClip)Resources.Load("AmbientFX/menu sound", typeof(AudioClip));
	}
	
	void OnMouseEnter(){
		// Change the color of the text
		renderer.material.color = Color.blue;
		cameraChild.audio.Play(); // Play the mouse over sound fx
	}
	
	void OnMouseExit(){
		// Change the color of the text back to the original color (white)
		renderer.material.color = Color.white;
	}
	
	// If a credit is clicked, open the coressponding URL
	void OnMouseUp(){
		Application.ExternalEval("window.open('http://www.desura.com/games/enigmatic','_blank')");
		//Application.OpenURL("http://www.desura.com/games/enigmatic");
	}
}
