using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Block : MonoBehaviour
{
    private int m_count;

    private Text countText;

    private AudioSource bounceSound;


    private void Awake()
    {
        countText = GetComponentInChildren<Text>();
        bounceSound = GameObject.Find("BounceSound").GetComponent<AudioSource>();
    }
    private void Update()
    {
        CheckIfOut();
    }

    private void CheckIfOut()
    {
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
    }

    public void SetStartingCount(int count)
    {
        m_count = count;
        countText.text = count.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Ball" && m_count > 0)
        {
            bounceSound.Play();
            m_count--;
            Camera.main.GetComponent<CameraTransitions>().Shake();
            countText.text = m_count.ToString();
            if (m_count == 0)
            {
                Destroy(gameObject);
                Camera.main.GetComponent<CameraTransitions>().MediumShake();
                GameObject.Find("ExtraBaLLImage").GetComponent<ExtraBallBar>().IncreaseCurrentWidth();
            }
        }
    }
}
