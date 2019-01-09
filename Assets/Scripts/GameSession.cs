using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

	[SerializeField] private int playerLives = 3;
	[SerializeField] private int playerLivesMax = 3;
	[SerializeField] private int gatheredCoins = 0;
	
	[SerializeField] private Text textCoins;
	[SerializeField] private Image heart_001;
	[SerializeField] private Image heart_010;
	[SerializeField] private Image heart_100;
	[SerializeField] private Image keyIcon;

	[SerializeField] private AudioClip lostLifeSFX;
	
	private void Awake()
	{
		int numGameSessions = FindObjectsOfType<GameSession>().Length;
		if (numGameSessions > 1) Destroy(gameObject);
		else DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start ()
	{
		textCoins.text = gatheredCoins.ToString();
		heart_001.enabled = true;
		heart_010.enabled = true;
		heart_100.enabled = true;
		keyIcon.enabled = false;
	}

	public void ProcessKeyPickup()
	{
		keyIcon.enabled = true;
		FindObjectOfType<ScenePersistance>().gatheredKey = true;
	}

	public void ResetKeyPickup()
	{
		keyIcon.enabled = false;
	}

	public void ProcessCoinPickup(int coinsToAdd)
	{
		gatheredCoins += coinsToAdd;
		textCoins.text = gatheredCoins.ToString();
	}
	
	public void ProcessPlayerDeath()
	{
		if (playerLives > 1) TakeLife();
		else ResetGameSession();
	}

	private void ResetGameSession()
	{
		FindObjectOfType<Radio>().PlayMenuMusic();
		SceneManager.LoadScene(0);
		Destroy(gameObject);
	}

	private void TakeLife()
	{
		playerLives--;
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
		if (playerLives == 2) heart_100.enabled = false;
		else if (playerLives == 1) heart_010.enabled = false;
		AudioSource.PlayClipAtPoint(lostLifeSFX, Camera.main.transform.position);
	}

	public string ProcessTrade(int neededCoins, int heartsGetting)
	{
		if (playerLives == playerLivesMax) return "fail_hearts";
		else if (gatheredCoins < neededCoins) return "fail_coins";
		else
		{
			gatheredCoins -= neededCoins;
			textCoins.text = gatheredCoins.ToString();
			playerLives += heartsGetting;
			if (playerLives == 3) heart_100.enabled = true;
			else if (playerLives == 2) heart_010.enabled = true;
			return "success";
		}
	}
}
