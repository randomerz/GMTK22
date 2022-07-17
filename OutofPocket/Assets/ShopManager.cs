using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("References")]
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private Material cueBallRed;
    [SerializeField] private Material cueBallGreen;
    [SerializeField] private Material cueBallBlue;
    [SerializeField] private Material cueBallWhite;
    [SerializeField] private Material cueBallSuperHot;

    [SerializeField] private Material normalBallMaterial;
    [SerializeField] private Material normalBallMaterialSuperHot;

    [SerializeField] private Material feltNormal;
    [SerializeField] private Material feltSuperHot;

    [Header("Properties")]
    [SerializeField] private int colorChangeCost;
    [SerializeField] private int stockCost;

    private int stocksOwned = 0;
    private Material currentlySelectedCueBallMaterial;

    private bool superHotIsEnabled;

    private void Awake()
    {
        InitializeSingleton();
        ScoreManager.Score = 999999;
    }

    private void Start()
    {
        InvokeRepeating("GetStockIncome", 0, 15);
        currentlySelectedCueBallMaterial = cueBallWhite;
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

            Material newMaterial = color.ToLower() switch
            {
                "red" => cueBallRed,
                "blue" => cueBallBlue,
                "green" => cueBallGreen,
                _ => cueBallWhite
            };

            if (!superHotIsEnabled)
            {
                UpdateCueBallMaterial(newMaterial);
            }
            currentlySelectedCueBallMaterial = newMaterial;
        }
    }

    private void UpdatePoolBallsMaterial(Material newMaterial)
    {
        foreach (PoolBall ball in PoolStateManager._instance.PoolBalls)
        {
            if (ball.GetComponent<CueBall>() == null)
            {
                foreach (MeshRenderer meshRenderer in ball.GetComponentsInChildren<MeshRenderer>())
                {
                    meshRenderer.material = newMaterial;
                }
            }
        }
    }

    private void UpdateCueBallMaterial(Material newMaterial)
    {
        foreach (MeshRenderer meshRenderer in PoolStateManager._instance.cueBall.GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderer.material = newMaterial;
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

    public static void SetSuperhotMode(bool enabled)
    {
        Debug.Log(_instance.currentlySelectedCueBallMaterial);
        _instance.superHotIsEnabled = enabled;
        if (enabled)
        {
            _instance.UpdateCueBallMaterial(_instance.cueBallSuperHot);
            _instance.UpdatePoolBallsMaterial(_instance.normalBallMaterialSuperHot);
        } else
        {
            _instance.UpdateCueBallMaterial(_instance.currentlySelectedCueBallMaterial);
            _instance.UpdatePoolBallsMaterial(_instance.normalBallMaterial);
        }
    }

    public static void SetShopActive(bool isActive)
    {
        _instance.shopPanel.SetActive(isActive);
    }
}
