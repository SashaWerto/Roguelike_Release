using UnityEngine;

public class GameEventsHandler : MonoBehaviour
{
    public void ShowRewardWindow()
    {
        GameEventsCaller.Instance.ShowRewardWindow();
    }
}
