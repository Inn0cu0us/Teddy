using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	Vector3 Movement;
	bool IsPushing = false;
	bool IsUsing = false;
	private Rigidbody myBody;
	private GameManager theManager;


	public float Speed;

	void Awake (){
		myBody = GetComponent<Rigidbody> ();
		var go = GameObject.FindGameObjectWithTag ("GameController");
		theManager = go.GetComponent<GameManager> ();
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

		if (IsUsing) {
			Debug.Log ("Pressed Use");
			TryUse ();
		}
	}

	void TryUse()
	{
		RaycastHit hitInfo;
		Debug.DrawRay (this.gameObject.transform.position, transform.forward, Color.green, 1f);
		if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hitInfo, 1f))
		{
			Debug.Log ("Hit " + hitInfo.collider.gameObject);
			Candle aCandle = hitInfo.collider.gameObject.GetComponent<Candle>();
			if (aCandle != null)
			{
				aCandle.PerformUseAction();
			}
		}
	}

	void ReadInput()
	{
		float xMovement = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		float zMovement = Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
		Movement = new Vector3 (xMovement, 0f, zMovement);
		IsPushing = Input.GetButton ("Push");
		IsUsing = Input.GetButtonDown ("Use");
	//	if (Input.GetButton (
	}

	public void SubmitPuzzleSolution()
	{
		if (theManager.CheckPuzzle ()) {
			//win
		} else {
			//you die
		}
	}
	 
}
