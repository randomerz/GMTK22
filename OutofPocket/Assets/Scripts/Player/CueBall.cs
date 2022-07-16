using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : PoolBall
{
    [SerializeField] private Rigidbody rb;

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
    }

    public void ShootBall(float shotPower, float angle)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        ShootBall(shotPower, new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)));
    }
}
