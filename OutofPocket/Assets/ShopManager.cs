using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private Material cueBallMaterial;

    private void Start()
    {
        SetCueBallColor(CueBallColor.Green);
    }

    public void SetCueBallColor(CueBallColor newColor)
    {
        cueBallMaterial.color = newColor switch
        {
            CueBallColor.Red => Color.red,
            CueBallColor.Blue => Color.blue,
            CueBallColor.Green => Color.green,
            _ => Color.white
        };
    }
}

public enum CueBallColor
{
    White,
    Red,
    Blue,
    Green
}
