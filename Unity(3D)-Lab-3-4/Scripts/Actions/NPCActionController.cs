using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCActionController : ActionController
{
    private NPC _npc;
    private NPCInteractable _interactable;
    
    protected override void Start()
    {
        base.Start();

        _npc = GetComponent<NPC>();
        _interactable = GetComponent<NPCInteractable>();
    }
    
    public override void Attack(Transform target)
    {
        if (Vector3.Distance(target.position, transform.position) > _npc.AttackRange)
        {
            FollowTarget(target, false, _npc.AttackRange);
            return;
        }
        
        base.Attack(target);
    }

    public override void FollowTarget(Transform target, bool stopFollow = false, float stoppingDistance = 0.4f)
    {
        base.FollowTarget(target, stopFollow, stoppingDistance);
        
        if (Vector3.Distance(target.position, transform.position) > _interactable.InteractionRadius * 1.5f)
        {
            ChangeAction(ActionType.Idle);
            _npc.ChangeState(NPC.State.None);
            return;
        }
    }
}
