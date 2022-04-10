using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlatfromPool : MonoBehaviour
{
    private ObjectPool<GameObject>[] pools;

    public GameObject[] PlatformPrefabs;

    void Awake()
    {
        pools = new ObjectPool<GameObject>[1];
        var index = (int)PlatfromType.Static;
        pools[index] = new ObjectPool<GameObject>(
            () => Instantiate(PlatformPrefabs[index]),
            (obj) => obj.SetActive(true),
            (obj) => obj.SetActive(false),
            (obj) => Destroy(obj),
            false,
            10,
            20);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject Create(Vector3 position, PlatfromType type)
    {
        var platform = pools[(int)type].Get();
        platform.transform.position = position;
        return platform;
    }

    public void Remove(GameObject platform)
    {
        pools[0].Release(platform); // TODO
    }
}