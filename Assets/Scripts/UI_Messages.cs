using UnityEngine;
using TMPro;

public class UI_Messages : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform messagesContent;
    [Header("Prefabs")]
    [SerializeField] private GameObject messagePrefab;
    
    private static UI_Messages uiMessages;
    public static UI_Messages Instance => uiMessages;

    private void Start()
    {
        uiMessages = this;
    }
    
    public void ShowMessage(string label, string info, float time)
    {
        var createdMessage = Instantiate(messagePrefab, messagesContent,false);
        createdMessage.TryGetComponent<Message>(out var message);
        message.ChangeText(label ,info, time);
        
    }
}
