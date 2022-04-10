using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    private PlatfromPool pool;
    private LinkedList<BasePlatform> platformList;

    private BasePlatform lastCreated;

    public int Total;
    public float yDestroyValue;
    public float xRange;

    public float MovingPlatformChance = 0.3f;
    public float TrapPlatformChance = 0.1f;

    void Start()
    {
        pool = GetComponent<PlatfromPool>();    

        platformList = new LinkedList<BasePlatform>();
        for (var i = 0; i < Total; ++i)
        {
            if (i == 0)
                CreatePlatform(new Vector3(0.0f, 0.0f), PlatfromType.Static, Vector3.zero);
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

            if (platform.YMaxPosition < yDestroyValue)
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
            Random.Range(-xRange, xRange), 
            lastCreated.transform.position.y + lastCreated.NextPlatformOffset);

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
