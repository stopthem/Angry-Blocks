using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float whenToDestroy;
    void Update()
    {
        Destroy(gameObject, whenToDestroy);
    }
}
