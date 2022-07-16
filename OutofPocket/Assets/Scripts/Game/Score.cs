using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Awake()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public int AddScore()
    {
        int r = Random.Range(0, 7);
        score += Random.Range(0, 7);
        return r;
    }

    public int GetScore()
    {
        return score;
    }

}
