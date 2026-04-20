using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
{
    public static SceneManager2 instance;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject helpPanel;
    private int index;

    private void Awake()
    {
        instance = this;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Help()
    {
        helpPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        helpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.ExitPlaymode();
    }

    public void LoadNextLevel()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
}
