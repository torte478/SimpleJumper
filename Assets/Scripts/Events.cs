using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Button PauseButton;
    public Canvas MenuCanvas;

    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("SampleScene");
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
}
