using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juicer : MonoBehaviour
{
    [SerializeField] public float juiceMultiplier;
    //[SerializeField] public TrailRenderer CueTrail;
    [SerializeField] public ParticleSystem impactParticleSystem;
    
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("PoolBall")) {
            //Debug.Log("shake");
            CameraShake.Shake(juiceMultiplier,0.5f);
            Spark();       
        }
    }
    private void Spark() {
        impactParticleSystem.Play();
    }

}