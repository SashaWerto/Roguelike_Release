using System;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public TranslationType languageType = TranslationType.english;
    private static LanguageChanger languageChanger;
    public static LanguageChanger Instance => languageChanger;
    public Action OnLanguageChange;

    private void Awake()
    {
        languageChanger = this;
    }

    public void Refresh()
    {
        ChangeLanguage(languageType);
    }

    public void ChangeLanguage(string languageName)
    {
        switch (languageName)
        {
            case "russian":
                languageType = TranslationType.russian;
                break;
            case "english":
                languageType = TranslationType.english;
                break;
            case "german":
                languageType = TranslationType.german;
                break;
            case "turkey":
                languageType = TranslationType.turkey;
                break;
        }
        OnLanguageChange?.Invoke();
    }
    public void ChangeLanguage(TranslationType translationType)
    {
        languageType = translationType;
        OnLanguageChange?.Invoke();
    }
}
