using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] TextMeshProUGUI playerLifesText;
    [SerializeField] TextMeshProUGUI endGameText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateScoreText;
        EnemyCounter.OnEnemyKilled += UpdateEnemyCountText;
        GameController.OnLifesUpdated += UpdateLifesText;
        GameController.OnGameEnded += UpdateEndGameText;
        UpdateScoreText(ScoreManager.score);
        UpdateEnemyCountText(0);
    }

    void UpdateEndGameText(bool isWin)
    {
        string endGameString = "DEFEAT";
        if(endGameText == null)
        {
            return;
        }
        endGameText.gameObject.SetActive(true);
        if (isWin)
        {
            endGameString = "VICTORY";
        }
        endGameText.text = endGameString;
    }

    void UpdateLifesText(int lifesCount)
    {
        if (playerLifesText == null)
            return;
        playerLifesText.text = lifesCount.ToString();
    }

    void UpdateEnemyCountText(int currentScore)
    {
        if (enemyCountText == null)
            return;
        enemyCountText.text = currentScore.ToString();
    }

    void UpdateScoreText(int currentScore)
    {
        if (scoreText == null)
            return;
        scoreText.text = currentScore.ToString();
    }
}
