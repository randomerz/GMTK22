using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private static int _score;
    public static int Score { get => _score; set => _score = value; }

    public TextMeshProUGUI scoreText;

    private bool isMoneyTime;

    void Update()
    {
        if (isMoneyTime)
        {
            scoreText.text = "Money: $" + _score.ToString();
        }
        else
        {
            scoreText.text = "Score: " + _score.ToString();

        }
    }

    /// <summary>
    /// Adds 0-6 score.
    /// </summary>
    /// <returns>The amount of score added</returns>
    public static int AddRandomAmountOfScore()
    {
        int r = Random.Range(0, 7);
        _score += Random.Range(0, 7);
        return r;
    }

    public 
}
