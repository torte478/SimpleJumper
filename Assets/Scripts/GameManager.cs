using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    void Start()
    {
        // TODO : dispose + reasign parent
        var platforms = new ObjectPool<GameObject>(
            () => Instantiate(platformPrefab),
            (obj) => obj.SetActive(true),
            (obj) => obj.SetActive(false),
            (obj) => Destroy(obj),
            false,
            10,
            10);

        //for (var i = 0; i < 5; ++i)
        //{
        //    var x = Random.Range(-2.5f, 2.5f);
        //    var y = -4.0f + i * 2;

        //    var platform = platforms.Get();
        //    platform.transform.position = new Vector3(x, y);
        //}

        var plarform = platforms.Get();
        plarform.transform.position = new Vector3(0, -2);

        var plarform2 = platforms.Get();
        plarform2.transform.position = new Vector3(0, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
