using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Button PauseButton;
    public Button ResumeButton;
    public Canvas MenuCanvas;
    public TMPro.TextMeshProUGUI MenuText;

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    public void Pause()
    {
        ResumeButton.gameObject.SetActive(true);
        OpenMenu("PAUSE");
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseButton.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
    }

    public void GameOver()
    {
        ResumeButton.gameObject.SetActive(false);
        OpenMenu("GAME OVER");
    }

    private void OpenMenu(string label)
    {
        Time.timeScale = 0.0f;
        PauseButton.gameObject.SetActive(false);
        MenuText.text = label;
        MenuCanvas.gameObject.SetActive(true);
    }
}
