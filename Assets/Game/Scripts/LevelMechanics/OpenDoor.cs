using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool coins;
    public bool enemies;
    public int requiredCoins;
    public int requiredEnemies;

    public GameObject portal;
    public GameObject unlockedText;
    public GameObject lockedText;

    private void Start()
    {
        portal.SetActive(false);
    }
    private void Update()
    {
        if (coins == true)
        {
            if(GameManager.instance.coins >= requiredCoins)
            {
                GameManager.instance.canEnterNextLevel = true;
                unlockedText.SetActive(true);
                portal.SetActive(true);
            }        
        }
        else if (enemies == true)
        {
            if (GameManager.instance.enemiesKilled >= requiredEnemies)
            {
                GameManager.instance.canEnterNextLevel = true;
                unlockedText.SetActive(true);
                portal.SetActive(true);
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

}
