using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _hitbox;
    [SerializeField] private LayerMask _targetMask;

    [Header("Player Status")]
    [SerializeField] private float _atk;

    [Header("Skill Input Setting")]
    [SerializeField] private KeyCode _skill1Key;
    [SerializeField] private KeyCode _skill2Key;
    [SerializeField] private KeyCode _skill3Key;

    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack("punch");
        }
        if (Input.GetKeyDown(_skill1Key))
        {
            BallSkill();
        }
        if (Input.GetKeyDown(_skill2Key))
        {
            WaveSkill();
        }
        if (Input.GetKeyDown(_skill3Key))
        {
            WallSkill();
        }
    }
    
    void Attack(string name)
    {
        Debug.Log("Attack");
        _anim.Play(name);
    }

    public void NormalAttack()
    {
        foreach (Collider col in Physics.OverlapBox(_hitbox.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
        {
            if (col.CompareTag("Enemy"))
            {
                var enemyManager = col.GetComponent<EnemyManager>();
                enemyManager.TakeDamage(_atk);
            }
        }
    }

    public void BallSkill()
    {
        //assign element
        GameObject GO = Instantiate(SkillManager.instance.GetBall(SkillManager.Elements.Fire), _hitbox.position, _hitbox.rotation);       
    }

    public void WaveSkill()
    {
        //assign element
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 15f, _targetMask);
        Action status = null;

        if (rangeChecks.Length != 0)
        {
            foreach (var enemy in rangeChecks)
            {
                var enemyManager = enemy.GetComponent<EnemyManager>();
                enemyManager.TakeDamage(1);
                
            }
        }
        //GameObject GO = Instantiate(SkillManager.instance.GetWave(SkillManager.Elements.Fire));
    }

    public void WallSkill()
    {
        //assign element
        Vector3 spawnPos = this.transform.position + this.transform.forward * 10f;
        spawnPos.y = 2.3f;
        GameObject GO = Instantiate(SkillManager.instance.GetWall(SkillManager.Elements.Fire), spawnPos, this.transform.rotation);
    }
}
