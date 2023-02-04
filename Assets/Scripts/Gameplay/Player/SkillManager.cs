using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public enum Elements
    {
        None,
        Fire,
        Thunder,
        Earth
    }

    [SerializeField] private GameObject _canvas;
    [SerializeField] private SkillData _skillData;

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

    [Header("Skill Sprites")]
    [SerializeField] private Sprite[] _skillSprites;

    [Header("Fire Prefabs")]
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _fireWave;
    [SerializeField] private GameObject _fireWall;

    [Header("Thunder Prefabs")]
    [SerializeField] private GameObject _thunderBall;
    [SerializeField] private GameObject _thunderWave;
    [SerializeField] private GameObject _thunderWall;

    [Header("Earth Prefabs")]
    [SerializeField] private GameObject _earthBall;
    [SerializeField] private GameObject _earthWave;
    [SerializeField] private GameObject _earthWall;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }           
    }

    public void ActiveSkillTree()
    {
        _canvas.SetActive(true);

        _fireBallUI.color = _skillData.SkillUnlocks[0] ? Color.white : Color.gray;
        _fireWaveUI.color = _skillData.SkillUnlocks[1] ? Color.white : Color.gray;
        _fireWallUI.color = _skillData.SkillUnlocks[2] ? Color.white : Color.gray;

        _thunderBallUI.color = _skillData.SkillUnlocks[3] ? Color.white : Color.gray;
        _thunderWaveUI.color = _skillData.SkillUnlocks[4] ? Color.white : Color.gray;
        _thunderWallUI.color = _skillData.SkillUnlocks[5] ? Color.white : Color.gray;

        _earthBallUI.color = _skillData.SkillUnlocks[6] ? Color.white : Color.gray;
        _earthWaveUI.color = _skillData.SkillUnlocks[7] ? Color.white : Color.gray;
        _earthWallUI.color = _skillData.SkillUnlocks[8] ? Color.white : Color.gray;
    }

    public GameObject GetBall(Elements element)
    {
        GameObject GO = null;
        switch (element)
        {
            case Elements.Fire: GO = _fireBall; break;
            case Elements.Thunder: GO = _thunderBall; break;
            case Elements.Earth: GO = _earthBall; break;
        }
        return GO;
    }

    public GameObject GetWave(Elements element)
    {
        GameObject GO = null;
        switch (element)
        {
            case Elements.Fire: GO = _fireWave; break;
            case Elements.Thunder: GO = _thunderWave; break;
            case Elements.Earth: GO = _earthWave; break;
        }
        return GO;
    }

    public GameObject GetWall(Elements element)
    {
        GameObject GO = null;
        switch (element)
        {
            case Elements.Fire: GO = _fireWall; break;
            case Elements.Thunder: GO = _thunderWall; break;
            case Elements.Earth: GO = _earthWall; break;
        }
        return GO;
    }

    public int GetSkillIndexLength()
    {
        return _skillData.SkillUnlocks.Length;
    }

    public bool GetSkillIndexCheck(int index)
    {
        return _skillData.SkillUnlocks[index];
    }

    public SkillData GetSkillData()
    {
        return _skillData;
    }

    public Sprite GetSkillSprite(int index)
    {
        return _skillSprites[index];
    }
}

[System.Serializable]
public class SkillData
{
    public bool[] SkillUnlocks;
}
