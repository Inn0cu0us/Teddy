using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private bool isVictory = false;
	private int WinStreak = 0;
	private Dictionary<GameObject, GameObject> PuzzleSolution;
	public GameObject RedObject;
	public GameObject RedRune;
	public GameObject GreenObject;
	public GameObject GreenRune;
	public GameObject BlackObject;
	public GameObject BlackRune;

	public GameObject[] Runes;
	public GameObject[] RitualObjects;

	void Start () {
		// Generate Puzzle
		// Assign one object to be red object
		// Assign one rune to be the red rune
	}
	
	// Update is called once per frame
	void Update () {
		if (isVictory)
		{

		}
	}

	public bool CheckPuzzle()
	{
		//called from player
		//if wrong, you die!
		return true;

	}

	void RestartLevel()
	{
		Application.LoadLevel ("main");
	}
}