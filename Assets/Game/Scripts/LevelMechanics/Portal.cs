using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private AudioClip levelCompleteSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CompleteLevel());
        }
    }

    IEnumerator CompleteLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel + 1;

        if (nextLevel > SessionProgress.unlockedLevel)
        {
            SessionProgress.unlockedLevel = nextLevel;
        }

        SoundManager.instance.PlaySound(levelCompleteSound);
        yield return new WaitForSeconds(2);

        if (nextLevel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            //No more levels to load so returns to main menu
            SceneManager.LoadScene(0);
        }
    }

}
