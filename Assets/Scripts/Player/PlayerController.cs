using UnityEngine;

public class PlayerController : PlayerBase
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Canvas _joystickUI;
    [SerializeField] private bool _mobileDevice = false;

    private Transform _targetZombie;
    private float _attackRange = 2f;
    private float _attackCooldown = 2f;
    private float _nextAttackTime = 0f;
    protected override void Awake()
    {
        base.Awake();
        CheckDevice();
    }

    private void Update()
    {
        Move();
        CheckAttack();
    }

    protected override void Move()
    {
        if (_animator.GetBool("isDamage") || _animator.GetBool("isDeath"))
        {
            return;
        }

        float moveX = 0f;
        float moveZ = 0f;

        if (_joystick != null && _joystick.gameObject.activeSelf && _mobileDevice)
        {
            moveX = _joystick.Horizontal;
            moveZ = _joystick.Vertical;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }

        Vector3 moveInput = new Vector3(moveX, 0, moveZ).normalized;

        bool isWalking = moveInput.magnitude > 0.1f;
        _animator.SetBool("isWalk", isWalking);

        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        _rb.linearVelocity = moveInput * _speed + new Vector3(0, _rb.linearVelocity.y, 0);
    }

    private void CheckAttack()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestZombie = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject zombie in zombies)
        {
            float distanceToZombie = Vector3.Distance(transform.position, zombie.transform.position);
            if (distanceToZombie <= _attackRange && distanceToZombie < closestDistance)
            {
                closestDistance = distanceToZombie;
                closestZombie = zombie;
            }
        }

        if (closestZombie != null && closestZombie.GetComponent<Animator>().GetBool("isDeath") == false && Time.time >= _nextAttackTime)
        {
            AttackZombie(closestZombie.transform);
        }
    }


    private void AttackZombie(Transform targetZombie)
    {
        if (_currentHealth <= 0) return;

        _animator.SetTrigger("isAttack");
        targetZombie.GetComponent<IDamageable>().TakeDamage(20f);

        _nextAttackTime = Time.time + _attackCooldown;
    }

    private void CheckDevice()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop && _joystickUI != null && !_mobileDevice)
        {
            _mobileDevice = false;
            _joystickUI.gameObject.SetActive(false);
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld && _joystickUI != null)
        {
            _mobileDevice = true;
            _joystickUI.gameObject.SetActive(true);
        }
    }

    public void SetTargetZombie(Transform zombie)
    {
        _targetZombie = zombie;
    }
}