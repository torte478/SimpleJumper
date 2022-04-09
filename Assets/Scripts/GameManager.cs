using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    void Start()
    {
        // TODO : dispose
        var platforms = new ObjectPool<GameObject>(
            () => Instantiate(platformPrefab),
            (obj) => obj.SetActive(true),
            (obj) => obj.SetActive(false),
            (obj) => Destroy(obj),
            false,
            10,
            10);

        for (var i = 0; i < 5; ++i)
        {
            var x = Random.Range(-2.5f, 2.5f);
            var y = -4.0f + i;

            var platform = platforms.Get();
            platform.transform.position = new Vector3(x, y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
