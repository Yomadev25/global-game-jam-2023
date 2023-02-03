using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public enum Elements
    {
        Fire,
        Thunder,
        Earth
    }

    [Header("Fire Prefabs")]
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private GameObject _fireWave;
    [SerializeField] private GameObject _fireWall;

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

    public GameObject GetBall(Elements element)
    {
        return _fireBall;
    }

    public GameObject GetWave(Elements element)
    {
        return _fireWave;
    }

    public GameObject GetWall(Elements element)
    {
        return _fireWall;
    }
}
