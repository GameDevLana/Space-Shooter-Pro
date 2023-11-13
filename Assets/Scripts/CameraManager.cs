using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    //camera shake variables
    [SerializeField]
    public float _shakeDuration = 1.0f;
    [SerializeField]
    public float _shakeMagnitude= 0.2f;

    private Vector2 _originalPosition;



    void Start()
    {

        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    public void ShakeCamera()
    {
        _originalPosition = _mainCamera.transform.position;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            _mainCamera.transform.position = _originalPosition + (Random.insideUnitCircle * _shakeMagnitude);
            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            _shakeDuration = 0f;
            _mainCamera.transform.position = _originalPosition;
        }

    }


    //IEnumerator CameraShakeRoutine

}
