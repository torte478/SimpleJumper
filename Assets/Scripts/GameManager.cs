using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;

    private bool gameOver = false;

    public Platforms Platforms;
    public Transform PlayerObj;

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
    }
}
