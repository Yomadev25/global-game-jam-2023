using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;

    public UnityEvent onTakeDamage;

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

    void Death()
    {
        Destroy(this.gameObject);
    }
}
