using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemyCountText;
    [SerializeField] TextMeshProUGUI playerLifesText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateScoreText;
        EnemyCounter.OnEnemyKilled += UpdateEnemyCountText;
        GameController.OnLifesUpdated += UpdateLifesText;
        UpdateScoreText(ScoreManager.score);
        UpdateEnemyCountText(0);
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
