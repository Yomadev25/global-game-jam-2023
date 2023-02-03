using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform _hitbox;

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
        //2 second duration
        GameObject GO = Instantiate(SkillManager.instance.GetWave(SkillManager.Elements.Fire));
    }

    public void WallSkill()
    {
        //assign element
        GameObject GO = Instantiate(SkillManager.instance.GetWall(SkillManager.Elements.Fire));
    }
}
