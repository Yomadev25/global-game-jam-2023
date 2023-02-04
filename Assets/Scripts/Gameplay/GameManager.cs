using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string MessageGameStart = "Game Start";
    public static GameManager instance;

    [SerializeField] private GameObject _randomSkill;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        Transition.Instance.FadeOut();
        RandomSkill();
    }

    void RandomSkill()
    {
        _randomSkill.SetActive(true);
    }

    public void GameStart()
    {
        MessagingCenter.Send(this, MessageGameStart);
    }

    public void GameCompleted()
    {
        SkillUnlock();
    }

    void SkillUnlock()
    {
        _randomSkill.GetComponent<SkillRandom>().UnlockSkill();
    }
}
