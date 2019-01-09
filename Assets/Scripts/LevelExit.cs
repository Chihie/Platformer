using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{

    [SerializeField] private float LevelLoadDelay = 2f;
    [SerializeField] private float LevelExitSlowMoFactor = 0.2f;
    [SerializeField] private AudioClip keyholeSFX;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(FindObjectOfType<ScenePersistance>().gatheredKey)
            StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        AudioSource.PlayClipAtPoint(keyholeSFX, Camera.main.transform.position);
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;
        
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex + 1 == 7) //TODO: Compare to the last scene index in build
            FindObjectOfType<Radio>().PlayFinaleMusic();
        Destroy(FindObjectOfType<ScenePersistance>());
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
