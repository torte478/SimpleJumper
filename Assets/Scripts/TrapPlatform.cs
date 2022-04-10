using UnityEngine;

public class TrapPlatform : BasePlatform
{
    public override PlatfromType PlatfromType => PlatfromType.Trap;

    public override float NextPlatformOffset => 1f;

    public override bool CheckCollision()
    {
        gameObject.SetActive(false);

        return false;
    }
}
