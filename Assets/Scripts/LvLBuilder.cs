using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LvLBuilder : MonoBehaviour {

	float ID;

	public GameObject[] Prefabs;
	public GameObject[] GameItems;// = new GameObject[];
	public List<GameObject> Spawnpoints;


	// Use this for initialization
	void Start () {
		int ran = Random.Range (6, 26);
		GameItems = new GameObject[ran];
		Spawnpoints = new List<GameObject> (40);
		ChooseObjects ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChooseObjects() {
		//Build list of spawn points
		Tile[] tiles = GetComponentsInChildren<Tile> ();
		foreach (Tile t in tiles) 
		{
			List<GameObject> spawnPointsInTile = new List<GameObject>(t.Spawnpoints);
			Spawnpoints.AddRange(spawnPointsInTile);
		}
		Debug.Log ("There are " + Spawnpoints.Count + " spawnpoints");
		for (int i = 0; i < GameItems.Length-1; i++){

		// Select a prefab from the list of prefabs
			int PrefabSelect = Random.Range(0,Prefabs.Length - 1);
			GameItems[i] = GameObject.Instantiate(Prefabs[PrefabSelect]);

		// Select a spawnpoint from the list of spawnpoints
			int SpawnSelect = Random.Range (0,Spawnpoints.Count - 1);
			GameItems[i].transform.position = Spawnpoints[SpawnSelect].transform.position;

		//remove spawnpoint from list
			Spawnpoints.RemoveAt(SpawnSelect);

		// Place the instance at the spawnpoint
	    // Update the puzzle solution 

			/*switch (i){
			case 0:

				break;
			case 1:

				break;
			case 2:

				break;
			default:

				break;
			}*/


		}

	}
}
