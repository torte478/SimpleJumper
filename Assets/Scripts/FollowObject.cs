using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {   
        transform.position = target.transform.position + offset;
    }
}
