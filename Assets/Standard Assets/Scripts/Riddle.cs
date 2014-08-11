using UnityEngine;
using System.Collections;

// FinalRiddle1.cs - Last Updated 06/28/2014
// Enigmatic - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any question

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
	
	// The small hint associated with this riddle
	public string smallHint;
	
	// The big hint associated with this riddle
	public string bigHint;
	
	// Constructor
	public Riddle(string _RiddleText, KeyCode _Inputs, PlayerAction _Action, int _ExpectedSceneIndex, AudioClip _audioClip, string _smallHint, string _bigHint){
		riddleText = _RiddleText;
		inputs = _Inputs;
		action = _Action;
		expectedSceneIndex = _ExpectedSceneIndex;
		audioClip = _audioClip;
		smallHint = _smallHint;
		bigHint = _bigHint;
	}
}
