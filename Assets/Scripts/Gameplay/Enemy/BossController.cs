using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    [Header("AI REFERENCES")]
    public NavMeshAgent _agent;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private float _timeBtwAttack;
    private float _cooldownAttack = 0;
    [HideInInspector] public Transform targetObj;
    [SerializeField] private Transform _hitboxPos;
    [SerializeField] private GameObject _ballPrefab;

    [Header("ANIMATION")]
    [SerializeField] private Animator _anim;

    public enum EnemyTypes { Melee, Range }
    [SerializeField] private EnemyTypes enemyType;
    public enum State { IDLE, CHASE, PATROL, ATTACK }
    private State _currentState;
    [SerializeField] private UnityEvent OnAttack;

    bool isPatrol;
    bool isAttack;
    bool canAttack;
    bool isDeath;
    [HideInInspector] public bool isStop;

    float isAttackCooldown;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        AIStateChange(State.IDLE);
    }

    void Update()
    {
        if (isDeath) return;

        Idle();

        if (_cooldownAttack <= 0)
        {
            canAttack = true;
        }
        else
        {
            _cooldownAttack -= Time.deltaTime;
            canAttack = false;
        }
    }

    void AIStateChange(State newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
    }

    void EnemyCheck()
    {
        if (isDeath) return;

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length != 0)
        {
            targetObj = rangeChecks[0].transform;

            AIStateChange(State.PATROL);
            PatrolBeforeAttack();
        }
    }

    void Idle()
    {
        if (_currentState != State.IDLE) return;

        EnemyCheck();
        _anim.SetBool("isRun", false);
    }

    void PatrolBeforeAttack()
    {
        if (enemyType == EnemyTypes.Melee)
        {
            StartCoroutine(MeleePatrolCoroutine());
        }
        else
        {
            StartCoroutine(RangePatrolCoroutine());
        }
    }

    IEnumerator MeleePatrolCoroutine()
    {
        while (true)
        {
            if (targetObj != null)
            {
                if (canAttack)
                {
                    _agent.destination = targetObj.position;
                }

                if (_agent.remainingDistance < 5 && canAttack)
                {
                    StartCoroutine(AttackCoroutine());
                    break;
                }
            }
            else
            {
                break;
            }

            if (_agent.remainingDistance < 5)
            {
                _agent.isStopped = true;
                _anim.SetBool("isRun", false);
            }
            else
            {
                _agent.isStopped = false;
                _anim.SetBool("isRun", true);
            }

            yield return null;
        }
    }

    IEnumerator AttackCoroutine()
    {
        _agent.isStopped = true;
        _anim.SetBool("isRun", false);
        var direction = targetObj.position - transform.position;
        direction.y = 0;
        transform.forward = direction;
        yield return new WaitForSeconds(0.5f);
        _anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(1.5f);
        foreach (Collider col in Physics.OverlapBox(_hitboxPos.position, new Vector3(1f, 1f, 1f), Quaternion.identity))
        {
            if (col.CompareTag("Player"))
            {
                var playerManager = col.GetComponent<PlayerManager>();
                playerManager.TakeDamage(10);
            }
        }
        _anim.SetBool("isAttack", false);

        yield return new WaitForSeconds(1f);
        _agent.isStopped = false;
        AIStateChange(State.IDLE);
        _cooldownAttack = _timeBtwAttack;
    }

    IEnumerator RangePatrolCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            float startTime = Time.time;
            float randomRotation = UnityEngine.Random.Range(0f, 360f);
            Vector3 movementDirection = Quaternion.Euler(0, randomRotation, 0) * new Vector3(UnityEngine.Random.Range(0f, 1f), 0, UnityEngine.Random.Range(0f, 1f));

            while (Time.time < startTime + 1.5f)
            {
                _anim.SetBool("isRun", true);
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0, movementDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
                _agent.Move(movementDirection.normalized * 5f * Time.deltaTime);

                yield return null;
            }
        }

        _agent.isStopped = true;
        var direction = targetObj.position - transform.position;
        direction.y = 0;
        transform.forward = direction;
        _anim.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.7f);
        GameObject GO = Instantiate(_ballPrefab, _hitboxPos.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(1f);

        _agent.isStopped = false;
        AIStateChange(State.IDLE);
    }

    void Attack()
    {
        if (isDeath) return;

        _agent.isStopped = true;

        if (isAttack || !canAttack)
        {
            AIStateChange(State.IDLE);
            return;
        }

        var direction = targetObj.position - transform.position;
        direction.y = 0;
        transform.forward = direction;

        isAttack = true;
        OnAttack?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }
}
