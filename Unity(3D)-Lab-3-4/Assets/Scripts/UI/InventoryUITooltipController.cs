using TMPro;
using UnityEngine;

public class InventoryUITooltipController : UITooltipController
{
    [SerializeField] private GameObject tooltipPrefab;
    private TMP_Text _tooltipText;
    private bool _initialized;
    protected override void Start() { }

    public void Initialize(string text)
    {
        if(_initialized) return;

        _initialized = true;
        
        tooltip = Instantiate(tooltipPrefab, transform);
        tooltip.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - tooltip.GetComponent<RectTransform>().rect.height, transform.localPosition.z);
        _tooltipText = tooltip.GetComponentInChildren<TMP_Text>();
        _tooltipText.SetText(text);
        
        CanvasGroup = tooltip.GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
        
        tooltip.SetActive(true);
    }

    public void Destroy()
    {
        if(!_initialized) return;
        
        _initialized = false;
        StopAllCoroutines();
        Destroy(tooltip.gameObject);
    }
}
