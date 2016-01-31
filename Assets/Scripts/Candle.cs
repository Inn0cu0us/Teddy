using UnityEngine;
using System.Collections;

public class Candle : Usable {

	public Color CandleColor;
    public float SecondsOfCandlelight;
	public float SecondsBetweenFlicker;
	public float FlickerRange;
	public Imposter RevealedObject;

	private float OriginalIntensity;
	private Light Flame;
	private ParticleSystem CandleParticles;
    private bool isLit = false;
    private float timer = 0f;
	private float flickerTimer = 0f;

	private Light GlobalLight;
	private Color GlobalLightStartingColor;

	void Awake ()
	{
		CandleParticles = GetComponentInChildren<ParticleSystem> ();
		Flame = GetComponentInChildren<Light> ();
		var go = GameObject.FindGameObjectWithTag ("GlobalLight");
		GlobalLight = go.GetComponent<Light> ();
	}

	void Start () 
	{
        Flame.color = CandleColor;
		OriginalIntensity = Flame.intensity;
		Flame.intensity = 0f;
		GlobalLightStartingColor = GlobalLight.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLit)
        {
			RevealedObject.RevealIdentity();
			flickerTimer += Time.deltaTime;
			if (flickerTimer >= SecondsBetweenFlicker)
			{
				flickerTimer = 0f;
				Flame.intensity = Random.Range(OriginalIntensity - FlickerRange, OriginalIntensity + FlickerRange);

			}
            timer += Time.deltaTime;
            if (timer >= SecondsOfCandlelight)
            {
				Extinguish();
                isLit = false;
				timer = 0f;
            }
        }
	}

    public override void PerformUseAction()
    {
		Debug.Log ("Candle on");
		Light ();
		CandleParticles.Play ();
        isLit = true;
    }

	private void Light()
	{
		GlobalLight.color = CandleColor;
		//creepy noise;
	}

	private void Extinguish()
	{
		//play candle off sshhhh
		RevealedObject.ConcealIdentity ();
		Debug.Log("Candle extinguished");
		CandleParticles.Stop ();
		GlobalLight.color = GlobalLightStartingColor;
		Flame.intensity = 0f;
	}
}
