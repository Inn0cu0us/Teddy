using UnityEngine;
using System.Collections;

public class candel : MonoBehaviour {

	public Light Flame;
	public Color CandleColor;
	
	// Use this for initialization
	void Start () {
		Flame = GetComponentInChildren<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		Flame.color = CandleColor;
	}
}
