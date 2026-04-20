using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int playerHealth;
    public Image heart1, heart2, heart3;

    private void Update()
    {
        if(GameManager.instance.playerHealth == 3)
        {
            heart1.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
            heart2.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
            heart3.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
        }
        else if (GameManager.instance.playerHealth == 2)
        {
            heart1.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
            heart2.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
            heart3.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
        }
        else if (GameManager.instance.playerHealth == 1)
        {
            heart1.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 255);
            heart2.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
            heart3.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
        }
        else if (GameManager.instance.playerHealth <= 0)
        {
            heart1.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
            heart2.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
            heart3.GetComponent<Image>().color = new Color(heart1.color.r, heart1.color.g, heart1.color.b, 0);
        }
    }
}
