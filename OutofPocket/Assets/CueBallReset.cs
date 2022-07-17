using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBallReset : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject cueBall;
    [SerializeField]
    private GameObject triangle;
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
            StartCoroutine(resetCB(other));
        }
        else if (other.transform.parent.parent.gameObject == triangle)
        {
            other.transform.parent.gameObject.GetComponent<PoolBall>().sunk = true;
        }
    }
    IEnumerator resetCB(Collider other)
    {
        yield return new WaitForSeconds(1);
        if(other.transform.parent.transform.position.y < -3.5)
        {
            Debug.Log("reset cue ball from outside");
            manager.ResetCueBall();
        }
    }
}

