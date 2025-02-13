using UnityEngine;
public class Scene_Changer : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Scene_Loader.Instance.ChangeScene(sceneName);
    }
}
