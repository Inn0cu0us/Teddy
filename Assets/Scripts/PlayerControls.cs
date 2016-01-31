using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
	public Text EndgameText;
	public Text EndgameFlavour;
	private string winString = "Ritual successful!";
	private string winFlavour = "Oh brave soul, stumbling ever on-wards. The ritual completed, oblivion's tender embrace thwarted for yet another night. And yet, to what avail? Annihilation can never be evaded long.";
	private string loseString = "Ritual failed!";
	private string loseFlavour = "Sweet, terrible sorrow. Despair's crushing embrace welcomes you with rending ferocity. Wagering a soul to eldritch ritual is a fools bet.";


	public float SecondsBeforeRestart;
	private bool restartLevel = false;
	private float restartTimer = 0f;

	public float Speed;

	private Audio Sound;

	void Awake (){

		myBody = GetComponent<Rigidbody> ();
		var go = GameObject.FindGameObjectWithTag ("GameController");
		theManager = go.GetComponent<GameManager> ();
        anim = GetComponent<Animator>();

		var so = GameObject.FindGameObjectWithTag ("SoundPlayer");
		Sound = so.GetComponent<Audio> ();
	}

	// Use this for initialization
	void Start () 
	{
		EndgameText.enabled = false;
		EndgameFlavour.enabled = false;
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
			Sound.effectCase = 4;
        }


		if (pushPressed)
        {
            TryPush();
        }
        else if (pushReleased)
        {
            objectPushed = null;
			Sound.StopSoundEffect ();
        }

		if (IsUsing) {
			Debug.Log ("Pressed Use");
			TryUse ();
		}
	}

	void Update()
	{
		if (restartLevel) {
			restartTimer += Time.deltaTime;
			if (restartTimer >= SecondsBeforeRestart)
			{
				Application.LoadLevel("main");
			}
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

				Sound.effectCase = 5;
				Sound.ChangeSoundEffectPlaying();
				Sound.PlaySoundEffect ();
            }
        }
    }

	void ReadInput()
	{
		if (!restartLevel) {
			float xMovement = Input.GetAxis ("Horizontal") * Speed * Time.deltaTime;
			float zMovement = Input.GetAxis ("Vertical") * Speed * Time.deltaTime;
			Movement = new Vector3 (xMovement, 0f, zMovement);
			if (xMovement != 0 || zMovement != 0) {
				lastMovement = Movement;
			}
			pushPressed = Input.GetButtonDown ("Push");
			pushReleased = Input.GetButtonUp ("Push");
			IsUsing = Input.GetButtonDown ("Use");
		}
		if (Input.GetButton ("Quit"))
		{
			Application.Quit();
		}
	
	}

	public void SubmitPuzzleSolution()
	{
		EndgameText.enabled = true;
		EndgameFlavour.enabled = true;
		if (theManager.CheckPuzzle ()) {
			EndgameText.text = winString;
			EndgameFlavour.text = winFlavour;
		} else {
			EndgameText.text = loseString;
			EndgameFlavour.text = loseFlavour;
		}
		restartLevel = true;
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