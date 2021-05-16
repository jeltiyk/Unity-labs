using System;
using TMPro;
using UnityEngine;

//temporary solution
public class ChatController : MonoBehaviour
{
    private PCInputController _pcInputController;
    
    [SerializeField] private Player player;
    
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text tmpTextPrefab;

    [Tooltip("Time until destroy message")][SerializeField] private float destroyTime;

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnStartTyping);
        inputField.onEndEdit.AddListener(OnEndTyping);
        
        inputField.onSubmit.AddListener(SendMessage);
        _pcInputController = player.GetComponent<PCInputController>();
        
        tmpTextPrefab.SetText("Get help: \"/help\"");
    }

    private void OnDestroy()
    {
        inputField.onValueChanged.AddListener(OnStartTyping);
        inputField.onEndEdit.AddListener(OnEndTyping);
        
        inputField.onSubmit.RemoveListener(SendMessage);
    }

    private void OnStartTyping(string a)
    {
        _pcInputController.isActiveKeys = false;
    }
    private void OnEndTyping(string a)
    {
        _pcInputController.isActiveKeys = true;
    }
    
    private new void SendMessage(string message)
    {       
        TMP_Text tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(),
            transform.position, Quaternion.identity);

        if (message.Length == 0)
        {
            tmpText.color = Color.red;
            message = "> Empty message! Please enter something. <";
        }
        
        if(message.IndexOf("/", StringComparison.Ordinal) == 0)
        {
            if(ExecuteCommand(message)) 
                Destroy(tmpText);
            else
            {
                tmpText.color = Color.red;
                message = "Command not found.";
            }
        }

        tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: " + message);
        tmpText.transform.SetParent(tmpTextPrefab.transform.parent);
        tmpText.rectTransform.localScale = tmpTextPrefab.rectTransform.localScale;

        inputField.text = null;

        Destroy(tmpText.gameObject, destroyTime);
    }

    private bool ExecuteCommand(string command)
    {
        string message = command;
        
        int lastIndex = message.IndexOf(" ", StringComparison.Ordinal);

        if (lastIndex < 0)
        {
            lastIndex = message.Length;
        }
        
        command = message.Substring(0, lastIndex);

        TMP_Text tmpText;
        
        switch (command)
        {
            case "/help":
                tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                tmpText.color = Color.yellow;
                tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Use \"/lvl %lvl count%\" to set lvl.");
                Destroy(tmpText.gameObject, destroyTime);
                
                tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                tmpText.color = Color.yellow;
                tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Use \"/name %name%\" to set name.");
                Destroy(tmpText.gameObject, destroyTime);
                return true;
            case "/name":
				try
                {
                    player.StatController.SetNickname(message.Substring(command.Length + 1));
                }
                catch (Exception e)
                {
                    tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                    tmpText.color = Color.red;
                    tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Enter value through space after command.");
                    Destroy(tmpText.gameObject, destroyTime);
                    return true;
                }

                if (message.Substring(command.Length + 1).Length > 20)
                {
                    tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                    tmpText.color = Color.red;
                    tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Very long nickname. Max length is 20 chars.");
                    Destroy(tmpText.gameObject, destroyTime);

                    return true;
                }
                else if(message.Substring(command.Length + 1).Length < 4)
                {
                    tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                    tmpText.color = Color.red;
                    tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Very short nickname. Min length is 4 chars.");
                    Destroy(tmpText.gameObject, destroyTime);

                    return true;
                }
                
                tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                tmpText.color = Color.green;
                tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Your new nickname is " + player.StatController.PlayerNickname + ".");
                Destroy(tmpText.gameObject, destroyTime);
                return true;
            case "/lvl":
                int lvl;
                
                try
                {
                    lvl = Convert.ToInt32(message.Substring(command.Length + 1));
                }
                catch (Exception e)
                {
                    tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                    tmpText.color = Color.red;
                    tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Enter number value through space after command.");
                    Destroy(tmpText.gameObject, destroyTime);
                    
                    return true;
                }
                
                player.StatController.SetLvl(lvl.ToString());
                
                tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                tmpText.color = Color.green;
                tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: Your new lvl is " + player.StatController.Lvl + ".");
                Destroy(tmpText.gameObject, destroyTime);
                return true;
            default:
                tmpText = Instantiate(tmpTextPrefab.GetComponent<TMP_Text>(), tmpTextPrefab.transform.parent);
                tmpText.color = Color.yellow;
                tmpText.SetText("[" + DateTime.Now.ToString("t") + "]: For help enter \"/help\".");
                Destroy(tmpText.gameObject, destroyTime);
                
                return false;
        }
    }
}
