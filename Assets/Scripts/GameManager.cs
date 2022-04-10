using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    private GameObject[] platforms;
    private float screenHalfHeight;

    public GameObject platformPrefab;
    public Transform view;

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
            var x = 0; // Random.Range(-2.5f, 2.5f);
            var y = -2.0f + i * 2.5f;

            var platform = pool.Get();
            platform.transform.position = new Vector3(x, y);
            platforms[i] = platform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var yDestroyValue = view.position.y - (screenHalfHeight + 1.0f);
        var yCreateValue = view.position.y + (screenHalfHeight + 1.0f);

        foreach (var platform in platforms)
        {
            if (platform.transform.position.y < yDestroyValue)
            {
                platform.transform.position = new Vector3(platform.transform.position.x, yCreateValue);
            }
        }
    }
}
