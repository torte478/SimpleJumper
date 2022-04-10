using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {   
        var position = transform.position;
        position.y = target.position.y;
        transform.position = position;
    }
}
