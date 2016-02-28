using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {
	public AudioSource musicPlayer;
	public AudioClip standardMusic;
	public AudioClip bossFightMusic;
	// Use this for initialization


	public void PlayBossMusic()
	{
		musicPlayer.clip = bossFightMusic;
		musicPlayer.loop  = true;
		musicPlayer.Play ();

	}

	public void PlayStandardMusic()
	{
		musicPlayer.clip = standardMusic;
		musicPlayer.loop  = true;
		musicPlayer.Play ();
	}

	public void SetMusicVolume(float volume)
	{
		musicPlayer.volume = volume;
	}
}
