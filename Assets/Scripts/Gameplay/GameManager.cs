using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const string MessageGameStart = "Game Start";
    public static GameManager instance;

    [SerializeField] private GameObject _randomSkill;
    [SerializeField] private string _sceneName;

    int enemyCount;
    int enemyKilledCount;

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

        enemyCount = FindObjectsOfType<EnemyManager>().Length;
    }

    void RandomSkill()
    {
        if (_randomSkill != null)
            _randomSkill.SetActive(true);
        else
            GameStart();
    }

    public void OnEnemyKilled()
    {
        enemyKilledCount++;

        if(enemyKilledCount >= enemyCount)
        {
            GameCompleted();
        }
    }

    public void GameStart()
    {
        MessagingCenter.Send(this, MessageGameStart);
    }

    public void GameCompleted()
    {
        SkillUnlock();

        Transition.Instance.FadeIn(() =>
        {
            SceneManager.LoadScene(_sceneName);
        });
    }

    void SkillUnlock()
    {
        if (_randomSkill != null)
        {
            _randomSkill.GetComponent<SkillRandom>().UnlockSkill();
        }
    }
}
