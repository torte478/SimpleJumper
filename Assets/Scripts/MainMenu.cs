using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Класс-обработчик событий главного меню.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Запускает игру.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Выходит из игры.
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
