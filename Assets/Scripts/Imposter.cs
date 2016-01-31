using UnityEngine;
using System.Collections;

public class Imposter : MonoBehaviour {

	public Material ImposterImage;
	public Material TrueImage;


	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RevealIdentity()
	{
		ChangeMaterial (TrueImage);
	}

	public void ConcealIdentity()
	{
		ChangeMaterial (ImposterImage);
	}

	private void ChangeMaterial(Material newMaterial)
	{
		MeshRenderer[] frontAndBackRenderer = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer mr in frontAndBackRenderer) {
			mr.material = newMaterial;
		}
	}

}
