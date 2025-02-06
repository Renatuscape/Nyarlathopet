using UnityEngine;

public static class GlobalSettings
{
    public static Language language
    {
        get => (Language)PlayerPrefs.GetInt("Language", (int)Language.English);
        set
        {
            PlayerPrefs.SetInt("Language", (int)value);
            PlayerPrefs.Save();
        }
    }
}

public enum Language
{
    English,
    Norsk,
    Latinus
}