using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _liveDuration = 5f;
    [SerializeField] private SkillManager.Elements _element;

    [SerializeField] private string targetTag;
    [SerializeField] private GameObject _destroyFx;

    private float _damage;

    private void Start()
    {
        switch (_element)
        {
            case SkillManager.Elements.None:
                _damage = 1;
                break;
            case SkillManager.Elements.Fire:
                _damage = 2;
                break;
            case SkillManager.Elements.Thunder:
                _damage = 2;
                break;
            case SkillManager.Elements.Earth:
                _damage = 2;
                break;
        }

        Destroy(gameObject, _liveDuration);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            switch (targetTag)
            {
                case "Player" :
                    var playerManager = other.GetComponent<PlayerManager>();
                    playerManager.TakeDamage(_damage);
                    break;
                case "Enemy" :                   
                    var enemyManager = other.GetComponent<EnemyManager>();
                    enemyManager.TakeDamage(_damage);
                    //take status
                    break;
            }            
        }
        Instantiate(_destroyFx, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
