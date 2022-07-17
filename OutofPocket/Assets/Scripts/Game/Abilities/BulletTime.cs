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
        Debug.Log($"UPDATING BulletTime????? {AbilityEnabled}");
        if (AbilityEnabled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale -= acceleration * Time.fixedDeltaTime;
            }
            else
            {
                Time.timeScale += acceleration * Time.fixedDeltaTime;
            }
            Time.timeScale = Mathf.Clamp(Time.timeScale, minTimeScale, 1f);
        }
  }
}