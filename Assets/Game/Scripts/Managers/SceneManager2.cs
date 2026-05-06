using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
{
    public static SceneManager2 instance;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject helpPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject levelSelectPanel;
    private int index;

    private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        CameraZoom.instance.panelOpen = true;
        GameManager.instance.PauseTimer();
        Time.timeScale = 0;
    }

    public void Help()
    {
        helpPanel.SetActive(true); 
        CameraZoom.instance.panelOpen = true;
        GameManager.instance.PauseTimer();
        Time.timeScale = 0;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        CameraZoom.instance.panelOpen = false;
        GameManager.instance.ResumeTimer();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.ExitPlaymode();
    }

    public void ClosePanels()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
            CameraZoom.instance.panelOpen = false;
            Time.timeScale = 1;
        }
        if (levelSelectPanel != null)
        {
            levelSelectPanel.SetActive(false);
        }
    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(true);
    }
    public void LoadNextLevel()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }

}
