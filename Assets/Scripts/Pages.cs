using UnityEngine;
using System.Collections;

public class Pages : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetButtonDown ("ViewPages")) {
			CanvasGroup.alpha = 1f; //show
		} else 
			CanvasGroup.alpha = 0f; //hide
	}

	void FlipPage () {

	}

}
