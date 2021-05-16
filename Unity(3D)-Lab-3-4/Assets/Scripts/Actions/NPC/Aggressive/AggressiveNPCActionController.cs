using UnityEngine;

[RequireComponent(typeof(AggressiveNPC))]
public class AggressiveNPCActionController : NPCActionController
{
    [SerializeField] protected Transform Target; // temporary solution
    private AggressiveNPC _aggressiveNPC;
    
    protected override void Awake()
    {
        base.Awake();

        _aggressiveNPC = GetComponent<AggressiveNPC>();
    }

    protected override void OnSearchState()
    {
        base.OnSearchState();

        if (Vector3.Distance(Target.position, transform.position) < _aggressiveNPC.FieldOfView)
        {
            Attack();
            return;
        }
    }

    protected override void OnAngryState()
    {
        base.OnAngryState();

        float distanceBetween = Vector3.Distance(Target.position, transform.position);
        
        if (distanceBetween > _aggressiveNPC.FieldOfView)
        {
            _aggressiveNPC.ChangeState(BaseNPCStates.Search);
            ChangeAction(BaseActions.Idle); // temporary solution
            return;
        }

        if (distanceBetween > _aggressiveNPC.AttackRange)
        {
            MoveTo(Target.position, _aggressiveNPC.InteractableController.StoppingDistance);
            return;
        }

        if (distanceBetween <= _aggressiveNPC.AttackRange)
        {
            ChangeAction(BaseActions.Attack);
            return;
        }
    }
}
