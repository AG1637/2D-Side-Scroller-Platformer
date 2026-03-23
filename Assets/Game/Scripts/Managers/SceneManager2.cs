using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
{
    public static SceneManager2 instance;
    [SerializeField] GameObject pauseMenu;
    private int index;

    private void Start()
    {
        instance = this;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
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
        pauseMenu.SetActive(false);
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
