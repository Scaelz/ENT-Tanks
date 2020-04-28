using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int lifesLeft;
    [SerializeField] Texture2D cursor;
    [SerializeField] float sceneSwapDelay;
    public static bool GameStarted = false;

    public static event Action OnGameStarted;
    public static event Action<bool> OnGameEnded;
    public static event Action<int> OnLifesUpdated;
    PlayersEagle playersEagle;
    CameraSetup cameraSetup;
    PlayerController player;
    [SerializeField] Spawn playerSpawn;


    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(16,0), CursorMode.Auto);
        OnLifesUpdated?.Invoke(lifesLeft);
        cameraSetup = FindObjectOfType<CameraSetup>();
        playersEagle = FindObjectOfType<PlayersEagle>();
        playersEagle.OnEagleDead += EndGame;
        PlayerPool.Instance.PreWarm(lifesLeft);
        EnemyCounter.OnAllEnemiesDead += GoToNextLevel;
    }

    public void AddLife()
    {
        lifesLeft++;
        OnLifesUpdated?.Invoke(lifesLeft);
    }

    public void SetPlayer(PlayerController player)
    {
        this.player = player;
        this.player.GetComponent<TankHealth>().OnGotKilled += DecreaseLifes;
    }

    void DecreaseLifes()
    {
        lifesLeft--;
        if(lifesLeft < 0)
        {
            EndGame();
            return;
        }
        OnLifesUpdated?.Invoke(lifesLeft);
        SpawnPlayer();
    }

    private void Update()
    {
        if (!GameStarted)
        {
            if(cameraSetup != null)
            {
                if (cameraSetup.CameraSet)
                {
                    StartGame();
                }
            }
        }
    }

    void GoToNextLevel()
    {
        if (GameStarted)
        {
            OnGameEnded?.Invoke(true);
            FindObjectOfType<LevelChanger>().DelayedFadeToNextLevel(sceneSwapDelay);
            GameStarted = false;
            OnGameEnded = null;
            OnGameStarted = null;
            //;
        }
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
        GameStarted = true;
        cameraSetup.PreventDoubleTrigger();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        StopAllCoroutines();
        playerSpawn.Process(1);
        StartCoroutine(CreatePlayerInstance());
    }

    IEnumerator CreatePlayerInstance()
    {
        yield return new WaitForSeconds(1);
        GameObject go = PlayerPool.Instance.GetInstance(playerSpawn.position, Quaternion.identity);
        go.transform.Rotate(new Vector3(0, 180, 0));
        SetPlayer(go.GetComponent<PlayerController>());
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke(false);
        GameStarted = false;
        OnGameEnded = null;
        OnGameStarted = null;
        FindObjectOfType<LevelChanger>().DelayedFadeToNextLevel(sceneSwapDelay);
    }
}
