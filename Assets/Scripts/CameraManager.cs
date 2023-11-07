using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //camera shake variables
    public float _shakeDuration = 0f;
    public float _shakeMagnitude= 0.2f;

    private Vector3 _originalPosition;



    void Start()
    /*{
     if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform
        }
    }*/


    /*public void ShakeCamera()
    {
        _originalPosition = cameraTransform.position;
    }*/

    void Update()
    {
        if (_shakeDuration > 0)
        {
            cameraTransform.position = _originalPosition + Random.insideUnitCircle * _shakeMagnitude;
            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            _shakeDuration = 0f;
            cameraTransform.position = _originalPosition;
        }

    }


    //IEnumerator CameraShakeRoutine

}
