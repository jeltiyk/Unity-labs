using UnityEngine;

public enum ActionType
{
     Idle,
     Move,
     Attack,
     Hurt,
     Death,
}

public interface IActionController
{
    void ChangeAction(ActionType newAction);
    void ResetAction();
    void Move(Vector3 targetPosition, float stoppingDistance = 0.4f);
    void FollowTarget(Transform target, bool stopFollow = false, float stoppingDistance = 0.4f);
    void Attack(Transform target);
}

public abstract class ActionController : MonoBehaviour, IActionController
{
    private Entity _entity;
    protected ActionType Current;

    protected virtual void Start()
    {
        _entity = GetComponent<Entity>();
    }
    
    protected virtual void Update()
    {
        if (Current == ActionType.Move)
        {
            if (Vector3.Distance(_entity.transform.position, _entity.NavMeshAgent.destination) <=
                _entity.NavMeshAgent.stoppingDistance)
            {
                ChangeAction(ActionType.Idle);
                _entity.NavMeshAgent.destination = _entity.transform.position;
            }
        }
    }

    public void ChangeAction(ActionType newAction)
    {
        if (Current == newAction) return;
        
        ResetAction();

        Current = newAction;
        _entity.Animator.SetTrigger(Current.ToString());    // _entity.Animator.SetBool(Current.ToString(), true);
    }

    public void ResetAction()
    {
        if(Current != ActionType.Idle)
            _entity.Animator.ResetTrigger(Current.ToString());  // _entity.Animator.SetBool(Current.ToString(), false);

        Current = ActionType.Idle;
    }

    public virtual void Move(Vector3 targetPosition, float stoppingDistance = 0.4f)
    {
        if(Current != ActionType.Move)
            ChangeAction(ActionType.Move);

        _entity.NavMeshAgent.SetDestination(targetPosition);
        _entity.NavMeshAgent.stoppingDistance = stoppingDistance;
    }

    public virtual void Attack(Transform target)
    {
        if(Current != ActionType.Attack)
            ChangeAction(ActionType.Attack);
    }

    public virtual void FollowTarget(Transform target, bool stopFollow = false, float stoppingDistance = 0.4f)
    {
        Move(target.position, stoppingDistance);

        // forced stop
        if (stopFollow)
        {
            ChangeAction(ActionType.Idle);
            return;
        }
        
        if (Vector3.Distance(target.transform.position, _entity.transform.position) <=
            _entity.NavMeshAgent.stoppingDistance)
        {
            ChangeAction(ActionType.Idle);
            return;
        }
    }
}
