using System;
using TMPro;
using UnityEngine;

public class ChatController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text tmpTextPrefab;

    [SerializeField] private float destroyTime;

    void Start()
    {
        inputField.onSubmit.AddListener(SendMessage);
    }

    private void OnDestroy()
    {
        inputField.onSubmit.RemoveListener(SendMessage);
    }

    private new void SendMessage(string message)
    {
        TMP_Text tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(),
            transform.position, Quaternion.identity);

        if (message.Length == 0)
            message = "> Empty message! Please enter something. <";

        if (message.Length > 47)
            message = "> Very long message! Max message length: 47 chars. <";
        
        tmpText.SetText("[" + DateTime.Now + "]: " + message);
        tmpText.transform.SetParent(tmpTextPrefab.transform.parent);
        tmpText.rectTransform.localScale = tmpTextPrefab.rectTransform.localScale;

        inputField.text = null;

        Destroy(tmpText.gameObject, destroyTime);
    }
}
