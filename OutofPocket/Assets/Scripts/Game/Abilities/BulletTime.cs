using UnityEngine;
using System.Collections;

public class BulletTime : Singleton<BulletTime> {
  [SerializeField] private float acceleration = 50f;
    [SerializeField] private float minTimeScale;

  public bool AbilityEnabled;

  private void Awake()
  {
    InitializeSingleton();
  }

  void Update(){
        //Debug.Log($"UPDATING BulletTime????? {AbilityEnabled}");
        if (AbilityEnabled)
        {
            float newScale = Time.timeScale;
            if (Input.GetKey(KeyCode.Space))
            {
                newScale -= acceleration * Time.fixedDeltaTime;
                GameManager._instance.powerupAnnotation.enabled = false;
            }
            else
            {
                newScale += acceleration * Time.fixedDeltaTime;
            }
            Time.timeScale = Mathf.Clamp(newScale, minTimeScale, 1f);
        }
  }
}