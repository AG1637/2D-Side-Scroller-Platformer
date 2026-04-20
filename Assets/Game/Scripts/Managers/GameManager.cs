using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerHealth = 3;
    public int coins;
    public int enemiesKilled;
    public bool canEnterNextLevel;

    public GameObject gameOverScreen;

    void Start()
    {
        instance = this;
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
}
