using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int _score;
    public int Score { get => _score; set => _score = value; }

    /// <summary>
    /// Adds 0-6 score.
    /// </summary>
    /// <returns>The amount of score added</returns>
    public int AddRandomAmountOfScore()
    {
        int r = Random.Range(0, 7);
        _score += Random.Range(0, 7);
        return r;
    }
}
