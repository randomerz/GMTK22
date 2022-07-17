using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juicer : MonoBehaviour
{
    [SerializeField] public float juiceMultiplier;
    [SerializeField] public TrailRenderer cueTrail;
    [SerializeField] public ParticleSystem impactParticleSystem;
    [SerializeField] public ParticleSystem cueHitParticles;

    
    void Update()
    {
        if(juiceMultiplier>0.0f) {
            cueTrail.emitting = true;
            cueTrail.time = 0.5f*juiceMultiplier;
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0.0f, 0.2f);
            curve.AddKey(0.5f, 0.36f*juiceMultiplier);
            curve.AddKey(1.0f, 0.0f);
            cueTrail.widthCurve = curve;
        } else {
            cueTrail.emitting = false;
        }
    }

    private void OnEnable()
    {
        PoolBall.ballInPocketEvent += OnPocket;
        CueBall.ballShot += OnHit;
    }

    private void OnDisable()
    {
        PoolBall.ballInPocketEvent -= OnPocket;
        CueBall.ballShot -= OnHit;
    }

    private void OnHit(object sender, CueBall.BallShotEventArgs e) {
        if (juiceMultiplier > 0.2f){
            CameraShake.Shake(juiceMultiplier,0.8f);
            Smack();
        }
    }

    private void OnPocket(object sender, PoolBall.BallEventArgs e) {
        if (juiceMultiplier > 0.2f){
            CameraShake.Shake(juiceMultiplier,1.5f);
            Yay(e.pocket);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (juiceMultiplier > 0.2f){
            if (other.gameObject.CompareTag("PoolBall")) {
                float mag = other.relativeVelocity.magnitude;
                if(mag > 20) {
                    CameraShake.Shake(juiceMultiplier,0.7f);
                    Spark(1.8f, 60); 
                } else if(mag>10) {
                    CameraShake.Shake(juiceMultiplier,0.5f);
                    Spark(1.0f, 30); 
                } else {
                    CameraShake.Shake(juiceMultiplier,0.2f);
                    Spark(0.2f, 5); 
                }  
            }
        }
    }

    private void Smack() {
        if(juiceMultiplier > 0.4f) {
            cueHitParticles.Play();
        }
    }
    
    private void Yay(GameObject pkt) {
        if(juiceMultiplier > 0.4f) {
            ParticleSystem sparkles = pkt.GetComponentInChildren<ParticleSystem>();;
            var sparklesMain = sparkles.main;
            var sparkleEm = sparkles.emission;
            sparklesMain.startSpeed = Random.Range(33.3f * juiceMultiplier, 66.6f * juiceMultiplier);
            sparkleEm.SetBursts(
                    new ParticleSystem.Burst[]
                    {
                        new ParticleSystem.Burst(0.0f, 166.6f * juiceMultiplier),
                    });
            sparkles.Play();
        }
    }
    private void Spark(float modifier,short numParticles) {
        if(juiceMultiplier > 0.4f) {
            var em = impactParticleSystem.emission;
            var main = impactParticleSystem.main;
            main.startSpeed = Random.Range(1.0f, 20.0f * modifier * juiceMultiplier);
            main.duration = 0.05f * juiceMultiplier;
            Debug.Log(main.duration);
            em.SetBursts(
                new ParticleSystem.Burst[]
                {
                    new ParticleSystem.Burst(0.0f, numParticles),
                });
            impactParticleSystem.Play();
        }
    }

}