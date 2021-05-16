using UnityEngine;

[RequireComponent(typeof(PhysicalItem))]
public class ItemInteractable : Interactable
{
    private PhysicalItem _physicalItem;

    protected override void Start()
    {
        base.Start();

        _physicalItem = GetComponent<PhysicalItem>();
    }

    protected override void Interact()
    {
        base.Interact();
        if(!CanInteract) return;
        
        _uiController.SetDebugTextColor(Color.yellow);
        
        _physicalItem.PickUp(player);

        //redundant
        //CanInteract = false;
    }

    // private IEnumerator PickItem()
    // {
    //     while (true)
    //     {
    //         if (Vector3.Distance(player.transform.position, transform.position) > StoppingDistance)
    //         {
    //             player.ActionController.Move(transform.position, StoppingDistance);
    //         }
    //         else
    //         {
    //             _debugCanvasText.color = Color.yellow;
    //             base.Interact();
    //             
    //             Destroy(gameObject);
    //             
    //             yield break;
    //         }
    //         
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }
}
