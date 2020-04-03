using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float sceneSwapDelay;
    bool GameStarted = false;

    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    PlayersEagle playersEagle;
    CameraSetup cameraSetup;

    private void Start()
    {
        cameraSetup = FindObjectOfType<CameraSetup>();
        playersEagle = FindObjectOfType<PlayersEagle>();
        playersEagle.OnEagleDead += EndGame;
    }

    private void Update()
    {
        if (!GameStarted)
        {
            if (cameraSetup.CameraSet)
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        GameStarted = true;
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke();
        FindObjectOfType<LevelChanger>().DelayedFadeToLevel(0, sceneSwapDelay);
    }
}
