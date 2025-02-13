public class Tutorial_TranslationBoard : TranslatorTMP
{
    public override void Refresh()
    {
        if(!textMesh) return;
        switch (LanguageChanger.Instance.languageType)
        {
            case TranslationType.russian:
                textMesh.text = $"WASD - Управление\n" +
                                $"E - Интеракция с объектами\n" +
                                $"Мышь - Направление атаки\n" +
                                $"Левая/Правая кнопка мыши - Атака";
                break;
            case TranslationType.english:
                textMesh.text = "WASD - Control" +
                                "\nE - Interaction with objects" +
                                "\nMouse - Attack direction\n" +
                                "Left/Right mouse button - Attack";
                break;
        }
    }
}
