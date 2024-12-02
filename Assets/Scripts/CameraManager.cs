using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;

    //camera shake variables
    [SerializeField]
    private float _shakeDuration = 1.0f;
    [SerializeField]
    private float _shakeMagnitude= 0.2f;
    private Vector3 _originalPosition;


    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
        _originalPosition = _mainCamera.transform.localPosition;
    }


    public void ShakeCamera()
    {
        StartCoroutine(CameraShakeRoutine());
    }


    IEnumerator CameraShakeRoutine()
    {
        float duration = _shakeDuration;
        while (duration > 0)
        {
            Vector2 shake = Random.insideUnitCircle * _shakeMagnitude;
            Vector3 newpos = new Vector3(shake.x, shake.y, _originalPosition.z);
            _mainCamera.transform.localPosition = newpos;
            duration -= Time.deltaTime;
            yield return null;
        }
        _mainCamera.transform.localPosition = _originalPosition;
    }
}


