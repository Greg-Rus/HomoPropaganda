using UnityEngine;
using System.Collections;

public class ParticleSystemPlayer : MonoBehaviour {
	public ParticleSystem[] particleSystems;
	// Use this for initialization
	public void Play()
	{
		for(int i = 0; i < particleSystems.Length; i++)
		{
			particleSystems[i].Play();
		}
	}
}
