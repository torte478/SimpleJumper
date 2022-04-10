using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс-контроллер платформ.
/// </summary>
public class Platforms : MonoBehaviour
{
    private PlatfromPool pool;
    private LinkedList<BasePlatform> platformList;
    private Vector3 scaleFactor;

    private BasePlatform lastCreated;

    /// <summary>
    /// Общее количество одновременно существующих платформ.
    /// </summary>
    public int Total;

    /// <summary>
    /// Координата уничтожения платформ.
    /// </summary>
    public float YDestroyValue;

    /// <summary>
    /// Диапазон, в котором создаются новые платформы.
    /// </summary>
    public float XRange;

    /// <summary>
    /// Шанс создания движущейся платформы.
    /// </summary>
    public float MovingPlatformChance = 0.3f;

    /// <summary>
    /// Шанс создания платформы-ловушки.
    /// </summary>
    public float TrapPlatformChance = 0.1f;

    void Start()
    {
        pool = GetComponent<PlatfromPool>();

        scaleFactor = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<ScreenScaler>().ScaleFactor;

        YDestroyValue *= scaleFactor.y;
        XRange *= scaleFactor.x;

        CreateStartPlatforms();
    }

    /// <summary>
    /// Перемещает все активные платформы по оси Y.
    /// </summary>
    /// <param name="yDistance">Расстояние перемещения.</param>
    public void MovePlatforms(float yDistance)
    {
        platformList.RemoveWhere(
            (p) =>
            {
                p.Move(yDistance);
                return p.YMaxPosition < YDestroyValue;
            },
            (p) => pool.Remove(p));

        while (platformList.Count < Total)
            CreateRandomPlatform();
    }

    private void CreateRandomPlatform()
    {
        var x = Random.Range(-XRange, XRange);
        var y = (lastCreated.transform.position.y + lastCreated.NextPlatformOffset) * scaleFactor.y;

        var type = GetRandomPlatformType();

        var movement = Random.Range(0, 2) == 0
            ? Vector3.right
            : Vector3.up;

        var newPlatform = CreatePlatform(new Vector3(x, y), type, movement);
    }

    private BasePlatform CreatePlatform(Vector3 position, PlatfromType type, Vector3 movement)
    {
        var newPlatform = pool.Create(position, type);

        if (type == PlatfromType.Moving)
        {
            (newPlatform as MovingPlatform).StartMovementDirection = movement;
        }

        newPlatform.ReInit();
        lastCreated = newPlatform;
        platformList.AddLast(newPlatform);

        return newPlatform;

    }

    private PlatfromType GetRandomPlatformType()
    {
        if (lastCreated.PlatfromType != PlatfromType.Static)
            return PlatfromType.Static;

        var chance = Random.Range(0.0f, 1.0f);

        if (chance <= MovingPlatformChance)
            return PlatfromType.Moving;

        chance -= MovingPlatformChance;
        if (chance <= TrapPlatformChance)
            return PlatfromType.Trap;

        return PlatfromType.Static;
    }

    private void CreateStartPlatforms()
    {
        platformList = new LinkedList<BasePlatform>();

        for (var i = 0; i < Total; ++i)
        {
            if (i == 0)
                CreatePlatform(new Vector3(2f * scaleFactor.x, 0.0f), PlatfromType.Static, Vector3.zero);
            else if (i == 1)
                CreatePlatform(new Vector3(-2f * scaleFactor.x, 2.5f * scaleFactor.y), PlatfromType.Static, Vector3.zero);
            else
                CreateRandomPlatform();
        }
    }
}
