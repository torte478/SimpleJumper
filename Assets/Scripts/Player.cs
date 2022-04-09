using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private float raycastDistance;

    private float yPrevious;

    public float JumpForceY;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        raycastDistance = GetComponent<BoxCollider2D>().bounds.extents.y + .1f;
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        var yCurrent = transform.position.y;

        if (Physics2D.Raycast(transform.position, Vector3.down, raycastDistance) && yCurrent < yPrevious)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0, JumpForceY));
        }

        yPrevious = yCurrent;
    }
}
