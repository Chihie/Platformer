using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistance : MonoBehaviour
{
	[SerializeField] public bool gatheredKey = false;
	
	private int startingSceneIndex;
	
	private void Awake()
	{
		int numScenePersistance = FindObjectsOfType<ScenePersistance>().Length;
		if (numScenePersistance > 1) Destroy(gameObject);
		else DontDestroyOnLoad(gameObject);
	}
	
	// Use this for initialization
	void Start ()
	{
		startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
		FindObjectOfType<GameSession>().ResetKeyPickup();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		if (currentSceneIndex != startingSceneIndex)
			Destroy(gameObject);
	}
}
