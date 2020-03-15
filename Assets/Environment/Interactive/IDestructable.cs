using System;

public interface IDestructable
{
    float Health { get; }
    event Action<float> OnGotHit;
    event Action OnDestroyed;
    void Hit(float damage);
}
