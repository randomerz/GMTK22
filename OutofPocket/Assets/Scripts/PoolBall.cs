using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Or Cube or whatever it may be.
public class PoolBall : MonoBehaviour
{
    public class BallHitEventArgs
    {
        public PoolBall ball;
        public GameObject hitBy;
    }
    public class BallEventArgs
    {
        public PoolBall ball;
    }
    public static event System.EventHandler<BallHitEventArgs> ballHitEvent;
    public static event System.EventHandler<BallEventArgs> ballInPocketEvent;

    private void OnEnable()
    {
        ballHitEvent += DefaultHitEventHandler;
        ballInPocketEvent += DefaultSunkEventHandler;
    }

    private void OnDisable()
    {
        ballHitEvent -= DefaultHitEventHandler;
        ballInPocketEvent -= DefaultSunkEventHandler;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PoolBall"))
        {
            ballHitEvent?.Invoke(this, new BallHitEventArgs
            {
                ball = this,
                hitBy = collision.gameObject
            }); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocket"))
        {
            ballInPocketEvent?.Invoke(this, new BallEventArgs
            {
                ball = this
            });
        }
    }

    private void DefaultHitEventHandler(object sender, BallHitEventArgs e)
    {
        Debug.Log($"{e.ball.gameObject.name} Hit by {e.hitBy.gameObject.name}");
    }

    private void DefaultSunkEventHandler(object sender, BallEventArgs e)
    {
        Debug.Log($"{e.ball.gameObject.name} Sunk!");
    }
}
