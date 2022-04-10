using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button PauseButton;
    public Canvas MenuCanvas;

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        PauseButton.gameObject.SetActive(false);
        MenuCanvas.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseButton.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
