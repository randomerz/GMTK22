using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndOfGame : Singleton<EndOfGame>
{
    private GameObject triangle;
    public PhysicMaterial[] randomMats = new PhysicMaterial[3];
    public Material[] colors = new Material[3];
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSingleton();
        triangle = GameObject.Find("/PoolTable/Triangle");
        random = new System.Random(); 
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time > 1 && Time.time < 1.05)
        // {
        //     EndOfGameify();
        // }
    }
    public void EndOfGameify()
    {
        Debug.Log("END OF GAME");
        PoolStateManager.ChangeAllToShape(PoolBall.Shape.Sphere);
        ShopManager.SetSuperhotMode(false);
        foreach (Transform poolBall in triangle.transform) 
        {

            GameObject shape = poolBall.GetComponent<PoolBall>().GetCurShape();
            int r = random.Next(0, 3);
            shape.GetComponent<Collider>().material = randomMats[r];
            shape.GetComponent<MeshRenderer>().materials =  new Material[] {colors[r]};
        }
    }
}
