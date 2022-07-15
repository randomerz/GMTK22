using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
    [SerializeField] private float forceMag;
    [SerializeField] private Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;

    private void Update()
    {
        PollInput();
        rb.AddForce(new Vector3(horizontalInput, 0, verticalInput) * forceMag * Time.deltaTime);
    }

    private void PollInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
}
