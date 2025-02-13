using UnityEngine;

public static class ConverterTool
{
    public static string ConvertToString<T>(T data)
    {
        return JsonUtility.ToJson(data);
    }
    public static T ConvertFromString<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
