using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    private PlatfromPool pool;
    private LinkedList<BasePlatform> platformList;
    private Vector3 scaleFactor;

    private BasePlatform lastCreated;

    public int Total;
    public float YDestroyValue;
    public float XRange;

    public float MovingPlatformChance = 0.3f;
    public float TrapPlatformChance = 0.1f;

    void Start()
    {
        pool = GetComponent<PlatfromPool>();   

        scaleFactor = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<ScreenScaler>().ScaleFactor;

        YDestroyValue *= scaleFactor.y;
        XRange *= scaleFactor.x;

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

    public void MovePlatforms(float yDistance)
    {
        var currentNode = platformList.First;
        
        while (currentNode != null)
        {
            var platform = currentNode.Value;
            platform.Move(yDistance);

            if (platform.YMaxPosition < YDestroyValue)
            {
                var toRemove = currentNode;
                currentNode = currentNode.Next;
                platformList.Remove(toRemove);

                pool.Remove(platform);
            }
            else
            {
                currentNode = currentNode.Next;
            }
        }

        while (platformList.Count < Total)
            CreateRandomPlatform();
    }

    private void CreateRandomPlatform()
    {
        var position = new Vector3(
            Random.Range(-XRange, XRange), 
            (lastCreated.transform.position.y + lastCreated.NextPlatformOffset) * scaleFactor.y);

        var type = GetRandomPlatformType();

        var movement = Random.Range(0, 2) == 0
            ? Vector3.right
            : Vector3.up;

        var newPlatform = CreatePlatform(position, type, movement);
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
        if (chance < TrapPlatformChance)
            return PlatfromType.Trap;

        return PlatfromType.Static;
    }
}
