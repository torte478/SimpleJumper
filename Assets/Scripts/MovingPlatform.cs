using UnityEngine;

public class MovingPlatform : BasePlatform
{
    private Vector3 originPosition;
    private Vector3 movementDirection;

    public Vector2 MaxOffset;
    public float Speed;

    public Vector3 StartMovementDirection = Vector3.right;

    public override PlatfromType PlatfromType => PlatfromType.Moving;
    public override float YMaxPosition => originPosition.y + MaxOffset.y;

    void Start()
    {
        var scaleFactor = GameObject
            .FindGameObjectWithTag("GameController")
            .GetComponent<ScreenScaler>().ScaleFactor;

        MaxOffset = new Vector3(
            MaxOffset.x * scaleFactor.x, 
            MaxOffset.y * scaleFactor.y);
        Speed *= MaxOffset.x;
    }

    void Update()
    {
        var shift = movementDirection * Speed * Time.deltaTime;
        transform.position += shift;

        var currentOffset = transform.position - originPosition;

        if (currentOffset.x < -MaxOffset.x || currentOffset.y < -MaxOffset.y)
            movementDirection = StartMovementDirection;
        
        if (currentOffset.x > MaxOffset.x || currentOffset.y > MaxOffset.y)
            movementDirection = -1 * StartMovementDirection;
    }

    public override void ReInit()
    {
        originPosition = new Vector3(0, transform.position.y);
        movementDirection = StartMovementDirection;
    }

    public override void Move(float yDistance)
    {
        transform.position = transform.position.ShiftY(yDistance);
        originPosition = originPosition.ShiftY(yDistance);
    }

}
