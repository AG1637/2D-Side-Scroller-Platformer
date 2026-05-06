using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        float time = GameManager.instance.GetLevelTime();
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
