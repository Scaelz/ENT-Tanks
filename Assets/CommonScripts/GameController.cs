using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    PlayersEagle playersEagle;

    private void Start()
    {
        playersEagle = FindObjectOfType<PlayersEagle>();
        playersEagle.OnEagleDead += EndGame;
    }


    public static void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public static void EndGame()
    {
        OnGameEnded?.Invoke();
    }
}
