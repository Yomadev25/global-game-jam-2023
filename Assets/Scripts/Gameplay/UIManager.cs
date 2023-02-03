using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _staminaBar;

    private PlayerManager playerManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void UpdateHpBar()
    {
        _hpBar.fillAmount = playerManager.HP / playerManager.MaxHp;
    }

    public void UpdateStaminaBar()
    {
        _staminaBar.fillAmount = playerManager.Stamina / playerManager.MaxStamina;
    }
}
