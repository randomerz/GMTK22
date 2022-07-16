using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("References")]
    [SerializeField] private Material cueBallMaterial;

    [Header("Properties")]
    [SerializeField] private int colorChangeCost;
    [SerializeField] private int stockCost;

    private int stocksOwned = 0;

    private void Start()
    {
        InvokeRepeating("GetStockIncome", 0, 15);
    }

    private void GetStockIncome()
    {
        ScoreManager.Score += Random.Range(0, stocksOwned);
    }

    public void SetCueBallColor(string color)
    {
        if (ScoreManager.Score >= colorChangeCost)
        {
            ScoreManager.Score -= colorChangeCost;

            cueBallMaterial.color = color.ToLower() switch
            {
                "red" => Color.red,
                "blue" => Color.blue,
                "green" => Color.green,
                _ => Color.white
            };
        }
    }

    public void BuyStocks()
    {
        if (ScoreManager.Score >= stockCost)
        {
            ScoreManager.Score -= stockCost;
            stocksOwned++;
        }
    }

    public void SaveForRetirement()
    {
        ScoreManager.Score = 0; // ehlmao
    }
}
