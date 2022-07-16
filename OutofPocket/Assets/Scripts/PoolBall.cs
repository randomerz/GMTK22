using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Or Cube or whatever it may be.
public class PoolBall : MonoBehaviour
{
    public class BallInPocketEventArgs
    {
        public PoolBall ballSunk;
    }
    public static event System.EventHandler<BallInPocketEventArgs> ballInPocketEvent;

    private void OnEnable()
    {
        ballInPocketEvent += DefaultEventHandler;
    }

    private void OnDisable()
    {
        ballInPocketEvent -= DefaultEventHandler;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocket"))
        {
            ballInPocketEvent.Invoke(this, new BallInPocketEventArgs
            {
                ballSunk = this
            });
        }
    }

    private void DefaultEventHandler(object sender, BallInPocketEventArgs e)
    {
        Debug.Log($"{e.ballSunk.gameObject.name} Sunk!");
    }
}
