using UnityEngine;

/// <summary>
/// Класс движущейся платформы.
/// </summary>
public class MovingPlatform : BasePlatform
{
    private Vector3 originPosition;
    private Vector3 movementDirection;

    /// <summary>
    /// Максимальное отклонение от первоначальной позиции.
    /// </summary>
    public Vector2 MaxOffset;

    /// <summary>
    /// Скорость движения.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Начальное направление движения.
    /// </summary>
    public Vector3 StartMovementDirection = Vector3.right;

    /// <inheritdoc cref="BasePlatform.PlatfromType"/>
    public override PlatfromType PlatfromType => PlatfromType.Moving;

    /// <inheritdoc cref="BasePlatform.YMaxPosition"/>
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
        MoveTransform();
    }

    /// <inheritdoc cref="BasePlatform.ReInit"/>
    public override void ReInit()
    {
        originPosition = new Vector3(0, transform.position.y);
        movementDirection = StartMovementDirection;
    }

    /// <inheritdoc cref="BasePlatform.Move(float)"/>
    public override void Move(float yDistance)
    {
        transform.position = transform.position.ShiftY(yDistance);
        originPosition = originPosition.ShiftY(yDistance);
    }

    private void MoveTransform()
    {
        var shift = movementDirection * Speed * Time.deltaTime;
        transform.position += shift;

        var currentOffset = transform.position - originPosition;

        if (currentOffset.x < -MaxOffset.x || currentOffset.y < -MaxOffset.y)
            movementDirection = StartMovementDirection;

        if (currentOffset.x > MaxOffset.x || currentOffset.y > MaxOffset.y)
            movementDirection = -1 * StartMovementDirection;
    }
}
