using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private Transport transport;

    [NonSerialized] public float currentScore = 0;
    [NonSerialized] public float hightScore = 0;

    private void Awake()
    {
        transport = FindAnyObjectByType<Transport>();
    }

    private void Start()
    {
        hightScore = PlayerPrefs.GetFloat("HightScore", 0);
    }

    private void FixedUpdate()
    {
        UpdateScore();

        if (Input.GetKey(KeyCode.O))
        {
            ClearHightScore();
        }
    }

    private void UpdateScore()
    {
        if (transport.transform.position.x > currentScore)
        {
            currentScore = transport.transform.position.x;
        }

        if (currentScore > hightScore)
        {
            hightScore = currentScore;
        }
    }

    private void ClearHightScore()
    {
        if (PlayerPrefs.GetFloat("HightScore", 0) != 0)
        {
            PlayerPrefs.DeleteKey("HightScore");
            PlayerPrefs.Save();
            hightScore = 0;
        }
    }
}
