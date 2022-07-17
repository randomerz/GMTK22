using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactParticleScript : MonoBehaviour
{
    [SerializeField] public ParticleSystem impactParticleSystem;
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("PoolBall")) {
            Spark();
        }
    }
    private void Spark() {
        impactParticleSystem.Play();
    }
}
