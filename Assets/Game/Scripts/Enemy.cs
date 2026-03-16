using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.instance.playerHealth = GameManager.instance.playerHealth - 1;
            Debug.Log(GameManager.instance.playerHealth);
        }
    }
}
