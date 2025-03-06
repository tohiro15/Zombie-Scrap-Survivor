using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class PlayerBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _speed = 5f;
    [SerializeField] protected float _maxHealth = 100f;

    protected float _currentHealth;
    protected Rigidbody _rb;
    protected Animator _animator;
    protected Scrollbar _healthScrollbar;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _healthScrollbar = GetComponentInChildren<Scrollbar>();

        _currentHealth = _maxHealth;
        _healthScrollbar.size = Mathf.Clamp01(_currentHealth / _maxHealth);

        if (_rb == null) Debug.LogError($"{name}: Rigidbody не найден!");
        if (_animator == null) Debug.LogError($"{name}: Animator не найден!");
    }

    public virtual void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthScrollbar.size = Mathf.Clamp01(_currentHealth / _maxHealth);

        _animator.SetBool("isDamage", true);
        _animator.SetBool("isWalk", false);

        if (_currentHealth <= 0)
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


    protected virtual void Die()
    {
        _healthScrollbar.gameObject.SetActive(false);
        _animator.SetBool("isDamage", false);
        _animator.SetBool("isWalk", false);
        _animator.SetBool("isDeath", true);

        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(currentScene.name);
    }

    protected abstract void Move();
}
