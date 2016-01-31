using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private bool isVictory = false;

	private int WinStreak = 0;
	private Dictionary<GameObject, GameObject> PuzzleSolution;

	public GameObject[] Runes;
	public GameObject[] RitualObjects;
	public int MoonPhase;


	public Candle RedCandle;
	public Dictionary<int, PuzzleSolution> PuzzleSolutions;
	public PuzzleSolution ActualSolution;
	void Start () 
	{



	}

	public void GeneratePuzzle()
	{
		// Select moon phase for this round
		MoonPhase = Random.Range (0, 5);
		PuzzleSolutions = new Dictionary<int, PuzzleSolution> ();
		// Generate Puzzle
		// Repeat for each phase of the moon
		for (int i=0; i<5; i++) {
			List<GameObject> tmpRuneList = new List<GameObject>(Runes);
			List<GameObject> tmpRitualObjectsList = new List<GameObject>(RitualObjects);
			PuzzleSolution aSolution = new PuzzleSolution() 
				{ 	BlackPair = new Dictionary<GameObject, GameObject>(),  
					GreenPair = new Dictionary<GameObject, GameObject>(),  
					RedPair = new Dictionary<GameObject, GameObject>() 
				};
			
			//aSolution.BlackPair.Add(tmpRitualObjectsList[tmpRitualObjectIndex], tmpRuneList[tmpRuneIndex]);
			CreatePair(aSolution.BlackPair, tmpRitualObjectsList, tmpRuneList);
			CreatePair(aSolution.RedPair, tmpRitualObjectsList, tmpRuneList);
			CreatePair(aSolution.GreenPair, tmpRitualObjectsList, tmpRuneList);
			PuzzleSolutions.Add (i, aSolution);
		}
		// Generate the five hint pages, one for each phase of the moon

		// Decide which puzzle to use based on the moon phase
		ActualSolution = PuzzleSolutions [MoonPhase];
		// Put a costume on the red, green and black the ritual objects

		// Hook up the coloured candles to their ritual objects
		//KeyCollection kc = 
		GameObject RedRitualObject = null; 
		var keys = ActualSolution.RedPair.Keys;
		foreach (GameObject go in keys) {
			RedRitualObject = go;
			break;
		}
		Imposter RedImposter = RedRitualObject.GetComponent<Imposter> ();
		RedCandle.RevealedObject = RedImposter;
	}

	private void CreatePair(Dictionary<GameObject, GameObject> RuneRitualObjPair,List<GameObject> RitualObjectList, List<GameObject> RuneList)
	{
		int tmpRuneIndex = Random.Range(0, RuneList.Count);
		int tmpRitualObjectIndex = Random.Range(0, RitualObjectList.Count);
		
		RuneRitualObjPair.Add (RitualObjectList [tmpRitualObjectIndex], RuneList[tmpRuneIndex]);
		RuneList.RemoveAt(tmpRuneIndex);
		RitualObjectList.RemoveAt(tmpRitualObjectIndex);

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

public class PuzzleSolution
{
	public Dictionary<GameObject, GameObject> RedPair;
	public Dictionary<GameObject, GameObject> GreenPair;
	public Dictionary<GameObject, GameObject> BlackPair;
}
