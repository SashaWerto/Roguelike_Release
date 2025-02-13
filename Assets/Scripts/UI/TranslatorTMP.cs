using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TranslatorTMP : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public List<TranslateVariant> translations;

    private void Start()
    {
        LanguageChanger.Instance.OnLanguageChange += Refresh;
        Refresh();
    }

    private void OnEnable()
    {
        if (LanguageChanger.Instance)
        {
            Refresh();
        }
    }

    public virtual void Refresh()
    {
        if(!textMesh) return;
        TranslationType selectedType = LanguageChanger.Instance.languageType;
        for (int i = 0; i < translations.Count; i++)
        {
            if (translations[i].type == selectedType)
            {
                textMesh.text = translations[i].textRef;
            }
        }
    }
}
[Serializable]
public class TranslateVariant
{
    public string textRef;
    public TranslationType type;
}
