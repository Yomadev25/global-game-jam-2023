using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _liveDuration = 5f;

    [SerializeField] private string targetTag;

    private void Start()
    {
        Destroy(gameObject, 5f);
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
                case "Player":
                    return;
                case "Enemy":
                    var enemyManager = other.GetComponent<EnemyManager>();
                    enemyManager.TakeDamage(1);
                    return;
            }

            //play effect
            Destroy(this.gameObject);
        }
    }
}
