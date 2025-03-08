using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);
    [SerializeField] private float _minDistance = 0.2f;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _maxCameraAngle = 60f;

    private Transform _player;
    private CameraCollisionHandler _collisionHandler;

    private void Start()
    {
        FindPlayer();
        // ѕередаем слой столкновений в конструктор
        _collisionHandler = new CameraCollisionHandler(_minDistance, _collisionLayer);
    }

    private void FixedUpdate()
    {
        if (_player == null)
        {
            FindPlayer();
            return;
        }

        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = _player.position + _offset;

        // ѕрилепл€ем камеру к преп€тстви€м по оси Z
        if (_collisionHandler.TryGetAdjustedPosition(_player.position, targetPosition, out targetPosition))
        {
            // ќграничиваем угол наклона камеры по оси X
            ClampCameraAngle(ref targetPosition);
        }

        // ”станавливаем позицию камеры
        transform.position = targetPosition;
        transform.LookAt(_player); //  амера всегда смотрит на игрока
    }

    private void ClampCameraAngle(ref Vector3 targetPosition)
    {
        // –ассчитываем угол наклона камеры по оси X
        Vector3 direction = targetPosition - _player.position;
        float angle = Vector3.Angle(Vector3.up, direction);

        // ≈сли угол наклона больше максимально допустимого, ограничиваем его
        if (angle > _maxCameraAngle)
        {
            direction = Quaternion.Euler(_maxCameraAngle, 0f, 0f) * direction;
            targetPosition = _player.position + direction;
        }
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _player = playerObject.transform;
        }
    }
}
