using UnityEngine;
using System.Collections;

// Abstract Riddle Class
public class Riddle {
	
	// This riddle's text
	public string riddleText;
	
	// The inputs required for this level
	public KeyCode inputs;
	
	// The SceneIndex that this Riddle is expected to be at
	public int expectedSceneIndex;
	
	public PlayerAction action;
	
	// Constructor
	public Riddle(string _RiddleText, KeyCode _Inputs, PlayerAction _Action, int _ExpectedSceneIndex){
		riddleText = _RiddleText;
		inputs = _Inputs;
		action = _Action;
		expectedSceneIndex = _ExpectedSceneIndex;
	}
}
