using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private Transport transport;

    [NonSerialized] public float score = 0;

    private void Awake()
    {
        transport = FindAnyObjectByType<Transport>();
    }

    private void FixedUpdate()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (transport.transform.position.x > score)
        {
            score = transport.transform.position.x;
        }
    }
}
