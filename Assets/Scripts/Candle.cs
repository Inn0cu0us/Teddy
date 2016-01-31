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
	private Candle[] allCandles;

	private Audio Sound;

	void Awake ()
	{
		CandleParticles = GetComponentInChildren<ParticleSystem> ();
		Flame = GetComponentInChildren<Light> ();
		var go = GameObject.FindGameObjectWithTag ("GlobalLight");
		GlobalLight = go.GetComponent<Light> ();
		InitAllCandles ();

		var so = GameObject.FindGameObjectWithTag ("SoundPlayer");
		Sound = so.GetComponent<Audio> ();
	}

	private void InitAllCandles()
	{
		GameObject[] allCandlesGo = GameObject.FindGameObjectsWithTag ("Candle");
		allCandles = new Candle[allCandlesGo.Length];
		for (int i=0; i<=allCandles.Length-1; i++) {
			allCandles[i] = allCandlesGo[i].GetComponent<Candle>();
		}
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
				Sound.effectCase = 6;

				Sound.ChangeSoundEffectPlaying();
				Sound.PlaySoundEffect ();
					


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
		ExtinguishAllCandles ();
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
		isLit = false;
		Sound.StopSoundEffect ();


	}

	private void ExtinguishAllCandles()
	{
		foreach (Candle c in allCandles) {
			c.Extinguish ();
		}
	}
}
