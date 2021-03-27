using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    #region Singleton

    public static ServiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    #endregion

    public PCInputController InputController { get; private set; }
    
    public bool Pause { get; private set; }

    public Action DestroyHandler = delegate {  };   
    public Action UpdateHandler = delegate {  };
    public Action LateUpdateHandler = delegate {  };
    public Action FixedUpdateHandler = delegate {  };

    private void Start()
    {
        InputController = new PCInputController();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Pause)
            SetPause(false);
        else if(Input.GetKeyUp(KeyCode.Escape) && !Pause)
            SetPause(true);

        if(Pause) return;
        UpdateHandler();
    }

    private void LateUpdate()
    {
        if(Pause) return;
        LateUpdateHandler();
    }

    private void FixedUpdate()
    {
        FixedUpdateHandler();
    }

    public void SetPause(bool pauseValue)
    {
        Time.timeScale = pauseValue ? 0 : 1;

        Pause = pauseValue;
    }

    private void OnDestroy()
    {
        
    }
}
