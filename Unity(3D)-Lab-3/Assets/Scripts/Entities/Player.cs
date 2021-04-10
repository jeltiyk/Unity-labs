using UnityEngine;

[RequireComponent(typeof(PlayerActionController))]
[RequireComponent(typeof(PCInputController))]
public class Player : Entity
{
    public PlayerActionController ActionController { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();

        ActionController = GetComponent<PlayerActionController>();
    }
}
