using UnityEngine;
using System.Collections;

public class SummoningCircle : MonoBehaviour {

	public float Radius;
	public float NumSegments;
	public GameObject RunePrefab;

	// Use this for initialization
	void Start () {
		PlaceRunes ();
		PlaceCastingZone ();
	}

	void PlaceCastingZone()
	{

	}

	// Update is called once per frame
	void Update () {
	
	}

	void PlaceRunes()
	{
		//defining initial values 
		float segAngle = 2f * Mathf.PI / NumSegments;
		float x = 0f;
		float z = 0f;
		for (int i = 0; i < NumSegments; i++) 
		{
			float theta = (i+1)*segAngle;
			x = x + Mathf.Cos(theta);
			z = z + Mathf.Sin (theta);
			Vector3 runePoint = new Vector3 ( x*Radius - Radius, 0f, z*Radius - Radius);


			var newRune = GameObject.Instantiate(RunePrefab);
			newRune.gameObject.transform.position = runePoint + gameObject.transform.position;

		}
	}
}
