using UnityEngine;

/// <summary>
/// Вспомогательный класс для расчета величины изменения разрешения экрана.
/// </summary>
public class ScreenScaler : MonoBehaviour
{
    /// <summary>
    /// Исходный размер экрана.
    /// </summary>
    public Vector3 OriginSize;

    /// <summary>
    /// Текущий размер экрана.
    /// </summary>
    public Vector3 Size;

    /// <summary>
    /// Множитель изменения размера.
    /// </summary>
    public Vector3 ScaleFactor;

    void Awake()
    {
        var minPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
        var maxPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        Size = (maxPosition - minPosition) * 0.5f;
        ScaleFactor = new Vector3(
            Size.x / OriginSize.x,
            Size.y / OriginSize.y);
    }
}
