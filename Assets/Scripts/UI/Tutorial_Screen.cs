using UnityEngine;

public class Tutorial_Screen : MonoBehaviour
{
    public GameObject tutorialCanvas;
    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialPassed") < 1)
        {
            ShowTutorial();
        }
        else
        {
            tutorialCanvas.SetActive(false);
        }
    }

    public void NeverShowTutorial()
    {
        PlayerPrefs.SetInt("TutorialPassed", 1);
        tutorialCanvas.SetActive(false);
    }

    public void ShowTutorial()
    {
        tutorialCanvas.SetActive(true);
    }
}
