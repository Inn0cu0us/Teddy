using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {



	public GameObject[] Runes;
	public GameObject[] RitualObjects;
	public int MoonPhase;
	public float CloseEnough; //the ritual object must be at least this close to its rune position

	public Candle RedCandle;
	public Candle GreenCandle;
	public Candle BlackCandle;
	public Dictionary<int, PuzzleSolution> PuzzleSolutions;
	public PuzzleSolution ActualSolution;
	private bool isVictory = false;
	
	private int WinStreak = 0;
	private Dictionary<GameObject, GameObject> PuzzleSolution;

	private Audio Sound;

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

		GameObject GreenRitualObject = null; 
		keys = ActualSolution.GreenPair.Keys;
		foreach (GameObject go in keys) {
			GreenRitualObject = go;
			break;
		}
		Imposter GreenImposter = GreenRitualObject.GetComponent<Imposter> ();
		GreenCandle.RevealedObject = GreenImposter;

		GameObject BlackRitualObject = null; 
		keys = ActualSolution.BlackPair.Keys;
		foreach (GameObject go in keys) {
			BlackRitualObject = go;
			break;
		}
		Imposter BlackImposter = BlackRitualObject.GetComponent<Imposter> ();
		BlackCandle.RevealedObject = BlackImposter;
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
		return (DistanceBetween (ActualSolution.BlackPair) <= CloseEnough && 
			DistanceBetween (ActualSolution.RedPair) <= CloseEnough &&
			DistanceBetween (ActualSolution.GreenPair) <= CloseEnough &&
			NoBadPlacements ());
	}

	private bool NoBadPlacements()
	{
		bool placementOk = true;
		RaycastHit hitInfo;
		Vector3 rayDir = Vector3.up;
		foreach (GameObject r in Runes) {
			Debug.DrawRay(r.transform.position, rayDir, Color.green, 1f);
			if (Physics.Raycast(r.transform.position, rayDir, out hitInfo, 10f))
			{
				//check the object we hit 
				GameObject maybeARitualObject = hitInfo.collider.gameObject;
				Debug.Log ("Bad placement hit: " + maybeARitualObject.name);
				//is it our buddy?
				//black case
				if (ActualSolution.BlackPair.ContainsKey(r))
				{
					GameObject thePairedRitualObject;
					ActualSolution.BlackPair.TryGetValue(r, out thePairedRitualObject);
					if (!thePairedRitualObject.Equals(maybeARitualObject)) {
						return false;
					}
				}
				if (ActualSolution.GreenPair.ContainsKey(r))
				{
					GameObject thePairedRitualObject;
					ActualSolution.GreenPair.TryGetValue(r, out thePairedRitualObject);
					if (!thePairedRitualObject.Equals(maybeARitualObject)) {
						return false;
					}
				}
				if (ActualSolution.RedPair.ContainsKey(r))
				{
					GameObject thePairedRitualObject;
					ActualSolution.RedPair.TryGetValue(r, out thePairedRitualObject);
					if (!thePairedRitualObject.Equals(maybeARitualObject)) {
						return false;
					}
				}

				// yes, ok!!!
				// no, you die!!!
			}
		}
		return placementOk;
	}
	void RestartLevel()
	{
		// todo
		// display win or lose msg
		// fade to play, reload in 3 seconds
		Application.LoadLevel ("main");
	}

	private float DistanceBetween(Dictionary<GameObject, GameObject> aPair)
	{
		var keys = aPair.Keys;
		foreach (GameObject k in keys) {
			GameObject v;
			aPair.TryGetValue(k, out v);
			return Vector3.Distance(k.transform.position, v.transform.position);
		}
		throw new UnityException ("No keys in dictionary to compute distance");
	}
}

public class PuzzleSolution
{
	public Dictionary<GameObject, GameObject> RedPair;
	public Dictionary<GameObject, GameObject> GreenPair;
	public Dictionary<GameObject, GameObject> BlackPair;

	
}
