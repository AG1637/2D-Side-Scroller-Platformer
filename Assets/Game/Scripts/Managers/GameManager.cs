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

    void Start()
    {
        instance = this;
        Time.timeScale = 1;
    }

    void Update()
    {
        coinText.text = "Coins: " + coins;
        if(playerHealth <= 0)
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
        /*if (canEnterNextLevel == true)
        {
            Debug.Log("next level");
        }*/
    }
}
