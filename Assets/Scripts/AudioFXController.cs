using UnityEngine;
using System.Collections;

public class AudioFXController : MonoBehaviour {

	public AudioClip bounce1;
	public AudioClip bounce2;
	public AudioClip bounce3;
	
	public AudioClip hit1;
	public AudioClip hit2;
	
	public AudioSource audio;
	public float volume = 1f;
	
	public static AudioFXController instance;
	// Use this for initialization
	void Awake()
	{
		instance = this;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	public void playBounceSound()
	{
		switch (Random.Range(1,4))
		{
		case 1: audio.PlayOneShot(bounce1); break;
		case 2: audio.PlayOneShot(bounce2); break;
		case 3: audio.PlayOneShot(bounce3); break;
		default: audio.PlayOneShot(bounce1); break;
			
		}
	}
	
	public void playHitSound()
	{
		switch (Random.Range(1,3))
		{
		case 1: audio.PlayOneShot(hit1); break;
		case 2: audio.PlayOneShot(hit2); break;
		default: audio.PlayOneShot(hit1); break;
		}
	}

	public void SetFXVolume()
	{
		audio.volume = volume;
	}

	public void SetFXVolumeByValue(float volume)
	{
		audio.volume = volume;
	}
}
