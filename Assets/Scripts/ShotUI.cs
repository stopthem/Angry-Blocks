using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShotUI : MonoBehaviour
{
    public AnimationCurve scaleCurve;

    private CanvasGroup m_canvasGroup;

    public TextMeshProUGUI shotText, shotNumberText;

    private void Awake()
    {
        m_canvasGroup = GetComponent<CanvasGroup>();
        transform.localScale = Vector3.zero;
    }

    public void SetShotText(string text)
    {
        shotText.text = text;
    }

    public void SetShotNumberText(string text)
    {
        shotNumberText.text = text;
    }

    public void Flash()
    {
        m_canvasGroup.alpha = 1;
        transform.localScale = Vector3.zero;
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i <= 60; i++)
        {
            transform.localScale = Vector3.one * scaleCurve.Evaluate((float)i / 50);
            if (i >= 40)
            {
                m_canvasGroup.alpha = (float)(60 - i) / 20;
               
            }
            yield return null;
        }
        yield break;
    }
}
