using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 raycastBounds;
    private AudioSource audioSource;

    private float yPrevious;
    private Vector2 viewSize;

    public float JumpForceY;
    public float MaxSpeed = 3.0f;
    public float Speed = 50.0f;

    public AudioClip JumpSound;
    public AudioClip GameOverSound;

    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        var collider = GetComponent<BoxCollider2D>();
        raycastBounds = new Vector2(
            collider.bounds.extents.x,
            collider.bounds.extents.y + 0.1f);

        viewSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        var xMove = GetXMove();

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

        var hit = Physics2D.Raycast(left, Vector3.down, raycastBounds.y);
        if (!hit)
            hit = Physics2D.Raycast(right, Vector3.down, raycastBounds.y);

        if (hit && yCurrent < yPrevious && hit.collider.gameObject.TryGetComponent<BasePlatform>(out var platform))
        {
            var canJump = platform.CheckCollision();
            
            if (canJump)
            {
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(new Vector2(0, JumpForceY));
                audioSource.PlayOneShot(JumpSound);
            }
        }

        yPrevious = yCurrent;


        if (transform.position.x < -viewSize.x)
        {
            transform.position = new Vector3(viewSize.x, transform.position.y);
        }
        else if (transform.position.x > viewSize.x)
        {
            transform.position = new Vector3(-viewSize.x, transform.position.y);
        }
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(GameOverSound);
    }

    private float GetXMove()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            return touch.position.x > 0 // TODO : fix
                ? 1.0f
                : -1.0f;
        }
        else 
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
