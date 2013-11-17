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
	
	// The action the player character should take for this riddle
	public PlayerAction action;
	
	// The music file associated with this riddle
	public AudioClip audioClip;
	
	// Constructor
	public Riddle(string _RiddleText, KeyCode _Inputs, PlayerAction _Action, int _ExpectedSceneIndex, AudioClip _audioClip){
		riddleText = _RiddleText;
		inputs = _Inputs;
		action = _Action;
		expectedSceneIndex = _ExpectedSceneIndex;
		audioClip = _audioClip;
	}
}
