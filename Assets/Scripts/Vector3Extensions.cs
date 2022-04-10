using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 SetX(this Vector3 origin, float newValue)
    {
        return new Vector3(newValue, origin.y, origin.z);
    }

    public static Vector3 ShiftX(this Vector3 origin, float distance)
    {
        return new Vector3(origin.x + distance, origin.y, origin.z);
    }

    public static Vector3 SetY(this Vector3 origin, float newValue)
    {
        return new Vector3(origin.x, newValue, origin.z);
    }

    public static Vector3 ShiftY(this Vector3 origin, float distance)
    {
        return new Vector3(origin.x, origin.y + distance, origin.z);
    }
}
