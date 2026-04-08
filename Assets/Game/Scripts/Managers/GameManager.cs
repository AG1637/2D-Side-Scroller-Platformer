using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins;
    public int playerHealth = 3;
    public int enemiesKilled;
    public bool canEnterNextLevel;

    public GameObject gameOverScreen;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI enemyText;

    void Start()
    {
        instance = this;
        Time.timeScale = 1;
    }

    void Update()
    {
        coinText.text = "Coins: " + coins;
        enemyText.text = "Enemies Killed: " + enemiesKilled;
        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }

    }
}
