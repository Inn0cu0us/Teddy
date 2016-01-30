using UnityEngine;
using System.Collections;

public class candel : Usable {

	public Light Flame;
	public Color CandleColor;
    public float SecondsOfCandlelight;

    private bool isLit = false;
    private float timer = 0f;
	// Use this for initialization
	void Start () {
		Flame = GetComponentInChildren<Light> ();
        Flame.color = CandleColor;
    }
	
	// Update is called once per frame
	void Update () {
        if (isLit)
        {
            Flame.intensity = 10;
            timer += Time.deltaTime;
            if (timer >= SecondsOfCandlelight)
            {
                Flame.intensity = 0f;
                isLit = false;
            }
        }
	}

    public override void PerformUseAction()
    {
        isLit = true;
    }
}
