using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    private CapsuleCollider _collider;
    private Animator _animator;
    private Scrollbar _healthBar;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<Scrollbar>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _currentHealth = _maxHealth;
        _healthBar.size = Mathf.Clamp01(_currentHealth / _maxHealth);
    }
    private void Start()
    {
        if (_healthBar == null) Debug.LogError("Health Bar - не инициализирован!");
        if (_animator == null) Debug.LogError("Animator - не инициализирован!");
        if (_collider == null) Debug.LogError("Collider - не инициализирован!");
    }

    public void TakeDamage(float damage)
    {
        if(_animator.GetBool("isDeath")) return;

        _currentHealth -= damage;
        _healthBar.size = Mathf.Clamp01(_currentHealth / _maxHealth);

        Debug.Log($"{name} получил {damage} урона");

        _animator.SetBool("isDamage", true);
        if (_currentHealth <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(ResetDamageAnimation());
        }
    }

    private IEnumerator ResetDamageAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isDamage", false);
    }

    private void Die()
    {
        _animator.SetTrigger("isDeath");
        _healthBar.gameObject.SetActive(false);
        _collider.enabled = false;

        Destroy(gameObject, 5f);
    }
}
