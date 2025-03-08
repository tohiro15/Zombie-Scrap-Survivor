using UnityEngine;

public class CameraCollisionHandler
{
    private float _minDistance;
    private LayerMask _collisionLayer;

    public CameraCollisionHandler(float minDistance, LayerMask collisionLayer)
    {
        _minDistance = minDistance;
        _collisionLayer = collisionLayer;
    }

    public bool TryGetAdjustedPosition(Vector3 playerPosition, Vector3 targetPosition, out Vector3 adjustedPosition)
    {
        // ������������ ������ ����� ������� � ������� ��������
        Vector3 direction = targetPosition - playerPosition;

        RaycastHit hit;
        if (Physics.Raycast(playerPosition, direction, out hit, direction.magnitude, _collisionLayer))
        {
            // ���������� ������ � �����������
            adjustedPosition = hit.point - hit.normal * _minDistance;
            return true;
        }

        adjustedPosition = targetPosition;
        return false;
    }
}
