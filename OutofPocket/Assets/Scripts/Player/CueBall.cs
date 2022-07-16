using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    public class BallShotEventArgs
    {
        public CueBall ball;
    }
    [SerializeField] private Transform shotSpot;
    [SerializeField] private Transform popUpSpot;
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
        //Debug.Log($"Ball Shot: {e.ball}");
    }

    private void Update()
    {
        //if (OOPInput.horizontal > 0 && !shotBall)
        //{
        //    ShootBall(shotPower, 10f);
        //    shotBall = true;
        //}

    }

    public void ShootBall(float shotPower, float popUpForce, Vector2 dir)
    {
        Vector3 force = shotPower * new Vector3(dir.x, 0, dir.y);
        rb.AddForceAtPosition(force, shotSpot.position, ForceMode.VelocityChange);   //Independent of mass.
        rb.AddForceAtPosition(popUpForce * Vector3.up, popUpSpot.position, ForceMode.VelocityChange);

        ballShot?.Invoke(this, new BallShotEventArgs()
        {
            ball = this
        });
    }

    public void ShootBall(float shotPower, float popUpForce, float angle)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        ShootBall(shotPower, popUpForce, new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));
    }
}
