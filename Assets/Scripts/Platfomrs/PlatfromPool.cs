using System;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Класс для управления пулами платформ.
/// </summary>
public class PlatfromPool : MonoBehaviour
{
    private ObjectPool<GameObject> staticPlatformPool;
    private ObjectPool<GameObject> movingPlatformPool;
    private ObjectPool<GameObject> trapPlatformPool;

    /// <summary>
    /// Префаб обычной платформы.
    /// </summary>
    public GameObject StaticPlatformPrefab;

    /// <summary>
    /// Префаб движущейся платформы.
    /// </summary>
    public GameObject MovingPlatformPrefab;

    /// <summary>
    /// Префаб платформы-ловушки.
    /// </summary>
    public GameObject TrapPlatformPrefab;

    void Awake()
    {
        staticPlatformPool = CreateObjectPool(StaticPlatformPrefab);
        movingPlatformPool = CreateObjectPool(MovingPlatformPrefab);
        trapPlatformPool = CreateObjectPool(TrapPlatformPrefab);
    }

    /// <summary>
    /// Создает новую платформу.
    /// </summary>
    /// <param name="position">Позиция платформы.</param>
    /// <param name="type">Тип платформы.</param>
    public BasePlatform Create(Vector3 position, PlatfromType type)
    {
        var pool = GetObjectPool(type);

        var platform = pool.Get();
        platform.transform.position = position;
        return platform.GetComponent<BasePlatform>();
    }

    /// <summary>
    /// Удаляет платформу.
    /// </summary>
    /// <param name="platform">Платформа.</param>
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
            PlatfromType.Trap => trapPlatformPool,
            _ => throw new ArgumentException($"Unable to create platform of type {type}")
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