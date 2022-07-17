using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform baseTransform;

    public static CameraShake _instance;
    
    private static float curIntensity;


    void Awake()
    {
        if (baseTransform == null)
        {
            Debug.LogWarning("Camera Shake is missing base!");
        }

        _instance = this;
    }

    public static void Shake(float globalMod, float localMod)
    {
        /*if (_instance == null || amount < curIntensity)
            return;*/
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.cShake(globalMod, localMod));
    }

    public IEnumerator cShake(float juiceLevel, float shakeActionMod)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < 0.2f * (1f+juiceLevel) * shakeActionMod) {
            float x = Random.Range(-1f, 1f) * 0.3f * (1f+juiceLevel) * shakeActionMod;
            float z = Random.Range(-1f, 1f) * 0.3f * (1f+juiceLevel) * shakeActionMod;
            transform.position = new Vector3(x, originalPos.y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = new Vector3(0,7,0);
    }

}