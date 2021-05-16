using UnityEngine;

[RequireComponent(typeof(NPC))]
public abstract class NPCActionController : ActionController
{
    private NPC _npc;
    
    protected override void Awake()
    {
        base.Awake();

        _npc = GetComponent<NPC>();
    }

    protected override void Update()
    {
        switch (_npc.CurrentState)
        {
            case BaseNPCStates.None:
                OnNoneState();
                break;
            case BaseNPCStates.Search:
                OnSearchState();
                break;
            case BaseNPCStates.Angry:
                OnAngryState();
                break;
        }
        
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
        
        _npc.ChangeState(BaseNPCStates.Angry);
    }

    public virtual void Search()
    {
        _npc.ChangeState(BaseNPCStates.Search);
    }

    protected virtual void OnNoneState()
    {
        ChangeAction(BaseActions.Idle);
    }
    protected virtual void OnSearchState() { }

    protected virtual void OnAngryState() { }
}
