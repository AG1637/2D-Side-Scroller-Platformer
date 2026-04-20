using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool questComplete;
    public bool coins;
    public bool enemies;

    public int requiredCoins;
    public int requiredEnemies;

    public GameObject portal;
    public TextMeshProUGUI questText;
    public GameObject unlockedText;
    public GameObject lockedText;

    private void Start()
    {
        portal.SetActive(false);
    }
    private void Update()
    {
        UpdateQuestText();
        if(questComplete == true)
        {
            GameManager.instance.canEnterNextLevel = true;
            unlockedText.SetActive(true);
            portal.SetActive(true);
            questText.color = new Color(117, 255, 76, 255);
        }

        if (coins == true)
        {
            if(GameManager.instance.coins >= requiredCoins)
            {
                questComplete = true;
            }        
        }
        else if (enemies == true)
        {
            if (GameManager.instance.enemiesKilled >= requiredEnemies)
            {
                questComplete = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            if (coins == true)
            {
                if (GameManager.instance.coins < requiredCoins)
                {
                    lockedText.SetActive(true);
                    Invoke("HideText", 5);
                }
            }
            else if (enemies == true)
            {
                if (GameManager.instance.enemiesKilled < requiredEnemies)
                {
                    lockedText.SetActive(true);
                    Invoke("HideText", 5);
                }
            }
        }
    }

    private void HideText()
    {
        unlockedText.SetActive(false);
        lockedText.SetActive(false);
    }

    private void UpdateQuestText()
    {
        if (requiredCoins > 0 && requiredEnemies > 0)
        {
            //quest for enemies and coins
            questText.text = "Coins: " + GameManager.instance.coins + "/" + requiredCoins + "\n" + "Enemies: " + GameManager.instance.enemiesKilled + "/" + requiredEnemies;
        }
        else if (requiredCoins > 0 && requiredEnemies <= 0)
        {
            //quest is just coins
            questText.text = "Coins: " + GameManager.instance.coins + "/" + requiredCoins;
        }
        else if (requiredCoins <= 0 && requiredEnemies > 0)
        {
            //quest is just enemies
            questText.text = "Enemies: " + GameManager.instance.enemiesKilled + "/" + requiredEnemies;
        }
    }
}
