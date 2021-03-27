using UnityEngine;


public abstract class ActionController
{
    protected enum ActionType
    {
        Idle,
        Run,
        Attack,
        Hurt,
        Death
    }
    
    private Entity _entity;
    private ActionType _currentAction;

    protected ActionController(Entity entity)
    {
        _entity = entity;
        _entity.ServiceManager.DestroyHandler += OnDestroy;
        _entity.ServiceManager.FixedUpdateHandler += OnFixedUpdate;

    }

    protected virtual void OnFixedUpdate()
    {
        if (Vector3.Distance(_entity.transform.position, _entity.NavMeshAgent.destination) <=
            _entity.NavMeshAgent.stoppingDistance)
        {
            ChangeAction(ActionType.Idle);
            _entity.NavMeshAgent.destination = _entity.transform.position;
        }
    }

    protected virtual void OnDestroy()
    {
        _entity.ServiceManager.DestroyHandler -= OnDestroy;
        _entity.ServiceManager.FixedUpdateHandler -= OnFixedUpdate;
    }

    protected virtual void Move(Vector3 targetPosition)
    {
        _entity.NavMeshAgent.destination = targetPosition;
        ChangeAction(ActionType.Run);
    }
    
    protected virtual void ChangeAction(ActionType actionType)
    {
        ResetAction();
        
        _currentAction = actionType;
        _entity.Animator.SetBool(_currentAction.ToString(), true);
    }

    protected virtual void ResetAction()
    {
        if (_currentAction != ActionType.Idle)
            _entity.Animator.SetBool(_currentAction.ToString(), false);

        _currentAction = ActionType.Idle;
    }
}
