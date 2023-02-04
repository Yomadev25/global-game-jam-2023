using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [Header("Fire Skill Tree")]
    [SerializeField] private Image _fireBallUI;
    [SerializeField] private Image _fireWaveUI;
    [SerializeField] private Image _fireWallUI;

    [Header("Thunder Skill Tree")]
    [SerializeField] private Image _thunderBallUI;
    [SerializeField] private Image _thunderWaveUI;
    [SerializeField] private Image _thunderWallUI;

    [Header("Earth Skill Tree")]
    [SerializeField] private Image _earthBallUI;
    [SerializeField] private Image _earthWaveUI;
    [SerializeField] private Image _earthWallUI;

    private CanvasGroup canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
        canvas.LeanAlpha(1, 2);

        ActiveSkillTree();
    }

    public void ActiveSkillTree()
    {
        _fireBallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[0] ? Color.white : Color.gray;
        _fireWaveUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[1] ? Color.white : Color.gray;
        _fireWallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[2] ? Color.white : Color.gray;

        _thunderBallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[3] ? Color.white : Color.gray;
        _thunderWaveUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[4] ? Color.white : Color.gray;
        _thunderWallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[5] ? Color.white : Color.gray;

        _earthBallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[6] ? Color.white : Color.gray;
        _earthWaveUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[7] ? Color.white : Color.gray;
        _earthWallUI.color = SkillManager.instance.GetSkillData().SkillUnlocks[8] ? Color.white : Color.gray;
    }

    public void OnClickMenu()
    {
        Transition.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}
