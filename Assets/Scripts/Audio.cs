using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {

	public AudioClip BackgroundMusic;
	public AudioClip[] BackgroundNoises;
	public AudioClip[] Effects;

	public float[] volumes;

	private AudioSource[] AudioComponents;

	private AudioSource SelectedSoundEffect;
	private AudioSource SelectedBackgroundNoise;
	private AudioSource SelectedBackgroundMusic;

	public int effectCase;


	void Awake() {

		AudioComponents = GetComponents<AudioSource>();
		SelectedBackgroundMusic = AudioComponents [0];
		SelectedBackgroundNoise = AudioComponents [1];
		SelectedSoundEffect = AudioComponents [2];

		effectCase = 0;

	}

	// Use this for initialization
	void Start () {
	
		SelectedBackgroundMusic.clip = BackgroundMusic;
		SelectedBackgroundMusic.Play ();
		SelectedBackgroundMusic.loop = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (SelectedSoundEffect.clip != null){
			PlaySoundEffect ();
		}
		if (!SelectedBackgroundNoise.isPlaying) {
			PlayBackgroundNoises();
		}
	}

	//Play Seleced sound
	private void PlaySoundEffect()
	{
		SelectedSoundEffect.Play ();
	}

	public void ChangeSoundEffectPlaying(){

		switch (effectCase) {
		case 1:
			SelectedSoundEffect.clip = Effects[0];
			break;
		case 2:
			SelectedSoundEffect.clip = Effects[1];
			break;
		case 3:
			SelectedSoundEffect.clip = Effects[2];
			break;
		case 4:
			SelectedSoundEffect.clip = Effects[3];
			break;
		case 5:
			SelectedSoundEffect.clip = Effects[4];
			break;
		case 6:
			SelectedSoundEffect.clip = Effects[5];
			break;
		default:
			SelectedSoundEffect.clip = null;
			break;
		}
	}

	//play background music 
	private void PlayBackgroundMusic ()
	{
		SelectedBackgroundMusic.Play();
	}

	public void StopBackgroundMusic ()
	{
		SelectedBackgroundMusic.Stop();
	}

	//play background noises
	private void SelectBackgroundNoises () 
	{
		int RandomBackgroundNoise = Random.Range (0, BackgroundNoises.Length);

		switch (RandomBackgroundNoise) {
		case 1:
			SelectedBackgroundNoise.clip = BackgroundNoises[0];
			break;
		case 2:
			SelectedBackgroundNoise.clip = BackgroundNoises[1];
			break;
		case 3:
			SelectedBackgroundNoise.clip = BackgroundNoises[2];
			break;
		default:
			SelectedBackgroundNoise.clip = null;
			break;
		}
	}

	private void PlayBackgroundNoises ()
	{
			SelectedBackgroundNoise.Play ();
	}
}
