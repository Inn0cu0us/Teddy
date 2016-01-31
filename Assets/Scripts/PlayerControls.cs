using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	Vector3 Movement;
    bool pushPressed = false;
    bool pushReleased = false;
	bool IsUsing = false;
	private Rigidbody myBody;
	private GameManager theManager;
    private Rigidbody objectPushed;
    Vector3 lastMovement;
    Animator anim;
    
	public float Speed;

	void Awake (){
		myBody = GetComponent<Rigidbody> ();
		var go = GameObject.FindGameObjectWithTag ("GameController");
		theManager = go.GetComponent<GameManager> ();
        anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () 
	{
	
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		ReadInput ();
		myBody.velocity = Movement;
        Animating(Movement.x, Movement.z);
        if(objectPushed != null)
        {
            objectPushed.velocity = Movement;
        }


		if (pushPressed)
        {
            TryPush();
        }
        else if (pushReleased)
        {
            objectPushed = null;
        }

		if (IsUsing) {
			Debug.Log ("Pressed Use");
			TryUse ();
		}
	}

	void TryUse()
	{
		RaycastHit hitInfo;
        Debug.DrawRay(this.gameObject.transform.position, lastMovement.normalized, Color.red, 3f);
        if (Physics.Raycast(this.gameObject.transform.position, lastMovement.normalized, out hitInfo, 3f))
		{
			Debug.Log ("Hit " + hitInfo.collider.gameObject);
			Candle aCandle = hitInfo.collider.gameObject.GetComponent<Candle>();
			if (aCandle != null)
			{
				aCandle.PerformUseAction();
			}
		}
	}

    void TryPush()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(this.gameObject.transform.position, lastMovement.normalized, Color.red, 3f);
        if (Physics.Raycast(this.gameObject.transform.position, lastMovement.normalized, out hitInfo, 3f))
        {
            Debug.Log("Hit " + hitInfo.collider.gameObject);
            if(hitInfo.collider.gameObject.tag.Equals("Pushable")) 
            {
                objectPushed = hitInfo.collider.gameObject.GetComponent<Rigidbody>();  
            }
        }
    }

	void ReadInput()
	{
		float xMovement = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
		float zMovement = Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
		Movement = new Vector3 (xMovement, 0f, zMovement);
        if(xMovement != 0 || zMovement != 0)
        {
            lastMovement = Movement;
        }
		pushPressed = Input.GetButtonDown ("Push");
        pushReleased = Input.GetButtonUp("Push");
		IsUsing = Input.GetButtonDown ("Use");
	//	if (Input.GetButton (
	}

	public void SubmitPuzzleSolution()
	{
		if (theManager.CheckPuzzle ()) {
			Debug.Log ("You win!!!");//win
		} else {
			Debug.Log ("You die!");
		}
	}

    void Animating(float h, float v)
    {
        if (!(h == 0 && v == 0))
        {
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                //horizontal
                if (h > 0)
                {
                    Debug.Log("MovingRight!");

                    anim.SetTrigger("MovingRight");
                }
                else
                {
                    Debug.Log("MovingLeft!");
                    anim.SetTrigger("MovingLeft");
                }
            }
            else
            {
                //vertical
                if (v > 0)
                {
                    Debug.Log("MovingUp!");
                    anim.SetTrigger("MovingUp");
                }
                else
                {
                    Debug.Log("MovingDown!");
                    anim.SetTrigger("MovingDown");
                }
            }
        }
    }
	 
}
