using UnityEngine;

/// <summary>
/// Базовый класс платформы.
/// </summary>
public class BasePlatform : MonoBehaviour
{
    /// <summary>
    /// Тип платформы.
    /// </summary>
    public virtual PlatfromType PlatfromType => PlatfromType.Static;

    /// <summary>
    /// Наивысшая точка, которую может достигнуть платформа после создания.
    /// </summary>
    public virtual float YMaxPosition => transform.position.y;

    /// <summary>
    /// Отступ до следующей платформы.
    /// </summary>
    public virtual float NextPlatformOffset => 3f;

    /// <summary>
    /// Инициализирует платформу после добавления на уровень.
    /// </summary>
    public virtual void ReInit()
    {
    }

    /// <summary>
    /// Сдвигает платформу по оси Y.
    /// </summary>
    /// <param name="yDistance"></param>
    public virtual void Move(float yDistance)
    {
        transform.position = transform.position.ShiftY(yDistance);
    }

    /// <summary>
    /// Обрабатывает столкновение с игроком.
    /// </summary>
    /// <returns>True, если столкновение доступно.</returns>
    public virtual bool CheckPlayerCollision()
    {
        return true;
    }
}
