using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{

	[SerializeField] private AudioSource[] music;
	[SerializeField] private float musicVolumeScaleGameplay = 5.0F;
	[SerializeField] private float musicVolumeScalePassings = 8.0F;
	
	private AudioSource menuMusic;
	private AudioSource gameplayMusic;
	private AudioSource finaleMusic;
	
	private AudioSource audioSource;
	
	private void Awake()
	{
		music = GetComponents<AudioSource>();
		menuMusic = music[0];
		gameplayMusic = music[1];
		finaleMusic = music[2];
		menuMusic.volume = musicVolumeScalePassings;
//		audioSource.volume = musicVolumeScalePassings;
		menuMusic.Play();
//		audioSource.Play(menuMusic);	
		Singleton();	
	}

	private void Singleton() 
	{
		int numRadio = FindObjectsOfType<Radio>().Length;
		if(numRadio > 1) Destroy(gameObject);
		else DontDestroyOnLoad(transform.gameObject);
	}
 
	public void PlayMenuMusic()
	{
		gameplayMusic.Stop();
		finaleMusic.Stop();
//		audioSource.Stop();
//		audioSource.PlayOneShot(menuMusic, musicVolumeScalePassings);
		menuMusic.Play();
	}

	public void PlayGameplayMusic()
	{
		menuMusic.Stop();
//		audioSource.Stop();
//		audioSource.PlayOneShot(gameplayMusic, musicVolumeScaleGameplay);
		gameplayMusic.Play();
	}

	public void PlayFinaleMusic()
	{
		gameplayMusic.Stop();
//		audioSource.Stop();
//		audioSource.PlayOneShot(finaleMusic, musicVolumeScalePassings);
		finaleMusic.Play();
	}
 
	public void StopMusic()
	{
		menuMusic.Stop();
		gameplayMusic.Stop();
		finaleMusic.Stop();
//		audioSource.Stop();
	}
}
