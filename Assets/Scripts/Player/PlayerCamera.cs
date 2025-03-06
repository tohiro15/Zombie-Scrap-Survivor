using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 10f, -10f);

    private void Start()
    {
        FindPlayer();
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothSpeed * Time.deltaTime);
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
