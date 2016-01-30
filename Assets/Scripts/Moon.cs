using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {

    public float SecondsBetweenPhases;
    public Sprite[] PhaseSprites;
    
    private float timer;
    private SpriteRenderer myRenderer;
    private int phaseIndex = 0;

    void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer > SecondsBetweenPhases)
        {
            timer = 0f;
            phaseIndex++;
            if (phaseIndex >= PhaseSprites.Length)
            {
                phaseIndex = 0;
            }
            myRenderer.sprite = PhaseSprites[phaseIndex];
        }
	}
}
