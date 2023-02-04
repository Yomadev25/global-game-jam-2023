using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _staminaBar;

    [Header("Skill Panel")]
    [SerializeField] private Image _skill1Icon;
    [SerializeField] private Image _skill2Icon;
    [SerializeField] private Image _skill3Icon;

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

        InitSkillIcon();
    }

    void InitSkillIcon()
    {
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < SkillManager.instance.GetSkillIndexLength(); i++)
        {
            if (SkillManager.instance.GetSkillData().SkillUnlocks[i])
            {
                sprites.Add(SkillManager.instance.GetSkillSprite(i));
            }
        }

        _skill1Icon.sprite = sprites[0];
        _skill2Icon.sprite = sprites[1];
        _skill3Icon.sprite = sprites[2];
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
