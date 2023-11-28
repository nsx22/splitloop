using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public void LoadLastSavedLevel()
    {
        StartCoroutine(LoadLevel(PlayerPrefs.GetInt("LastSave", 1)));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevelIndex(int levelIndex)
    {
        if(SceneManager.GetActiveScene().buildIndex == 6)
        {
            PlayerPrefs.SetInt("LastSave", 1);
        }
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
