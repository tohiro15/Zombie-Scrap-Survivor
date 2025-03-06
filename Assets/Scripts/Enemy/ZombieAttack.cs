using UnityEngine;

public class ZombieAttack : IAttackable
{
    private readonly Animator _animator;
    private readonly float _damage;
    private readonly float _attackCooldown;
    private float _nextAttackTime = 0f;

    public ZombieAttack(Animator animator, float damage, float attackCooldown)
    {
        _animator = animator;
        _damage = damage;
        _attackCooldown = attackCooldown;
    }

    public void Attack(IDamageable target)
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isAttack", true);
            
            target.TakeDamage(_damage);
            _nextAttackTime = Time.time + _attackCooldown;
        }
        else
        {
            _animator.SetBool("isAttack", false);
        }    
    }
}
