using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool questComplete;
    public bool coins;
    public bool enemies;
    public bool alreadyChecked;

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
            portal.SetActive(true);
            questText.color = new Color(0, 1, 0);
            if (alreadyChecked == false)
            {
                alreadyChecked = true;
                unlockedText.SetActive(true);
                Invoke("HideText", 5);
            }
        }
        else
        {
            questText.color = new Color(1,1,1);
        }

        if (coins == true && enemies == true)
        {
            if (GameManager.instance.coins >= requiredCoins && GameManager.instance.enemiesKilled >= requiredEnemies)
            {
                questComplete = true;
            }
        }
        else if (coins == true && enemies != true)
        {
            if (GameManager.instance.coins >= requiredCoins)
            {
                questComplete = true;
            }
        }
        else if (enemies == true && coins != true)
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
