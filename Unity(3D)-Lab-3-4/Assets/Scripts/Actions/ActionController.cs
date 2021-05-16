using UnityEngine;

public abstract class ActionController : MonoBehaviour
{
    private Entity _entity;
    
    public BaseActions CurrentAction { get; private set; }

    protected virtual void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    protected virtual void Update()
    {
        if(CurrentAction == BaseActions.Move)
        {
            if (Vector3.Distance(_entity.transform.position, _entity.NavMeshAgent.destination) <=
             _entity.NavMeshAgent.stoppingDistance)
            {
                ChangeAction(BaseActions.Idle);
                _entity.NavMeshAgent.SetDestination(_entity.transform.position);
            }
        }
    }

    private void ResetAction()
    {
        if(CurrentAction != BaseActions.Idle)
            _entity.Animator.ResetTrigger(CurrentAction.ToString());

        CurrentAction = BaseActions.Idle;
    }
    
    protected void ChangeAction(BaseActions action)
    {
        if(action == CurrentAction) return;
       
        ResetAction();
        CurrentAction = action;
        _entity.Animator.SetTrigger(CurrentAction.ToString());
    }

    public virtual void MoveTo(Vector3 destination, float stoppingDistance = 0.4f)
    {
        ChangeAction(BaseActions.Move);

        _entity.Animator.SetTrigger(BaseActions.Move.ToString());

        _entity.NavMeshAgent.SetDestination(destination);
        _entity.NavMeshAgent.stoppingDistance = stoppingDistance;
    }
    
    public virtual void Follow(Transform target, bool stop = false, float stoppingDistance = 0.4f) { }

    public virtual void Attack()
    {
        ChangeAction(BaseActions.Attack);
    }
    
    public virtual void Defence()
    {
        ChangeAction(BaseActions.Defence);
    }
}
