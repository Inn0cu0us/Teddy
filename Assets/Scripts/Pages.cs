using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pages : MonoBehaviour {

	private Image[] pages;

	void Awake (){
		pages = GetComponentsInChildren<Image> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
			if (Input.GetButton("ViewPages")) {
			foreach( Image joint in pages ){
				joint.color = Color.white; //show
			}
			} else {
			foreach( Image joint in pages ){
				joint.color = Color.clear; //hide
			}
		}
	}

	void FlipPage () {

	}

}
