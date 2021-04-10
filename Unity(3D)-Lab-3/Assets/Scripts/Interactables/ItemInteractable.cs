using UnityEngine;

public class ItemInteractable : Interactable
{
    protected override void Interact()
    {
        base.Interact();
        
        // Code below of this if-case ignores if-case in base (now is commented)
        if(!CanInteract) return;
        
        _debugCanvasText.color = Color.yellow;

        Destroy(gameObject);
        
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
