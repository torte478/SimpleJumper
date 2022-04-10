using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    private GameObject[] platforms;
    private float screenHalfHeight;

    public GameObject platformPrefab;
    public Transform view;
    public Transform player;

    public float Border;
    public float yDestroyValue;
    public float yCreateValue;

    void Start()
    {
        // TODO : dispose + reasign parent
        var pool = new ObjectPool<GameObject>(
            () => Instantiate(platformPrefab),
            (obj) => obj.SetActive(true),
            (obj) => obj.SetActive(false),
            (obj) => Destroy(obj),
            false,
            10,
            10);

        const int PlatformCount = 5;
        platforms = new GameObject[PlatformCount];
        screenHalfHeight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).y;

        for (var i = 0; i < PlatformCount; ++i)
        {
            var x = i == 0 ? 0 : Random.Range(-2.5f, 2.5f);
            var y = 0.0f + i * 2.5f;

            var platform = pool.Get();
            platform.transform.position = new Vector3(x, y);
            platforms[i] = platform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > Border)
        {
            var diff = player.position.y - Border;
            
            player.position = new Vector3(player.position.x, Border);
            foreach (var platform in platforms)
            {
                var pos = platform.transform.position;
                pos.y -= diff;
                platform.transform.position = pos;

                if (platform.transform.position.y < yDestroyValue)
                {
                    platform.transform.position = new Vector3(platform.transform.position.x, yCreateValue);
                }
            }
        }
    }
}
