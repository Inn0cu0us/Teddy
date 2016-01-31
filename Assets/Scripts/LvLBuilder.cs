using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LvLBuilder : MonoBehaviour {

	public GameObject[] ClutterPrefabs;
	public GameObject[] RitualPrefabs;
	public List<GameObject> Spawnpoints;
	public GameObject[] RitualObjects;
	public Material[] ClutterMaterials;
	private GameObject[] GameItems;// = new GameObject[];
	private GameManager theManager;
	private SummoningCircle theCircle;
	private GameObject[] Runes;

	void Awake()
	{
		theManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		theCircle = GameObject.FindGameObjectWithTag ("SummoningCircle").GetComponent<SummoningCircle> ();
	}

	// Use this for initialization
	void Start () 
	{
		Runes = theCircle.PlaceRunes ();
		PlaceCastingZone ();
		int ran = Random.Range (6, 26);
		GameItems = new GameObject[ran];
		RitualObjects = new GameObject[RitualPrefabs.Length];
		Spawnpoints = new List<GameObject> (40);
		PlaceObjects ();
		InitManager ();
		theManager.GeneratePuzzle ();
	}

	void InitManager()
	{
		theManager.Runes = Runes;
		theManager.RitualObjects = RitualObjects;
	}

	// Update is called once per frame
	void Update () 
	{
	
	}

	void PlaceObjects() {
		//Build list of spawn points
		Tile[] tiles = GetComponentsInChildren<Tile> ();
		foreach (Tile t in tiles) 
		{
			List<GameObject> spawnPointsInTile = new List<GameObject>(t.Spawnpoints);
			Spawnpoints.AddRange(spawnPointsInTile);
		}
		Debug.Log ("There are " + Spawnpoints.Count + " spawnpoints");

		// Place the ritual objects
		for (int i=0; i<=RitualPrefabs.Length-1; i++) {
			RitualObjects[i] = GameObject.Instantiate(RitualPrefabs[i]);
			Imposter fake = RitualObjects[i].GetComponent<Imposter>();
			if (fake != null)
			{
				//randomly select a clutter object
				int clutterIndex = Random.Range(0, ClutterMaterials.Length);
				GameObject aClutterObject = ClutterPrefabs[clutterIndex];

				//?? doesn't work???
				//MeshRenderer aDisguise = aClutterObject.GetComponentInChildren<MeshRenderer>();


				// get its material
				Material materialDisguise = ClutterMaterials[clutterIndex];
				// set the imposter image to the material
				fake.ImposterImage = materialDisguise;
				MeshRenderer[] backAndFrontRenderer = RitualObjects[i].GetComponentsInChildren<MeshRenderer>();
				foreach (MeshRenderer mr in backAndFrontRenderer)
				{
					fake.TrueImage = mr.material; //delete if not working
					mr.material = fake.ImposterImage;
				}
			}
			int SpawnSelect = Random.Range (0,Spawnpoints.Count - 1);
			RitualObjects[i].transform.position = Spawnpoints[SpawnSelect].transform.position;
			Spawnpoints.RemoveAt(SpawnSelect);
		}
		// Place the clutter
		for (int i = 0; i < GameItems.Length-1; i++){

		// Select a prefab from the list of prefabs
			int PrefabSelect = Random.Range(0,ClutterPrefabs.Length - 1);
			GameItems[i] = GameObject.Instantiate(ClutterPrefabs[PrefabSelect]);

		// Select a spawnpoint from the list of spawnpoints
			int SpawnSelect = Random.Range (0,Spawnpoints.Count - 1);
			GameItems[i].transform.position = Spawnpoints[SpawnSelect].transform.position;

		//remove spawnpoint from list
			Spawnpoints.RemoveAt(SpawnSelect);
		}
	}

	void PlaceCastingZone()
	{
		
	}


}
