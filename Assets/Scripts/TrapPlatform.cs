using UnityEngine;

public class TrapPlatform : BasePlatform
{
    public override PlatfromType PlatfromType => PlatfromType.Trap;

    public override float NextPlatformOffset => 1f;

    public Transform Particles;

    public override bool CheckCollision()
    {
        gameObject.SetActive(false);
        var particles = ((Transform)Instantiate(Particles, transform.position, transform.rotation)).gameObject;
        Destroy(particles, 2.0f);

        return false;
    }
}
