using System;
using UnityEngine;

public class PCInputController
{
    private ServiceManager _serviceManager;
    
    public Action<Vector3, Collider> LeftPointerClickedHandler = delegate {  };

    private Camera _camera;
    private bool _lMouseBtnClicked;

    public PCInputController()
    {
        _serviceManager = ServiceManager.Instance;
        _serviceManager.UpdateHandler += OnUpdate;
        _serviceManager.LateUpdateHandler += OnLateUpdate;
        _serviceManager.FixedUpdateHandler += OnFixedUpdate;
        _serviceManager.DestroyHandler += OnDestroy;

        _camera = Camera.main;
    }

    private void OnUpdate()
    {
        _lMouseBtnClicked = Input.GetMouseButton(0);
    }

    private void OnLateUpdate()
    {
        
    }

    private void OnFixedUpdate()
    {
        if (_lMouseBtnClicked)
        {
            RaycastHit info;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out info, 100))
                LeftPointerClickedHandler(info.point, info.collider);
        }
    }

    private void OnDestroy()
    {
        _serviceManager.UpdateHandler -= OnUpdate;
        _serviceManager.LateUpdateHandler -= OnLateUpdate;
        _serviceManager.FixedUpdateHandler -= OnFixedUpdate;
        _serviceManager.DestroyHandler -= OnDestroy;
    }
}
