using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    public bool coins;
    public bool enemies;
    public int requiredCoins;
    public int requiredEnemies;

    public GameObject lockedTextCoins;
    public GameObject lockedTextEnemies;
    public GameObject unlockedTextCoins;
    public GameObject unlockedTextEnemies;

    private void Update()
    {
        if (coins == true)
        {
            if(GameManager.instance.coins >= requiredCoins)
            {
                GameManager.instance.canEnterNextLevel = true;
                unlockedTextCoins.SetActive(true);
                //unlock portal
            }        
        }
        else if (enemies == true)
        {
            if (GameManager.instance.enemiesKilled >= requiredEnemies)
            {
                GameManager.instance.canEnterNextLevel = true;
                unlockedTextEnemies.SetActive(true);
                //unlock portal
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (coins == true)
        {
            if (GameManager.instance.coins < requiredCoins)
            {
                lockedTextCoins.SetActive(true);
                Invoke("HideText", 5);
            }
        }
        else if (enemies == true)
        {
            if (GameManager.instance.enemiesKilled < requiredEnemies)
            {
                lockedTextEnemies.SetActive(true);
                Invoke("HideText", 5);
            }
        }
    }

    private void HideText()
    {
        lockedTextCoins.SetActive(false);
        lockedTextEnemies.SetActive(false);
        unlockedTextCoins.SetActive(false);
        unlockedTextEnemies.SetActive(false);
    }

}
