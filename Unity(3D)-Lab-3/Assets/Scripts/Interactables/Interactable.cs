using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected float interactionRadius;
    [Range(0f, 1f)] [SerializeField] protected float stoppingIndex;

    protected bool CanInteract { get; set; }

    public float InteractionRadius => interactionRadius;
    public float StoppingDistance => interactionRadius * stoppingIndex;

    protected TMP_Text _debugCanvasText;
    
    protected virtual void Start()
    {
        _debugCanvasText = FindObjectOfType<Canvas>().transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= interactionRadius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        // if (!CanInteract)
        // {
            // StartCoroutine(WalkUp());
            // return;
        // }
        
        // _debugCanvasText.SetText("Interaction with: " + gameObject.name);
        
        if(!CanInteract)
            StartCoroutine(WalkUp());
        else
            _debugCanvasText.SetText("Interaction with: " + gameObject.name);
    }

    protected virtual IEnumerator WalkUp()
    {
        while (true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > StoppingDistance)
            {
                CanInteract = false;
                player.ActionController.Move(transform.position, StoppingDistance);
            }
            else
            {
                CanInteract = true;
                Interact();

                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
