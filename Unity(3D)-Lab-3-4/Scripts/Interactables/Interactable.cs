using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected float interactionRadius;
    [Range(0f, 1f)] [SerializeField] protected float stoppingIndex;

    protected bool CanInteract { get; set; }
    protected UIController _uiController;

    private bool isInteracting;

    protected PCInputController PCInputController;
    
    public float InteractionRadius => interactionRadius;
    public float StoppingDistance => interactionRadius * stoppingIndex;
    
    protected virtual void Start()
    {
        _uiController = FindObjectOfType<UIController>();
        PCInputController = player.GetComponent<PCInputController>();
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactionRadius)
        {
            if (Input.anyKeyDown && isInteracting)
            {
                isInteracting = false;
                StopCoroutine(WalkUp());
                player.ActionController.Move(player.transform.position);
            }
            
            if (PCInputController.HasInteraction)
            {
                isInteracting = true;
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        if(!CanInteract)
            StartCoroutine(WalkUp());
        else
            _uiController.SetDebugText("Interaction with " + gameObject.name);
    }

    protected virtual IEnumerator WalkUp()
    {
        while (isInteracting)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > StoppingDistance)
            {
                CanInteract = false;
                player.ActionController.Move(transform.position, StoppingDistance);
            }
            else
            {
                CanInteract = true;
                isInteracting = false;
                Interact();

                yield break;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
    
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
