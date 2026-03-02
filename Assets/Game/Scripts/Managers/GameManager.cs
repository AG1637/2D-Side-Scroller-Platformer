using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coins;
    public TextMeshProUGUI coinText;
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        coinText.text = "Coins: " + coins;
    }
}
