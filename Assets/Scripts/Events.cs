using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Класс-обработчик базовых событий игры.
/// </summary>
public class Events : MonoBehaviour
{
    /// <summary>
    /// Кнопка "Пауза".
    /// </summary>
    public Button PauseButton;

    /// <summary>
    /// Кнопка "Продолжить".
    /// </summary>
    public Button ResumeButton;

    /// <summary>
    /// Canvas, содержащий меню игры.
    /// </summary>
    public Canvas MenuCanvas;

    /// <summary>
    /// Заголовок меню.
    /// </summary>
    public TextMeshProUGUI MenuText;

    /// <summary>
    /// Перезапускает уровень.
    /// </summary>
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Ставит уровень на паузу.
    /// </summary>
    public void Pause()
    {
        ResumeButton.gameObject.SetActive(true);
        OpenMenu("PAUSE");
    }

    /// <summary>
    /// Запускает уровень после паузы.
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseButton.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Переходит в главное меню.
    /// </summary>
    public void ToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MenuScene");
    }

    /// <summary>
    /// Завершает игру.
    /// </summary>
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
