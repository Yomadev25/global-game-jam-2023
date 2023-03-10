using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [Header("Cooldown")]
    [SerializeField] private float _skill1cooldown;
    [SerializeField] private float _skill2cooldown;
    [SerializeField] private float _skill3cooldown;

    [SerializeField] private Animator _rightAnim;
    [SerializeField] private Animator _leftAnim;

    [SerializeField] private ParticleSystem _slashEffect;
    [SerializeField] private GameObject _slashFx;

    public enum Types { Ball, Wave, Wall }
    public List<Types> types = new List<Types>();

    void Start()
    {
        SkillSetup();
    }

    void SkillSetup()
    {
        Types type = Types.Ball;

        for (int i = 0; i < SkillManager.instance.GetSkillIndexLength(); i++)
        {
            if (SkillManager.instance.GetSkillData().SkillUnlocks[i])
            {
                switch (i)
                {
                    case 0:
                        type = Types.Ball;
                        break; 
                    case 1:    
                        type = Types.Wave;
                        break; 
                    case 2:    
                        type = Types.Wall;
                        break; 
                    case 3:    
                        type = Types.Ball;
                        break; 
                    case 4:    
                        type = Types.Wave;
                        break; 
                    case 5:    
                        type = Types.Wall;
                        break; 
                    case 6:    
                        type = Types.Ball;
                        break; 
                    case 7:    
                        type = Types.Wave;
                        break; 
                    case 8:    
                        type = Types.Wall;
                        break;
                }

                types.Add(type);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Attack("Attack");
        }
        if (Input.GetKeyDown(_skill1Key))
        {
            OnSkill(types[0]);
        }
        if (Input.GetKeyDown(_skill2Key))
        {
            OnSkill(types[1]);
        }
        if (Input.GetKeyDown(_skill3Key))
        {
            OnSkill(types[2]);
        }

        if (_skill1cooldown > 0)
        {
            _skill1cooldown -= Time.deltaTime;
        }
        if (_skill2cooldown > 0)
        {
            _skill2cooldown -= Time.deltaTime;
        }
        if (_skill3cooldown > 0)
        {
            _skill3cooldown -= Time.deltaTime;
        }
    }
    
    void Attack(string name)
    {
        _rightAnim.Play(name);
        GameObject GO = Instantiate(_slashFx);
        Destroy(GO, 2f);

        NormalAttack();
    }

    public void NormalAttack()
    {
        _slashEffect.Play();
        foreach (Collider col in Physics.OverlapBox(_hitbox.position, new Vector3(0.7f, 0.7f, 0.7f), Quaternion.identity))
        {
            if (col.CompareTag("Enemy"))
            {
                var enemyManager = col.GetComponent<EnemyManager>();
                enemyManager.TakeDamage(_atk);
            }
        }
    }

    void OnSkill(Types skillType)
    {
        if (skillType == Types.Ball)
        {
            BallSkill();
        }
        else if (skillType == Types.Wave)
        {
            WaveSkill();
        }
        else if (skillType == Types.Wall)
        {
            WallSkill();
        }
    }

    public void BallSkill()
    {
        if (_skill1cooldown > 0) return;

        SkillManager.Elements element = SkillManager.Elements.None;
        if (SkillManager.instance.GetSkillData().SkillUnlocks[0])
        {
            element = SkillManager.Elements.Fire;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[3])
        {
            element = SkillManager.Elements.Thunder;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[6])
        {
            element = SkillManager.Elements.Earth;
        }
        else
        {
            element = SkillManager.Elements.None;
        }

        GameObject GO = Instantiate(SkillManager.instance.GetBall(element), _hitbox.position, _hitbox.rotation);
        _skill1cooldown = 10;

        _leftAnim.Play("Attack");
    }

    public void WaveSkill()
    {
        if (_skill2cooldown > 0) return;

        SkillManager.Elements element = SkillManager.Elements.None;
        EnemyManager.Status status = EnemyManager.Status.None;
        float damage = 0;
        if (SkillManager.instance.GetSkillData().SkillUnlocks[1])
        {
            element = SkillManager.Elements.Fire;
            status = EnemyManager.Status.Burn;
            damage = 2;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[4])
        {
            element = SkillManager.Elements.Thunder;
            status = EnemyManager.Status.Slow;
            damage = 1;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[7])
        {
            element = SkillManager.Elements.Earth;
            damage = 3;
        }

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 15f, _targetMask);

        if (rangeChecks.Length != 0)
        {
            foreach (var enemy in rangeChecks)
            {
                var enemyManager = enemy.GetComponent<EnemyManager>();
                enemyManager.TakeDamage(damage);
                enemyManager.TakeStatus(status);
            }
        }

        GameObject GO = Instantiate(SkillManager.instance.GetWave(element), this.transform.position, Quaternion.identity);
        Destroy(GO, 1);
        _skill2cooldown = 10;

        _leftAnim.Play("Attack");
    }

    public void WallSkill()
    {
        if (_skill3cooldown > 0) return;

        SkillManager.Elements element = SkillManager.Elements.None;
        if (SkillManager.instance.GetSkillData().SkillUnlocks[2])
        {
            element = SkillManager.Elements.Fire;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[5])
        {
            element = SkillManager.Elements.Thunder;
        }
        else if (SkillManager.instance.GetSkillData().SkillUnlocks[8])
        {
            element = SkillManager.Elements.Earth;
        }

        Vector3 spawnPos = this.transform.position + this.transform.forward * 10f;
        spawnPos.y = 2.3f;
        GameObject GO = Instantiate(SkillManager.instance.GetWall(element), spawnPos, this.transform.rotation);
        _skill3cooldown = 10;

        _leftAnim.Play("Attack");
    }
}


