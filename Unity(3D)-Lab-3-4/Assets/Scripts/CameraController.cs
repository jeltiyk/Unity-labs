using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followObject;
    
    [Space]
    [SerializeField] private float minFOV;
    [SerializeField] private float maxFOV;
    [SerializeField] private float scalingSpeed;

    [Space]
    [SerializeField] private float minOffsetByY = 4;
    [SerializeField] private float maxOffsetByY = 7;
    [Tooltip("Depends of scaling speed")]
    [SerializeField] private float divisionFactor;
    
    [Space]
    [SerializeField] private float smoothing;

    private Camera _camera;

    private float _startFOV;
    private float _currentFOV;

    private Vector3 _cameraOffset;
    
    private void Start()
    {
        _camera = GetComponent<Camera>();
        _startFOV = _currentFOV = _camera.fieldOfView;
        _cameraOffset = transform.position - followObject.position;
    }

    private void LateUpdate()
    {
        _currentFOV = _camera.fieldOfView;

        if (Input.mouseScrollDelta.y > 0)
        {
            _currentFOV -= scalingSpeed;
            
            if(_currentFOV > minFOV)
                _cameraOffset.y -= scalingSpeed / divisionFactor;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            _currentFOV += scalingSpeed;
            
            if(_currentFOV < maxFOV)
                _cameraOffset.y += scalingSpeed / divisionFactor;
        }

        _currentFOV = Mathf.Clamp(_currentFOV, minFOV, maxFOV);
        _cameraOffset.y = Mathf.Clamp(_cameraOffset.y, minOffsetByY, maxOffsetByY);
        
        _camera.fieldOfView = _currentFOV;

        transform.position = Vector3.Lerp(transform.position, followObject.position + _cameraOffset,
            Time.deltaTime / smoothing);
        
        transform.LookAt(new Vector3(followObject.position.x, followObject.position.y, followObject.position.z + 2f));
    }
}
