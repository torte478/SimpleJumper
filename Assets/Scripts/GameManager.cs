using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;

    private bool gameOver = false;

    public Platforms Platforms;
    public Transform PlayerObj;

    public float Border;

    void Start()
    {
        player = PlayerObj.GetComponent<Player>();
    }

    void Update()
    {
        if (gameOver)
            return;

        if (player.transform.position.y < Platforms.yDestroyValue)
        {
            gameOver = true;
            player.GameOver();
            GetComponent<Events>().GameOver();
        }

        var diff = Border - PlayerObj.position.y;
        if (diff < 0)
        {
            player.transform.position = player.transform.position.SetY(Border);
            Platforms.MovePlatforms(diff);
        }
    }
}
