using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Language
{
    Hungarian, English
}

public enum StringFormat
{
    LowerCase, UpperCase, FirstLetterUpperCase, EveryFirstLetterUppercase
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLanguageController : MonoBehaviour
{
    static Dictionary<Language, Dictionary<string, string>> languageMap = new Dictionary<Language, Dictionary<string, string>>()
    {
        { Language.English, new Dictionary<string, string>
            {
                { "easy","easy" },
                { "hard","hard" },
                { "game","game" },
                { "easy_game","easy game" },
                { "hard_game","hard game" },
                { "settings","settings" },
                { "quit","quit" },
                { "resolution","resolution" },
                { "full_screen","full screen" },
                { "graphics","graphics" },
                { "volume","volume" },
                { "cards","cards" },
                { "order_cards","reorder cards" },
                { "language","language" },
                { "hungarian","hungarian" },
                { "english","english" },
                { "very_low","very low" },
                { "low","low" },
                { "medium","medium" },
                { "high","high" },
                { "very_high","very high" },
                { "ultra","ultra" }
            }
        },
        { Language.Hungarian, new Dictionary<string, string>
            {
                { "easy","könnyű" },
                { "hard","nehéz" },
                { "game","játék" },
                { "easy_game","könnyű játék" },
                { "hard_game","nehéz játék" },
                { "settings","beállítások" },
                { "quit","kilépés" },
                { "resolution","felbontás" },
                { "full_screen","teljes képernyő" },
                { "graphics","grafika" },
                { "volume","hangerő" },
                { "cards","kártyák" },
                { "order_cards","kártyák rendezése" },
                { "language","nyelv" },
                { "hungarian","magyar" },
                { "english","angol" },
                { "very_low","nagyon alacsony" },
                { "low","alacsony" },
                { "medium","közepes" },
                { "high","magas" },
                { "very_high","nagyon magas" },
                { "ultra","extra" }
            }
        }
    };

    static Language actualLanguage = Language.Hungarian;
    public static Language ActualLanguage { get => actualLanguage; set => actualLanguage = value; }
    public static Dictionary<Language, Dictionary<string, string>> LanguageMap { get => languageMap; }

    public StringFormat stringFormat = StringFormat.UpperCase;
    public string wordKey = "";

    TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        ChangeText();
    }

    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string EveryFirstLetterUppercase(string s)
    {
        string[] temp = s.Split(' ');
        string full = "";
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = UppercaseFirst(temp[i]);
            full += temp[i] + " ";
        }

        return full.Trim();
    }

    public void ChangeText()
    {
        string setTo = languageMap[ActualLanguage][wordKey.ToLower().Trim()];

        switch (stringFormat)
        {
            case StringFormat.LowerCase:
                setTo = setTo.ToLowerInvariant();
                break;
            case StringFormat.UpperCase:
                setTo = setTo.ToUpperInvariant();
                break;
            case StringFormat.FirstLetterUpperCase:
                setTo = UppercaseFirst(setTo);
                break;
            case StringFormat.EveryFirstLetterUppercase:
                setTo = EveryFirstLetterUppercase(setTo);
                break;
        }

        text.SetText(setTo.Trim());
    }
}
