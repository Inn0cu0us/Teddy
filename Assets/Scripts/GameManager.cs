using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private bool isVictory;
	private int WinStreak = 0;

	void Start () {
		//Generate Puzzle
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