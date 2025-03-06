using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Настройки зомби")]
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackCooldown = 1.5f;

    private Transform _player;
    private Animator _animator;
    private IMovable _movement;
    private IAttackable _attack;
    private IDamageable _playerHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = new ZombieMovement(transform, _moveSpeed);
        _attack = new ZombieAttack(_animator, _attackDamage, _attackCooldown);
    }

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _player = playerObject.transform;
            _playerHealth = playerObject.GetComponent<IDamageable>();

            playerObject.GetComponent<PlayerController>().SetTargetZombie(transform);
        }
    }

    private void Update()
    {
        if (!_player || _playerHealth == null || _animator.GetBool("isDamage")) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        Vector3 direction = (_player.position - transform.position).normalized;

        if (distanceToPlayer > _attackDistance)
        {
            _animator.SetBool("isWalk", true);
            _animator.SetBool("isAttack", false);
            _movement.Move(direction);
        }
        else
        {
            _attack.Attack(_playerHealth);
        }
    }
}
