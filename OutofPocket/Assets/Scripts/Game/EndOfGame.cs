using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EndOfGame : MonoBehaviour
{
    private GameObject triangle;
    public PhysicMaterial[] randomMats = new PhysicMaterial[3];
    private System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        triangle = GameObject.Find("/Triangle");
        random = new System.Random(); 
    }

    // Update is called once per frame
    void Update()
    {
        // if(Time.time > 1 && Time.time < 1.25)
        // {
        //     EndOfGameify();
        // }
    }
    public void EndOfGameify()
    {
        Debug.Log("END OF GAME");
        GetComponent<PoolStateManager>().ChangeAllToShape(PoolBall.Shape.Sphere);
        foreach (Transform poolBall in triangle.transform) 
        {

            GameObject shape = poolBall.GetComponent<PoolBall>().GetCurShape();
            shape.GetComponent<Collider>().material = randomMats[random.Next(0, 3)];
        }
    }
}
