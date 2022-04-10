using UnityEngine;

/// <summary>
/// Вспомогательный класс расширений для работы с Vector3.
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// Возвращает новый вектор с измененным значением X.
    /// </summary>
    /// <param name="origin">Исходный вектор.</param>
    /// <param name="newValue">Новое значение X.</param>
    public static Vector3 SetX(this Vector3 origin, float newValue)
    {
        return new Vector3(newValue, origin.y, origin.z);
    }

    /// <summary>
    /// Возвращает новый вектор, получившийся сдвигом исходного по оси X.
    /// </summary>
    /// <param name="origin">Исходный вектор.</param>
    /// <param name="distance">Величина сдвига.</param>
    public static Vector3 ShiftX(this Vector3 origin, float distance)
    {
        return new Vector3(origin.x + distance, origin.y, origin.z);
    }

    /// <summary>
    /// Возвращает новый вектор с измененным значением Y.
    /// </summary>
    /// <param name="origin">Исходный вектор.</param>
    /// <param name="newValue">Новое значение Y.</param>
    public static Vector3 SetY(this Vector3 origin, float newValue)
    {
        return new Vector3(origin.x, newValue, origin.z);
    }

    /// <summary>
    /// Возвращает новый вектор, получившийся сдвигом исходного по оси Y.
    /// </summary>
    /// <param name="origin">Исходный вектор.</param>
    /// <param name="distance">Величина сдвига.</param>
    public static Vector3 ShiftY(this Vector3 origin, float distance)
    {
        return new Vector3(origin.x, origin.y + distance, origin.z);
    }
}
