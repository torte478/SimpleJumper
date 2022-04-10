using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public Platforms platforms;
    public Transform player;

    public float Border;

    void Start()
    {
        
    }

    void Update()
    {
        var diff = Border - player.position.y;
        if (diff < 0)
        {
            player.position = player.position.SetY(Border);
            platforms.MovePlatforms(diff);
        }
    }
}
