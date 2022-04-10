using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlatfromPool : MonoBehaviour
{
    private ObjectPool<GameObject> staticPlatformPool;
    private ObjectPool<GameObject> movingPlatformPool;

    public GameObject StaticPlatformPrefab;
    public GameObject MovingPlatformPrefab;

    void Awake()
    {
        staticPlatformPool = CreateObjectPool(StaticPlatformPrefab);
        movingPlatformPool = CreateObjectPool(MovingPlatformPrefab);
    }

    public BasePlatform Create(Vector3 position, PlatfromType type)
    {
        var pool = GetObjectPool(type);

        var platform = pool.Get();
        platform.transform.position = position;
        return platform.GetComponent<BasePlatform>();
    }

    public void Remove(BasePlatform platform)
    {
        var platformType = platform.GetComponent<BasePlatform>().PlatfromType;
        var pool = GetObjectPool(platformType);
        pool.Release(platform.gameObject);
    }

    private ObjectPool<GameObject> GetObjectPool(PlatfromType type)
    {
        return type switch
        {
            PlatfromType.Static => staticPlatformPool,
            PlatfromType.Moving => movingPlatformPool,
            _ => throw new System.Exception("TODO")
        };
    }

    private static ObjectPool<GameObject> CreateObjectPool(GameObject prefab)
    {
        return new ObjectPool<GameObject>(
            () => Instantiate(prefab),
            (obj) => obj.SetActive(true),
            (obj) => obj.SetActive(false),
            (obj) => Destroy(obj),
            false,
            10,
            20);
    }
}