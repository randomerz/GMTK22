using UnityEngine;
using System.Collections;

public class BulletTime : Singleton<BulletTime> {
  [SerializeField] private float acceleration = 0.2f;

  private void Awake()
  {
    InitializeSingleton();
  }

  void Update(){
    if(Input.GetKey(KeyCode.Space)){
      Time.timeScale -= acceleration * Time.fixedDeltaTime;
    }
    else{
      Time.timeScale += acceleration * Time.fixedDeltaTime;
    }
    Time.timeScale = Mathf.Clamp(Time.timeScale, 0.5f, 1f);
  }
}