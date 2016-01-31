using UnityEngine;
using System.Collections;

public class CastingZone : MonoBehaviour {

	void OnTriggerStay(Collider other) {

		if (string.Compare(other.gameObject.tag, "Player") == 0) 
		{
			PlayerControls thePlayer = other.gameObject.GetComponent<PlayerControls>();
			if (Input.GetButtonDown("Use"))
			{
				Debug.Log ("Submitted puzzle");
				thePlayer.SubmitPuzzleSolution();
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
