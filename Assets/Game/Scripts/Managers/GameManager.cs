using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins;
    public int enemiesKilled;
    public bool canEnterNextLevel;

    public TextMeshProUGUI coinText;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        coinText.text = "Coins: " + coins;
        /*if (canEnterNextLevel == true)
        {
            Debug.Log("next level");
        }*/
    }
}
