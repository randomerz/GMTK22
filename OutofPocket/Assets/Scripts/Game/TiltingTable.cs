using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingTable : MonoBehaviour
{
    public float maxTilt;

    private float accel = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Doesnt work
    void Update()
    {
        float xtilt = 10f;
        float ytilt = 0f;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Hello!");
            xtilt += -maxTilt;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            xtilt += maxTilt;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ytilt += maxTilt;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ytilt += -maxTilt;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(xtilt, ytilt, 0f), Time.deltaTime * accel);
    }
}
