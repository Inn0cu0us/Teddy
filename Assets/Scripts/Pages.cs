using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pages : MonoBehaviour {

	private Image[] pages;

	private int PageToBurn;
	private int[] Burned;

	void Awake (){
		pages = GetComponentsInChildren<Image> ();
		PageToBurn = 1;
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
			pages [PageToBurn - 1].color = Color.red;


			} else {
			foreach( Image joint in pages ){
				joint.color = Color.clear; //hide
			}
		}
		FlipPage ();
	}

	void FlipPage () {

		if(Input.GetButton ("1")){
			PageToBurn = 1;}
		if(Input.GetButton ("2")){
			PageToBurn = 2;}
		if(Input.GetButton ("3")){
			PageToBurn = 3;}
		if(Input.GetButton ("4")){
			PageToBurn = 4;}
		if(Input.GetButton ("5")){
			PageToBurn = 5;}
		
	}

}
