using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : PoolBall
{
    public class BallShotEventArgs
    {
        public CueBall ball;
    }
    [SerializeField] private Rigidbody rb;

    public static event System.EventHandler<BallShotEventArgs> ballShot;

    private void OnEnable()
    {
        ballShot += DefaultShotEventHandler;
    }

    private void OnDisable()
    {
        ballShot -= DefaultShotEventHandler;
    }

    private void DefaultShotEventHandler(object sender, BallShotEventArgs e)
    {
        Debug.Log($"Ball Shot: {e.ball}");
    }

    private void Update()
    {
        //if (OOPInput.horizontal > 0 && !shotBall)
        //{
        //    ShootBall(shotPower, 10f);
        //    shotBall = true;
        //}

    }

    public void ShootBall(float shotPower, Vector2 dir)
    {
        Vector3 force = shotPower * new Vector3(dir.x, 0, dir.y);
        rb.AddForce(force, ForceMode.VelocityChange);   //Independent of mass.

        ballShot?.Invoke(this, new BallShotEventArgs()
        {
            ball = this
        });
    }

    public void ShootBall(float shotPower, float angle)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        ShootBall(shotPower, new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));
    }
}
