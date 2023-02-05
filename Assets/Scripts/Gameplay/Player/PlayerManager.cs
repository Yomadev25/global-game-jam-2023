using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool isUseStamina;

    private FirstPersonController _firstPersonController;
    bool isDead;

    private void Awake()
    {
        MessagingCenter.Subscribe<GameManager>(this, GameManager.MessageGameStart, (sender) =>
        {
            PlayerActive();
        });
    }

    void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
        _firstPersonController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _hp = _maxHp;
        _stamina = _maxStamina;
    }

    void PlayerActive()
    {
        _firstPersonController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        UIManager.instance.UpdateHpBar();
        UIManager.instance.UpdateStaminaBar();

        if (_hp <= 0)
        {
            Gameover();
        }

        if (isUseStamina)
        {
            _stamina -= Time.deltaTime * 3;
        }
        else
        {
            _stamina += Time.deltaTime;
        }

        if (_stamina <= 0)
        {
            _stamina = 0;
        }
        if (_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;
        UIManager.instance.DamageEffect();
    }

    public void Gameover()
    {
        if (isDead) return;
        isDead = true;

        Transition.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
