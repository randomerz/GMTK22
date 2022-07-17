using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMan : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private float timeTilWalk = 50;
    private Vector3 startingLocation;

    // Start is called before the first frame update
    void Start()
    {
        startingLocation = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        timeTilWalk -= Time.deltaTime;
        if (timeTilWalk <= 0){
            timeTilWalk = Random.Range(10, 31);
            transform.localPosition = startingLocation;
        }
    }
}
