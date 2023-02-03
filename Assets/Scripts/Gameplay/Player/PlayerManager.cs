using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _maxHp;
    [SerializeField] private float _hp;
    public float MaxHp => _maxHp;
    public float HP => _hp;

    [SerializeField] private float _maxStamina;
    [SerializeField] private float _stamina;
    public float Stamina => _stamina;
    public float MaxStamina => _maxStamina;

    void Start()
    {
        _hp = _maxHp;
        _stamina = _maxStamina;
    }

    void Update()
    {
        UIManager.instance.UpdateHpBar();
        UIManager.instance.UpdateStaminaBar();

        if (_hp <= 0)
        {
            Gameover();
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        //effect
    }

    public void Gameover()
    {

    }
}
