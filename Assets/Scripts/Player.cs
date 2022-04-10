using UnityEngine;

/// <summary>
/// Класс игрока.
/// </summary>
public class Player : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 raycastBounds;
    private AudioSource audioSource;

    private float yPrevious;
    private Vector2 viewSize;

    /// <summary>
    /// Сила прыжка.
    /// </summary>
    public float JumpForceY;

    /// <summary>
    /// Максимальная скорость.
    /// </summary>
    public float MaxSpeed = 3.0f;

    /// <summary>
    /// Множитель скорости.
    /// </summary>
    public float SpeedFactor = 50.0f;

    /// <summary>
    /// Управляется ли игрок пользователем.
    /// </summary>
    public bool IsUserControl;

    /// <summary>
    /// Максимальная высота, достижимая игроком.
    /// </summary>
    public float TopY;

    /// <summary>
    /// Звук прыжка.
    /// </summary>
    public AudioClip JumpSound;

    /// <summary>
    /// Звук поражения.
    /// </summary>
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

        var scaleFactor = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<ScreenScaler>().ScaleFactor;

        SpeedFactor *= scaleFactor.x;
        MaxSpeed *= scaleFactor.x;
        JumpForceY *= scaleFactor.y;
        TopY *= scaleFactor.y;
        transform.position = new Vector3(
            transform.position.x * scaleFactor.x,
            transform.position.y * scaleFactor.y);
    }

    void FixedUpdate()
    {
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
        CheckHorizontalWrap();
    }

    /// <summary>
    /// Обрабатчик события завершения игры.
    /// </summary>
    public void GameOver()
    {
        audioSource.PlayOneShot(GameOverSound);
    }

    private void CheckHorizontalWrap()
    {
        if (transform.position.x < -viewSize.x)
        {
            transform.position = new Vector3(viewSize.x, transform.position.y);
        }
        else if (transform.position.x > viewSize.x)
        {
            transform.position = new Vector3(-viewSize.x, transform.position.y);
        }
    }

    private void UpdateVerticalMovement()
    {
        var yCurrent = transform.position.y;

        var leftEdge = new Vector3(transform.position.x - raycastBounds.x, yCurrent);
        var rightEdge = new Vector3(transform.position.x + raycastBounds.x, yCurrent);

        var hit = Physics2D.Raycast(leftEdge, Vector3.down, raycastBounds.y);
        if (!hit)
            hit = Physics2D.Raycast(rightEdge, Vector3.down, raycastBounds.y);

        var canJump = hit && 
                      yPrevious > yCurrent && 
                      yCurrent < TopY && 
                      hit.collider.gameObject.TryGetComponent<BasePlatform>(out var platform) && 
                      platform.CheckPlayerCollision();

        if (canJump)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.AddForce(new Vector2(0, JumpForceY));

            if (IsUserControl)
                audioSource.PlayOneShot(JumpSound);
        }

        yPrevious = yCurrent;
    }

    private void UpdateHorizontalMovement()
    {
        var xMove = GetXMove();

        if (xMove != 0)
        {
            var xSpeed = Mathf.Abs(xMove * body.velocity.x);

            if (xSpeed < MaxSpeed)
            {
                var force = SpeedFactor * xMove * Vector3.right;
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
    }

    private float GetXMove()
    {
        if (!IsUserControl)
            return 0.0f;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
            return worldPosition.x > 0
                ? 1.0f
                : -1.0f;
        }
        else 
        {
            return Input.GetAxis("Horizontal");
        }
    }
}
