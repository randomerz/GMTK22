using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour
{
    public Light light;
    private Vector3 defaultPosition;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();   
        defaultPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        light.range = 40 * Mathf.Sin(Time.time * 0.5f) + 200;
        transform.localPosition = new Vector3(defaultPosition.x + 0.25f * Mathf.Sin(Time.time * 0.5f), defaultPosition.y, defaultPosition.z);
    }
}
