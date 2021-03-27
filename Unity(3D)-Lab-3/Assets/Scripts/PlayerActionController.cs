using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : ActionController
{
    private Player _player;
    
    public PlayerActionController(Player player) : base(player)
    {
        _player = player;
        _player.ServiceManager.InputController.LeftPointerClickedHandler += OnLeftPointerClicked;
    }
    
    private void OnLeftPointerClicked(Vector3 targetPosition, Collider targetCollider)
    {
        if(targetCollider != null) Debug.Log(targetCollider);

        Move(targetPosition);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        _player.ServiceManager.InputController.LeftPointerClickedHandler -= OnLeftPointerClicked;
    }
}
