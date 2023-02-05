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

    [SerializeField] private GameObject _mesh;
    private SkinnedMeshRenderer _meshRenderer;
    private List<Color> originalColor = new List<Color>();

    [SerializeField] private GameObject _dieFx;
    [SerializeField] private GameObject _dmgSFX;

    void Start()
    {
        _meshRenderer = _mesh.GetComponent<SkinnedMeshRenderer>();
        foreach (var material in _meshRenderer.materials)
        {
            originalColor.Add(material.color);
        }

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
        StartCoroutine(DamageEffect());

        onTakeDamage?.Invoke();
    }

    IEnumerator DamageEffect()
    {
        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _meshRenderer.materials[i].color = Color.red;
        }
        GameObject GO = Instantiate(_dmgSFX);
        Destroy(GO, 1);

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _meshRenderer.materials[i].color = originalColor[i];
        }
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
        Instantiate(_dieFx, this.transform.position, Quaternion.identity);
        onDeath?.Invoke();
        Destroy(this.gameObject);
    }
}
