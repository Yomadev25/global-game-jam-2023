using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }
    [SerializeField] private CanvasGroup _panel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    public void FadeOut(Action callback = null, float delay = 1)
    {
        StartCoroutine(FadeOutCoroutine(callback, delay));
    }

    IEnumerator FadeOutCoroutine(Action callback = null, float delay = 1)
    {
        _panel.LeanAlpha(0, 1f);
        yield return new WaitForSeconds(delay);
        if (callback != null) callback();
    }

    public void FadeIn(Action callback = null, float delay = 1)
    {
        StartCoroutine(FadeInCoroutine(callback, delay));
    }

    IEnumerator FadeInCoroutine(Action callback = null, float delay = 1)
    {
        _panel.LeanAlpha(1, 1f);
        yield return new WaitForSeconds(delay);
        if (callback != null) callback();
    }
}
