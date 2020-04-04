using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI enemyCountText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.OnScoreChanged += UpdateScoreText;
        EnemyCounter.OnEnemyKilled += UpdateEnemyCountText;
        UpdateScoreText(ScoreManager.score);
        UpdateEnemyCountText(0);
    }

    void UpdateEnemyCountText(int currentScore)
    {
        enemyCountText.text = currentScore.ToString();
    }

    void UpdateScoreText(int currentScore)
    {
        scoreText.text = currentScore.ToString();
    }
}
