using UnityEngine;

/// <summary>
/// Класс платформы ловушки.
/// </summary>
public class TrapPlatform : BasePlatform
{
    /// <inheritdoc cref="BasePlatform.PlatfromType"/>
    public override PlatfromType PlatfromType => PlatfromType.Trap;

    /// <inheritdoc cref="BasePlatform.NextPlatformOffset"/>
    public override float NextPlatformOffset => 1f;

    /// <summary>
    /// Эффект разрушения платформы.
    /// </summary>
    public Transform Particles;

    /// <inheritdoc cref="BasePlatform.CheckPlayerCollision"/>
    public override bool CheckPlayerCollision()
    {
        gameObject.SetActive(false);
        var particles = ((Transform)Instantiate(Particles, transform.position, transform.rotation)).gameObject;
        Destroy(particles, 2.0f);

        return false;
    }
}
