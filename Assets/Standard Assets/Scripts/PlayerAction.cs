using UnityEngine;
using System.Collections;

// Abstract base class
public abstract class PlayerAction{
	// Base variables all player actions will need
	public CharacterController playerController;
	public GameObject playerObj;
	public float speed = 6.0f;
	public float jumpSpeed  = 8.0f;
	public float gravity = 20.0f;
	public Vector3 moveDirection = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		playerObj = GameObject.FindGameObjectWithTag("Player");
		playerController = playerObj.GetComponent<CharacterController>();
	}

	// Abstract action method
	abstract public void Action();
}

// Player move action (moving only left to right)
class MoveAction : PlayerAction{
	public void GetPlayerObject() {
		if (playerObj == null) {
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
	}
	
	public void GetPlayerController() {
		if (playerController == null) {
			playerController = playerObj.GetComponent<CharacterController>();
		}
	}
	
	// Override action method
	public override void Action(){
		GetPlayerObject();
		GetPlayerController();
		
		// If player is grounded, recalcuate move direction
		if (playerController.isGrounded){
			moveDirection = new Vector3(1, 0, 0);
			moveDirection = playerController.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		playerController.Move(moveDirection * Time.deltaTime);
	}
	
	// Example instantiation of MoveAction
	static void Main(){
		MoveAction move = new MoveAction();
		move.Action();
	}
}

// Player jump action
class JumpAction : PlayerAction{
	// Override action method
	public override void Action(){
		moveDirection.y = jumpSpeed;
	}
	
	// Example instantiation of JumpAction
	static void Main(){
		JumpAction jump = new JumpAction();
		jump.Action();
	}
}

class ShootAction : PlayerAction{
	// Override action method
	public override void Action(){
		// ToDo: Shoot code here
	}
}