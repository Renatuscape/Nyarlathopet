using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static Language language = Language.English;
}

public enum Language
{
    English,
    Norsk,
    Latinus
}