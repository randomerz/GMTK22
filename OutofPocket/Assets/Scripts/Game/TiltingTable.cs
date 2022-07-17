using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingTable : MonoBehaviour
{

    public float accel = 5f;
    public float maxXTilt = 10f; //degrees
    public float maxZTilt = 5f; // degrees

    private float lastXTarget = 0;
    private float lastZTarget = 0;
    //private float xt=0;
    //private float zt=0;
    private float t = 0;
    private Quaternion startRot;
    // Start is called before the first frame update

    public bool TiltingEnabled = false;
    void Start()
    {
        startRot = transform.rotation;
    }

    // Update is called once per frame
    // Doesnt work
    void Update()
    {

        //float xtilt = 10f;
        //float ztilt = 0f;
        float xTarget = 0;
        float zTarget = 0;
        if (TiltingEnabled)
        {
            if (OOPInput.vertical < 0)
            {
                //Debug.Log("Hello!");
                xTarget += -maxXTilt;
                //Debug.Log(xtilt);
            }
            if (OOPInput.vertical > 0)
            {
                xTarget += maxXTilt;
            }
            if (OOPInput.horizontal > 0)
            {
                zTarget += -maxZTilt;
            }
            if (OOPInput.horizontal < 0)
            {
                zTarget += maxZTilt;
            }
        }
        //Debug.Log(xtilt);
        if (lastXTarget != xTarget || lastZTarget != zTarget)
        {
            startRot = transform.rotation;
            t = 0;
            //Debug.Log("Resetting Target");
        }

        transform.rotation = Quaternion.Slerp(startRot, Quaternion.Euler(xTarget, 0, zTarget), t);
        //Debug.Log(Quaternion.Slerp(startRot, Quaternion.Euler(xTarget, 0, zTarget), t));
        //xt += Time.deltaTime * accel;
        //zt += Time.deltaTime * accel;
        t += Time.deltaTime * accel;
        lastXTarget = xTarget;
        lastZTarget = zTarget;
        
    }
}
