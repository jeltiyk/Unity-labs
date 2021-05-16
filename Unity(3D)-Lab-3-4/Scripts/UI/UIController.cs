using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private Canvas _canvas;
    private Color _debugTextColor;
    
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject debugPanel;
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject equipmentPanel;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        
        _debugTextColor = debugText.color;
    }

    public void ChangeUIVisibility(GameObject gameObject = null)
    {
        if(gameObject != null && !this.gameObject.activeInHierarchy) return;
        
        
        if (gameObject == null) gameObject = this.gameObject;
        
        switch (gameObject.activeInHierarchy)
        {
            case true:
            {
                gameObject.SetActive(false);
                break;   
            }
            case false:
            {
                gameObject.SetActive(true);
                break;   
            }
        }
    }
    
    public void ChangeInfoPanelVisibility()
    {
        ChangeUIVisibility(infoPanel);
    }

    public void ChangeInventoryVisibility()
    {
        ChangeUIVisibility(inventoryPanel);
    }

    public void ChangeEquipmentVisibility()
    {
        ChangeUIVisibility(equipmentPanel);
    }
    
    public void HideDebugPanel()
    {
        if(!debugPanel.activeInHierarchy || !gameObject.activeInHierarchy) return;
        
        debugPanel.SetActive(false);
    }

    public void ShowDebugPanel()
    {
        if(debugPanel.activeInHierarchy || !gameObject.activeInHierarchy) return;
        
        debugPanel.SetActive(true);
    }

    public void SetDebugText(string text)
    {
        debugText.text = text;
    }

    public void SetDebugTextColor(Color color)
    {
        debugText.color = color;
    }
}
