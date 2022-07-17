using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallReset : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject cueBall;
    [SerializeField]
    private PoolStateManager manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("detected obj");
        //Debug.Log(other.gameObject.name);
        if(other.transform.parent.gameObject == cueBall)
        {
            Debug.Log("reset cue ball from outside");
            manager.ResetCueBall();
        }
    }
}
