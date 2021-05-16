using UnityEngine;

[RequireComponent(typeof(NPC))]
public class NPCInteractable : Interactable
{
    private NPC _npc;
    private Canvas _canvas;
    
    protected override void Start()
    {
        base.Start();
        
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
                    PCInputController.enabled = false;
                    return;
                }
                
                _canvas.gameObject.SetActive(false);
                PCInputController.enabled = true;
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
        if(!CanInteract) return;
        

        if (_npc.IsFriendly && !_canvas.gameObject.activeInHierarchy)
        {
            _uiController.SetDebugTextColor(Color.green);
            
            _canvas.gameObject.SetActive(true);
        }
        else if (_npc.CurrentState != NPC.State.Angry)
        {
            _uiController.SetDebugTextColor(Color.red);

            _npc.ChangeState(NPC.State.Angry);
        }
        
        CanInteract = false;
    }
}
