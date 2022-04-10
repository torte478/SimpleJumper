using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    private PlatfromPool pool;
    private LinkedList<GameObject> platformList;

    private Transform lastCreated;


    public int Total;
    public float yDestroyValue;
    public float yCreateValue;

    void Start()
    {
        pool = GetComponent<PlatfromPool>();    

        platformList = new LinkedList<GameObject>();
        for (var i = 0; i < Total; ++i)
        {
            var x = i == 0 ? 0 : Random.Range(-2.5f, 2.5f);
            var y = 0.0f + i * 2.5f;

            var platform = pool.Create(new Vector3(x, y), PlatfromType.Static);
            lastCreated = platform.transform;
            platformList.AddLast(platform);
        }
    }

    public void MovePlatforms(float yDistance)
    {
        var currentNode = platformList.First;
        
        while (currentNode != null)
        {
            var platform = currentNode.Value;

            platform.transform.position = platform.transform.position.ShiftY(yDistance);

            if (platform.transform.position.y < yDestroyValue)
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
        {
            var position = new Vector3(
                Random.Range(-2.5f, 2.5f), 
                lastCreated.transform.position.y + 2.5f);

            var newPlatform = pool.Create(position, PlatfromType.Static);
            lastCreated = newPlatform.transform;
            platformList.AddLast(newPlatform);
        }
    }
}
