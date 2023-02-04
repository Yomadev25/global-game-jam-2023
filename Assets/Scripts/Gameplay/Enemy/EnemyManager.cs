using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public enum Status
    {
        None,
        Burn,
        Slow
    }

    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;

    public UnityEvent onTakeDamage;
    public UnityEvent onDeath;

    void Start()
    {
        _hp = _maxHp;
    }

    void Update()
    {
        if (_hp <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        onTakeDamage?.Invoke();
    }

    public void TakeStatus(Status status)
    {
        switch (status)
        {
            case Status.Burn:
                StopCoroutine(BurnCoroutine());
                StartCoroutine(BurnCoroutine());
                break;
            case Status.Slow:
                StopCoroutine(SlowCoroutine());
                StartCoroutine(SlowCoroutine());
                break;
        }
    }

    IEnumerator BurnCoroutine()
    {
        float duration = 2f;

        while (duration > 0)
        {
            _hp -= 0.01f;
            duration -= Time.deltaTime;
            yield return null;
        }
         
    }

    IEnumerator SlowCoroutine()
    {
        float duration = 2f;

        while (duration > 0)
        {
            //slow
            duration -= Time.deltaTime;
            yield return null;
        }
        
    }

    void Death()
    {
        onDeath?.Invoke();
        Destroy(this.gameObject);
    }
}
