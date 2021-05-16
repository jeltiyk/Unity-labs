using UnityEngine;

[RequireComponent(typeof(NPCActionController))]
[RequireComponent(typeof(NPCInteractable))]
public abstract class NPC : Entity
{
    [SerializeField] private float fieldOfView;
      
    public BaseNPCStates CurrentState { get; private set; }
    public float FieldOfView => fieldOfView;

    protected override void Awake()
    {
        base.Awake();

        //temporary solution
        NPCInteractable interactable  = GetComponent<NPCInteractable>();
        if (fieldOfView > interactable.InteractionRadius)
            fieldOfView = interactable.InteractionRadius;
    }

    public void ChangeState(BaseNPCStates state)
    {
        if(CurrentState == state) return;
    
        CurrentState = state;
    }
}
