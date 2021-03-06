using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [Header("References")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject billiardsTable;

    [SerializeField] private Material cueBallRed;
    [SerializeField] private Material cueBallGreen;
    [SerializeField] private Material cueBallBlue;
    [SerializeField] private Material cueBallWhite;
    [SerializeField] private Material cueBallSuperHot;

    [SerializeField] private Material normalBallMaterial;
    [SerializeField] private Material normalBallMaterialSuperHot;

    [SerializeField] private Material feltNormal;
    [SerializeField] private Material feltSuperHot;

    [SerializeField] private Material pocketNormal;

    [Header("Properties")]
    [SerializeField] private int redCost;
    [SerializeField] private int greenCost;
    [SerializeField] private int blueCost;
    [SerializeField] private int stockCost;

    private int stocksOwned = 0;
    private Material currentlySelectedCueBallMaterial;

    public bool superHotIsEnabled;  //Don't change back to private plz


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
        Material newMaterial = color.ToLower() switch
        {
            "red" => cueBallRed,
            "blue" => cueBallBlue,
            "green" => cueBallGreen,
            _ => cueBallWhite
        };
        int cost = color.ToLower() switch
        {
            "red" => redCost,
            "blue" => blueCost,
            "green" => greenCost,
            _ => 0
        };

        if (ScoreManager.Score >= cost)
        {
            ScoreManager.Score -= cost;

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

    private void UpdatePoolTableFeltMaterial(Material newMaterial)
    {
        MeshRenderer mr = billiardsTable.GetComponent<MeshRenderer>();
        Debug.Log($"BILLIARDS: {newMaterial}");
        Material[] intMaterials = new Material[mr.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {
            if (i != 4)
            {
                intMaterials[i] = mr.materials[i];
            } else
            {
                intMaterials[i] = newMaterial;
            }
        }
        mr.materials = intMaterials;
    }

    private void UpdatePoolTablePocketMaterial(Material newMaterial)
    {
        MeshRenderer mr = billiardsTable.GetComponent<MeshRenderer>();
        Debug.Log($"BILLIARDS: {newMaterial}");
        Material[] intMaterials = new Material[mr.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {
            if (i != 0)
            {
                intMaterials[i] = mr.materials[i];
            }
            else
            {
                intMaterials[i] = newMaterial;
            }
        }
        mr.materials = intMaterials;
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
        SuperHotAudio.isSuperHotOn = enabled;
        if (enabled)
        {
            _instance.UpdateCueBallMaterial(_instance.cueBallSuperHot);
            _instance.UpdatePoolBallsMaterial(_instance.normalBallMaterialSuperHot);
            _instance.UpdatePoolTableFeltMaterial(_instance.feltSuperHot);
            _instance.UpdatePoolTablePocketMaterial(_instance.feltSuperHot);
        } else
        {
            _instance.UpdateCueBallMaterial(_instance.currentlySelectedCueBallMaterial);
            _instance.UpdatePoolBallsMaterial(_instance.normalBallMaterial);
            _instance.UpdatePoolTableFeltMaterial(_instance.feltNormal);
            _instance.UpdatePoolTablePocketMaterial(_instance.pocketNormal);
        }
    }

    public static void SetShopActive(bool isActive)
    {
        _instance.shopPanel.SetActive(isActive);
    }
}
