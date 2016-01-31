using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pages : MonoBehaviour {

	private Image page;

	void Awake (){
		page = GetComponentInChildren<Image> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	

		if (Input.GetButton("ViewPages")) {
			page.color = Color.white; //show
		} else 
			page.color = Color.clear; //hide
	}

	void FlipPage () {

	}

}
