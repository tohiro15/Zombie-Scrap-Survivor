using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Canvas _joystickUI;

    [SerializeField] private bool _testMobileJoysctick = false;

    private Rigidbody _rb;
    private Vector3 _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (SystemInfo.deviceType == DeviceType.Desktop && _joystickUI != null && _testMobileJoysctick == false)
        {
            _joystickUI.gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (_joystick != null && _joystick.gameObject.activeSelf == true && _testMobileJoysctick == true)
        {
            moveX = _joystick.Horizontal;
            moveZ = _joystick.Vertical;
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }


        _moveInput = new Vector3(moveX, 0, moveZ).normalized;
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * _speed + new Vector3(0, _rb.linearVelocity.y, 0);
    }
}
