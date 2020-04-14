using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] float sceneSwapDelay;
    public static bool GameStarted = false;

    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    PlayersEagle playersEagle;
    CameraSetup cameraSetup;
    PlayerController player;

    private void Start()
    {
        cameraSetup = FindObjectOfType<CameraSetup>();
        playersEagle = FindObjectOfType<PlayersEagle>();
        playersEagle.OnEagleDead += EndGame;
        EnemyCounter.OnAllEnemiesDead += GoToNextLevel;
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
        this.player.GetComponent<TankHealth>().OnGotKilled += EndGame;
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

    void GoToNextLevel()
    {
        if (GameStarted)
        {
            FindObjectOfType<LevelChanger>().DelayedFadeToNextLevel(sceneSwapDelay);
        }
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        GameStarted = true;
        cameraSetup.PreventDoubleTrigger();
    }

    public void EndGame()
    {
        GameStarted = false;
        OnGameEnded?.Invoke();
        OnGameEnded = null;
        OnGameStarted = null;
        FindObjectOfType<LevelChanger>().DelayedFadeToLevel(0, sceneSwapDelay);
    }
}
