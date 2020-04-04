using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static event Action<int> OnScoreChanged;

    private void Awake()
    {
        OnScoreChanged = null;
        GameController.OnGameEnded += NullScore;
    }

    public static void NullScore()
    {
        score = 0;
    }

    public static void AddScore(int value)
    {
        if (GameController.GameStarted)
        {
            score += value;
            OnScoreChanged?.Invoke(score);
        }
    }

}
