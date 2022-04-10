using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 raycastBounds;

    private float yPrevious;

    public float JumpForceY;
    public float MaxSpeed = 3.0f;
    public float Speed = 50.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        var collider = GetComponent<BoxCollider2D>();
        raycastBounds = new Vector2(
            collider.bounds.extents.x,
            collider.bounds.extents.y + 0.1f);
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        var xMove = Input.GetAxis("Horizontal");
        if (xMove != 0)
        {
            var xSpeed = Mathf.Abs(xMove * body.velocity.x);

            if (xSpeed < MaxSpeed)
            {
                var force = Speed * xMove * Vector3.right;
                body.AddForce(force);
            }

            if (Mathf.Abs(body.velocity.x) > MaxSpeed)
            {
                var velocity = new Vector2(
                    Mathf.Sign(body.velocity.x) * MaxSpeed,
                    body.velocity.y);

                body.velocity = velocity;
            }
        }
        else
        {
            var velocity = body.velocity;
            velocity.x *= 0.9f;
            body.velocity = velocity;
        }

        var yCurrent = transform.position.y;

        var left = new Vector3(transform.position.x - raycastBounds.x, yCurrent);
        var right = new Vector3(transform.position.x + raycastBounds.x, yCurrent);

        if ((Physics2D.Raycast(left, Vector3.down, raycastBounds.y) 
            || Physics2D.Raycast(right, Vector3.down, raycastBounds.y)) 
            && yCurrent < yPrevious)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0, JumpForceY));
        }

        yPrevious = yCurrent;
    }
}
