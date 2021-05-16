using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInfoUIController : MonoBehaviour, IDragHandler
{
    public enum ItemInfoType
    {
        Item,
        Equipment
    }

    private ItemInfoType _current;

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TMP_Text primaryStatsText;
    [SerializeField] private TMP_Text additionalStatsText;
    
    public void SetImage(Sprite image)
    {
        if(image == null) return;
        
        itemImage.sprite = image;
    }

    public void SetType(ItemInfoType type)
    {
        _current = type;

        switch (_current)
        {
            case ItemInfoType.Item:
                primaryStatsText.transform.parent.gameObject.SetActive(false);
                additionalStatsText.transform.parent.gameObject.SetActive(false);
                statsPanel.SetActive(false);
                break;
            case ItemInfoType.Equipment:
                if(primaryStatsText.text.Length > 0)
                    primaryStatsText.transform.parent.gameObject.SetActive(true);

                if(additionalStatsText.text.Length > 0)
                    additionalStatsText.transform.parent.gameObject.SetActive(true);

                statsPanel.SetActive(true);
                break;
        }
    }

    public void SetName(string name)
    {
        itemNameText.SetText(name);
    }

    public void SetDescription(string description)
    {
        descriptionText.SetText(description);
    }

    public void SetPrimaryStats(string primaryStats)
    {
        primaryStatsText.SetText(primaryStats);
    }

    public void SetAdditionalStats(string additionalStats)
    {
        additionalStatsText.SetText(additionalStats);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
}
