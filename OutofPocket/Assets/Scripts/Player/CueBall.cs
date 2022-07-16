using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    [SerializeField] private float shotPower;
    [SerializeField] private Rigidbody rb;

    private bool shotBall;

    private void Update()
    {
        if (OOPInput.horizontal > 0 && !shotBall)
        {
            ShootBall(shotPower, 10f);
            shotBall = true;
        }

    }

    public void ShootBall(float shotPower, float angle)
    {
        float angleInRadians = angle * Mathf.Deg2Rad;
        Vector3 force = shotPower * new Vector3(Mathf.Cos(angleInRadians), 0f, Mathf.Sin(angleInRadians));
        rb.AddForce(force, ForceMode.VelocityChange);   //Independent of mass.
    }
}
