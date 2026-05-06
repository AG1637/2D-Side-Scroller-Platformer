using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHealth = 3;
    private float levelStartTime;
    private bool timerActive = false;
    public int coins;
    public int enemiesKilled;
    public bool canEnterNextLevel;

    public GameObject gameOverScreen;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }

    public void StartTimer()
    {
        levelStartTime = Time.time;
        timerActive = true;
    }

    public float GetLevelTime()
    {
        if (timerActive)
        {
            return Time.time - levelStartTime;
        }
        return 0f;
    }

    public void PauseTimer()
    {
        timerActive = false;
    }

    public void ResumeTimer()
    {
        timerActive = true;
    }
}

