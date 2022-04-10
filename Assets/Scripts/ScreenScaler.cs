using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    public Vector3 OriginSize;
    public Vector3 Size;
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
