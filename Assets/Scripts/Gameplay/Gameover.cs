using StarterAssets;
using System;
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

    [Header("Row")]
    [SerializeField] private Image[] _activeRow;
    [SerializeField] private Image[] _unactiveRow;

    [SerializeField] private AudioClip _victoryBGM;
    private CanvasGroup canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
        canvas.LeanAlpha(1, 2);

        var player = FindObjectOfType<FirstPersonController>();
        player.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        InitSkillTree();

        var bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        bgm.Stop();
        bgm.clip = _victoryBGM;
        bgm.Play();
    }

    void InitSkillTree()
    {
        int index = 0;
        List<int> activeSkill = new List<int>();
        List<int> unactiveSkill = new List<int>();

        for (int i = 0; i < SkillManager.instance.GetSkillData().SkillUnlocks.Length; i++)
        {
            if (SkillManager.instance.GetSkillData().SkillUnlocks[i])
            {
                activeSkill.Add(i);
            }
            else
            {
                unactiveSkill.Add(i);
            }
        }

        for (int i = 0; i < _activeRow.Length; i++)
        {
            _activeRow[i].sprite = SkillManager.instance.GetSkillSprite(activeSkill[i]);
        }

        for (int i = 0; i < _unactiveRow.Length; i++)
        {
            _unactiveRow[i].sprite = SkillManager.instance.GetSkillSprite(unactiveSkill[i]);
            _unactiveRow[i].color = Color.gray;
        }
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
