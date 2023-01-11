using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Camera")]
    public Transform mainCamera;

    [Header("Shake duration")]
    public float shakeDuration = 0f;

    [Header("Amplitude Variables")]
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public bool shakeOnImpact = false;
    
    [Header("Camera position")]
    Vector3 originalPos;

    public static CameraShake instance;
    
    
    
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        
        if (mainCamera == null)
        {
            mainCamera = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = mainCamera.localPosition;
    }

    void Update()
    {
        if (shakeOnImpact) 
        {
            if (shakeDuration > 0) {
                mainCamera.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                shakeDuration = 1f;
                mainCamera.localPosition = originalPos;
                shakeOnImpact = false;
            }
        }
    }

    public void ShakeCamera()
    {
        shakeOnImpact = true;
    }
}
