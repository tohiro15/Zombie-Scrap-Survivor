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
        // �������� ���� ������������ � �����������
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

        // ���������� ������ � ������������ �� ��� Z
        if (_collisionHandler.TryGetAdjustedPosition(_player.position, targetPosition, out targetPosition))
        {
            // ������������ ���� ������� ������ �� ��� X
            ClampCameraAngle(ref targetPosition);
        }

        // ������������� ������� ������
        transform.position = targetPosition;
        transform.LookAt(_player); // ������ ������ ������� �� ������
    }

    private void ClampCameraAngle(ref Vector3 targetPosition)
    {
        // ������������ ���� ������� ������ �� ��� X
        Vector3 direction = targetPosition - _player.position;
        float angle = Vector3.Angle(Vector3.up, direction);

        // ���� ���� ������� ������ ����������� �����������, ������������ ���
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
