using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
	
	/// For Source array and volume array:
	///  0 = Background Music
	///  1 = Background Noises
	///  2 = SoundEffects


	public AudioClip BackgroundMusic;
	public AudioClip[] BackgroundNoises;
	
	/// Noises List
	/// 0 = Creaking House
	/// 1 = ticking
	/// 2 = Water Drip

	public AudioClip[] Effects;
	
	/// Effects List
	/// 0 = Deamon Laugh
	/// 1 = Happy Teddy
	/// 2 = Match Lighting
	/// 3 = Mistake was Made / Messed up
	/// 4 = Object Move
	/// 5 = Candle Flame

	public float[] volumes;

	private AudioSource[] AudioComponents;

	public AudioSource SelectedSoundEffect;
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
		SelectedBackgroundMusic.volume = volumes [0];
		SelectedBackgroundMusic.Play ();
		SelectedBackgroundMusic.loop = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (SelectedSoundEffect.clip != null){
			PlaySoundEffect ();
		}
		if (!SelectedBackgroundNoise.isPlaying) {
			SelectBackgroundNoises();
			PlayBackgroundNoises();
		}
	}

	//Play Seleced sound
	public void PlaySoundEffect()
	{
		SelectedSoundEffect.volume = volumes [2];
		if (!SelectedSoundEffect.isPlaying)
			SelectedSoundEffect.Play ();
	}
	public void StopSoundEffect()
	{
		SelectedSoundEffect.Stop ();
		SelectedSoundEffect.clip = null;
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
