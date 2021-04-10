using System.Collections;
using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCInteractable : Interactable
{
    private PCInputController _pcInputController;
    private Canvas _canvas;

    private NPC _npc;
    
    protected override void Start()
    {
        base.Start();
        
        _pcInputController = player.GetComponent<PCInputController>();

        _npc = GetComponent<NPC>();

        if(_npc.IsFriendly)
            _canvas = GetComponentInChildren<Canvas>(true);
    }

    protected override void Update()
    {
        if (_npc.IsFriendly)
        {
            if (_canvas.gameObject.activeInHierarchy)
            {
                // //close dialog(chat) window
                // if (Vector3.Distance(player.transform.position, _npc.transform.position) > interactionRadius)
                // {
                //     _canvas.gameObject.SetActive(false);
                //     return;
                // }
                
                //disable player movement(mouse input)
                if (!Input.GetKey(KeyCode.Escape))
                {
                    _pcInputController.enabled = false;
                    return;
                }
                
                _canvas.gameObject.SetActive(false);
                _pcInputController.enabled = true;
                return;
            }
        } 
        else if (_npc.CurrentState == NPC.State.Angry)
        {
            _npc.ActionController.Attack(player.transform);
            return;
        }

        base.Update();
    }

    protected override void Interact()
    {
        base.Interact();
        // Code below of this if-case ignores if-case in base (now is commented)
        if(!CanInteract) return;
        
        
        if (_npc.IsFriendly && !_canvas.gameObject.activeInHierarchy)
        {
            _debugCanvasText.color = Color.green;

            _canvas.gameObject.SetActive(true);
        }
        else if (_npc.CurrentState != NPC.State.Angry)
        {
            _debugCanvasText.color = Color.red;

            _npc.ChangeState(NPC.State.Angry);
        }
        
        CanInteract = false;
    }
}
