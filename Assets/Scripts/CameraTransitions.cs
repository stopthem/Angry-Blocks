using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    private Transform m_containerTransform;

    private float m_rotateSemiAmount = 4;
    private float m_shakeAmount;

    private Vector3 m_startingPosition;

    private void Start()
    {
        m_containerTransform = GameObject.Find("CameraContainer").transform;
    }

    private void Update()
    {
        if (m_shakeAmount > 0.01f)
        {
            Vector3 localPosition = m_startingPosition;
            localPosition.x += m_shakeAmount * Random.Range(3, 5);
            localPosition.y += m_shakeAmount * Random.Range(3, 5);
            transform.localPosition = localPosition;
            m_shakeAmount = 0.9f * m_shakeAmount;
        }
    }

    public void Shake()
    {
        m_shakeAmount = Mathf.Min( .1f, m_shakeAmount + 0.01f);
    }
    
    public void MediumShake()
    {
        m_shakeAmount = Mathf.Min( .1f, m_shakeAmount + 0.015f);
    }

    public void RotateCameraToSide()
    {
        StartCoroutine(RotateCameraToSideRoutine());
    }

    public void RotateCameraToFront()
    {
        StartCoroutine(RotateCameraToFrontRoutine());
    }

    private IEnumerator RotateCameraToSideRoutine()
    {
        int frames = 20;
        float increment = m_rotateSemiAmount / (float)frames;
        for (int i = 0; i < frames; i++)
        {
            m_containerTransform.RotateAround(Vector3.zero, Vector3.up, increment);
            yield return null;
        }

        yield break;
    }

    private IEnumerator RotateCameraToFrontRoutine()
    {
        int frames = 60;
        float increment = m_rotateSemiAmount / (float)frames;
        for (int i = 0; i < frames; i++)
        {
            m_containerTransform.RotateAround(Vector3.zero, Vector3.up, -increment);
            yield return null;
        }
        m_containerTransform.localEulerAngles = new Vector3(0,0,0);
        yield break;
    }
}
