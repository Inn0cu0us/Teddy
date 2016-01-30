using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	Vector3 Movement;
	bool IsPushing = false;
	bool UseThing = false;
	private Rigidbody myBody;


	public float Speed;

	void Awake (){
		myBody = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void Update () 
	{
		ReadInput ();
		myBody.velocity = Movement;

		if (IsPushing) {
			myBody.mass = 100f;
		} else {
			myBody.mass = 1f;
		}
	}

	void ReadInput()
	{
		//ReadIi
		float xMovement = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		float zMovement = Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
		Movement = new Vector3 (xMovement, 0f, zMovement);
		IsPushing = Input.GetButton ("Push");
		Debug.Log ("Movement vector: " + Movement);
	}
	 
}
