using UnityEngine;
using System.Collections;

public class Candle : Usable {

	public Color CandleColor;
    public float SecondsOfCandlelight;
	public float SecondsBetweenFlicker;
	public float FlickerRange;

	private float OriginalIntensity;
	private Light Flame;
    private bool isLit = false;
    private float timer = 0f;
	private float flickerTimer = 0f;

	private Light GlobalLight;
	private Color GlobalLightStartingColor;

	void Start () 
	{

		Flame = GetComponentInChildren<Light> ();
        Flame.color = CandleColor;
		OriginalIntensity = Flame.intensity;
		Flame.intensity = 0f;
		var go = GameObject.FindGameObjectWithTag ("GlobalLight");
		GlobalLight = go.GetComponent<Light> ();
		GlobalLightStartingColor = GlobalLight.color;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLit)
        {
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
		Debug.Log("Candle extinguished");
		GlobalLight.color = GlobalLightStartingColor;
		Flame.intensity = 0f;
	}
}
