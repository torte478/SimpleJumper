using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    public virtual PlatfromType PlatfromType => PlatfromType.Static;

    public virtual float YMaxPosition => transform.position.y;

    public virtual void ReInit()
    {
    }

    public virtual void Move(float yDistance)
    {
        transform.position = transform.position.ShiftY(yDistance);
    }
}
