using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private static int _score;
    public static int Score { get => _score; set => _score = value; }

    public TextMeshProUGUI scoreText;

    private static bool _isMoneyTime;

    // Use IsMoneyTime to toggle the score setting from Score to Money
    public static bool IsMoneyTime { get => _isMoneyTime; set => _isMoneyTime = value; }




    void Update()
    {
        if (_isMoneyTime)
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
        int r = UnityEngine.Random.Range(0, 7);
        _score += UnityEngine.Random.Range(0, 7);
        return r;
    }

}
