using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Wall : MonoBehaviour
{
    [SerializeField] private float _liveDuration = 5f;
    [SerializeField] private SkillManager.Elements _element;

    [SerializeField] private BoxCollider _boxCollider;

    void Start()
    {
        switch (_element)
        {
            case SkillManager.Elements.Fire:
                _boxCollider.isTrigger = false;
                break;
            case SkillManager.Elements.Thunder:
                _boxCollider.isTrigger = false;
                break;
            case SkillManager.Elements.Earth:
                _boxCollider.isTrigger = true;
                this.AddComponent<NavMeshObstacle>();
                break;
        }

        Destroy(gameObject, _liveDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemyManager = other.GetComponent<EnemyManager>();
            
            switch (_element)
            {
                case SkillManager.Elements.Fire:
                    enemyManager.TakeStatus(EnemyManager.Status.Burn);
                    break;
                case SkillManager.Elements.Thunder:
                    enemyManager.TakeStatus(EnemyManager.Status.Slow);
                    break;
            }
        }
    }
}
