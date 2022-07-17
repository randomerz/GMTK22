using UnityEngine;
using System.Collections;

public class BigCue : Singleton<BigCue> {
  [SerializeField] private Vector3 acceleration = new Vector3(1f, 1f, 1f);
  [SerializeField] private Transform TargetObject;

  private Vector3 defaultScale;

  private void Awake()
  {
    InitializeSingleton();
    defaultScale = TargetObject.localScale;
  }

  void Update(){
    Vector3 scale = TargetObject.localScale;

    if(Input.GetKey(KeyCode.Space)){
      scale +=  acceleration * Time.fixedDeltaTime;
    }
    else{
      scale -=  acceleration * Time.fixedDeltaTime;
    }
    TargetObject.localScale = new Vector3(Mathf.Clamp(scale.x, defaultScale.x, defaultScale.x * 2), 
                                          Mathf.Clamp(scale.y, defaultScale.y, defaultScale.y * 2), 
                                          Mathf.Clamp(scale.z, defaultScale.z, defaultScale.z * 2));
  }
}