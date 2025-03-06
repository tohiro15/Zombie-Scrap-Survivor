using UnityEngine;

public class ZombieMovement : IMovable
{
    private readonly Transform _zombieTransform;
    private readonly float _speed;
    public ZombieMovement(Transform zombieTransform, float speed)
    {
        _zombieTransform = zombieTransform;
        _speed = speed;
    }

    public void Move(Vector3 direction)
    {
        _zombieTransform.position += direction * _speed * Time.deltaTime;
        Rotate(direction);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _zombieTransform.rotation = Quaternion.Lerp(
                _zombieTransform.rotation, targetRotation, Time.deltaTime * 5f
            );
        }
    }
}
