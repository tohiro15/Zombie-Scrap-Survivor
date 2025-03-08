using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}
public interface ICameraCollisionHandler
{
    bool TryGetAdjustedPosition(Vector3 playerPosition, Vector3 cameraPosition, out Vector3 adjustedPosition);
}
