using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float whenToDestroy;
    void Update()
    {
        if (transform.position.y <= -10)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,whenToDestroy);
        }
    }
}
