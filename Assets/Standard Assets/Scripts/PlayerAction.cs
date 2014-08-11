﻿using UnityEngine;
using System.Collections;

// PlayerAction.cs - Last Updated 08/11/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

// Abstract base class
public abstract class PlayerAction{
	// Base variables all player actions will need
	public CharacterController playerController;
	public GameObject playerObj;
	public float speed = 6.0f;
	public float jumpSpeed  = 8.0f;
	public float gravity = 20.0f;
	public Vector3 moveDirection = Vector3.zero;
	
	public PlayerAnimator playerAnim;

	Player playerScript;
	
	// Use this for initialization
	void Start () {
		// Find & assign the required gameObjects
		playerObj = GameObject.FindGameObjectWithTag("Player");
		playerController = playerObj.GetComponent<CharacterController>();
		playerAnim = GameObject.Find("PlayerAnimatedSprite").GetComponent<PlayerAnimator>();
		Debug.Log(playerAnim);
	}

	// Find & assign the player gameObject
	public void GetPlayerObject() {
		if (playerObj == null) {
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
	}

	// Find & assign the player controller
	public void GetPlayerController() {
		if (playerController == null) {
			playerController = playerObj.GetComponent<CharacterController>();
		}
	}

	// Find & assign the Player.cs script
	public void GetPlayerScript(){
		if(playerScript == null){
			playerScript = playerObj.GetComponent<Player>();
		}
	}

	// Move the player character
	public void MoveForward() {
		GetPlayerObject();
		GetPlayerController();
		
		// If player is grounded, recalcuate move direction
		if (playerController.isGrounded){
			playerAnim = GameObject.Find("PlayerAnimatedSprite").GetComponent<PlayerAnimator>();
			playerAnim.anim.Play("Walk");
			moveDirection = new Vector3(1, 0, 0);
			moveDirection = playerController.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		playerController.Move(moveDirection * Time.deltaTime);
	}

	public void Jump() {
		GetPlayerObject();
		GetPlayerController();
		GetPlayerScript();

		// If the player is on a falling platform, ignore grounded requirements, and let player do a half jump
		if(playerScript.onDropPlatform == true){
			playerAnim = GameObject.Find("PlayerAnimatedSprite").GetComponent<PlayerAnimator>();
			// If the jump animation isn't already playing, play it
			if(!playerAnim.anim.IsPlaying("Jump")){
				playerAnim.anim.Play("Jump");
			}else{
				playerAnim.anim.Play("Walk");
			}
			
			moveDirection = new Vector3(1, 0, 0);
			moveDirection = playerController.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			moveDirection.y = jumpSpeed;
		}

		// If player is grounded, recalcuate move direction with jump speed added to Y direction
		if (playerController.isGrounded){
			playerAnim = GameObject.Find("PlayerAnimatedSprite").GetComponent<PlayerAnimator>();
			// If the jump animation isn't already playing, play it
			if(!playerAnim.anim.IsPlaying("Jump")){
				playerAnim.anim.Play("Jump");
			}else{
				playerAnim.anim.Play("Walk");
			}

			moveDirection = new Vector3(1, 0, 0);
			moveDirection = playerController.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			moveDirection.y = jumpSpeed;
		}

		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		// Move the controller
		playerController.Move(moveDirection * Time.deltaTime);
	}

	public void Shoot() {
		GetPlayerObject();
		GetPlayerController();

		if (GameObject.FindGameObjectWithTag("Projectile") == null) {
			// instantiate a projectile
			GameObject proj = (GameObject)GameObject.Instantiate(playerObj.GetComponent<Player>().projectile);
			proj.transform.position = playerObj.transform.position;
		}
	}

	// Abstract action method
	abstract public void Action();
	
	// Abstract passive action method
	abstract public void Passive();
}

// Move actively (moving only left to right)
class MoveAction : PlayerAction{
	// Override action method
	public override void Action(){
		MoveForward();
	}
	
	public override void Passive(){}
	
	// Example instantiation of MoveAction
	static void Main(){
		MoveAction move = new MoveAction();
		move.Action();
		move.Passive();
	}
}

// Jump actively, Move passively
class JumpAction : PlayerAction{
	// Override action method
	public override void Action() {
		Jump();
	}
	
	public override void Passive() {
		MoveForward();
	}
	
	// Example instantiation of JumpAction
	static void Main(){
		JumpAction jump = new JumpAction();
		jump.Action();
		jump.Passive();
	}
}

// Shoot actively, Move passively
class ShootAction : PlayerAction{
	// Override action method
	public override void Action(){
		Shoot();
	}
	public override void Passive(){
		MoveForward();
	}
}

// Jump actively, Move and Shoot passively
class JumpShootAction : PlayerAction{
	// Override action method
	public override void Action(){
		Jump();
	}
	public override void Passive(){
		MoveForward();
		Shoot();
	}
}

// Move actively, shoot passively
class MoveShootAction : PlayerAction{
	// Override action method
	public override void Action(){
		MoveForward();
	}
	// Override passive method
	public override void Passive(){
		Shoot();
	}
}

// No Action
class NoAction : PlayerAction{
	public override void Action(){}
	public override void Passive(){}
}

