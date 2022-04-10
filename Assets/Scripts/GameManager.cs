using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;

    private bool gameOver = false;

    public int TouchToHideTutorial;

    public Platforms Platforms;
    public Transform PlayerObj;
    public TMPro.TextMeshProUGUI TutorialText;

    void Start()
    {
        player = PlayerObj.GetComponent<Player>();
    }

    void Update()
    {
        if (gameOver)
            return;

        if (player.transform.position.y < Platforms.YDestroyValue)
        {
            gameOver = true;
            player.GameOver();
            GetComponent<Events>().GameOver();
        }

        var diff = player.TopY - PlayerObj.position.y;
        if (diff < 0)
        {
            player.transform.position = player.transform.position.SetY(player.TopY);
            Platforms.MovePlatforms(diff);
        }

        if (TouchToHideTutorial > 0 && Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; ++i)
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                    --TouchToHideTutorial;

            if (TouchToHideTutorial < 0)
                TutorialText.gameObject.SetActive(false);
        }
    }
}
