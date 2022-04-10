using UnityEngine;

/// <summary>
/// Основной класс, управляющий игрой.
/// </summary>
public class GameManager : MonoBehaviour
{
    private Player player;

    private bool gameOver = false;

    /// <summary>
    /// Количество "тапов", необходимое чтобы скрыть обучение.
    /// </summary>
    public int TapToHideTutorial;

    /// <summary>
    /// Контроллер платформ.
    /// </summary>
    public Platforms Platforms;

    /// <summary>
    /// Игровой объект игрока.
    /// </summary>
    public Transform PlayerObj;

    /// <summary>
    /// Текст обучения.
    /// </summary>
    public TMPro.TextMeshProUGUI TutorialText;

    void Start()
    {
        player = PlayerObj.GetComponent<Player>();
    }

    void Update()
    {
        if (gameOver)
            return;

        CheckGameOver();
        CheckPlatformMovement();
        CheckTutorialHide();
    }

    private void CheckGameOver()
    {
        if (player.transform.position.y >= Platforms.YDestroyValue)
            return;

        gameOver = true;
        player.GameOver();
        GetComponent<Events>().GameOver();
    }

    private void CheckTutorialHide()
    {
        if (TapToHideTutorial <= 0 || Input.touchCount <= 0)
            return;

        for (var i = 0; i < Input.touchCount; ++i)
            if (Input.GetTouch(i).phase == TouchPhase.Began)
                --TapToHideTutorial;

        if (TapToHideTutorial <= 0)
            TutorialText.gameObject.SetActive(false);
    }

    private void CheckPlatformMovement()
    {
        var diff = player.TopY - PlayerObj.position.y;
        if (diff < 0)
        {
            player.transform.position = player.transform.position.SetY(player.TopY);
            Platforms.MovePlatforms(diff);
        }
    }
}
