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
                { "order_cards","order cards" },
                { "language","language" },
                { "hungarian","hungarian" },
                { "english","english" }
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
                { "english","angol" }
            }
        }
    };

    public Language language = Language.English;
    public StringFormat stringFormat = StringFormat.FirstLetterUpperCase;
    public string wordKey = "";
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        string setTo = languageMap[language][wordKey];

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

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    static string EveryFirstLetterUppercase(string s)
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
}
