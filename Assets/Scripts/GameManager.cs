using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;

    private Player player;

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
