using UnityEngine;
using TMPro;
public class Message : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI labelTextMesh;
    public TextMeshProUGUI messageTextMesh;
    private bool initiated;
    private float delay;

    private void Update()
    {
        if (initiated)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ChangeText(string label, string message, float time)
    {
        labelTextMesh.text = label;
        messageTextMesh.text = message;
        delay = time;
        initiated = true;
    }
}
