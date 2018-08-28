using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringLiterals
{
    static public Language language { get; set; }

    static public string CountDownObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Başla";
                case Language.ENGLISH:
                    return "GO";
                default:
                    return "";
            }
        }
    }

    static public string WinObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Kazandın!";
                case Language.ENGLISH:
                    return "You Won!";
                default:
                    return "";
            }
        }
    }

    static public string LoseObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Başaramadın!";
                case Language.ENGLISH:
                    return "You Lose!";
                default:
                    return "";
            }
        }
    }

    static public string BetterLuckObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Bir dahaki sefere bol şans!!!";
                case Language.ENGLISH:
                    return "Better luck next time!!!";
                default:
                    return "";
            }
        }
    }
    static public string FailedObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Hatalı";
                case Language.ENGLISH:
                    return "Failed";
                default:
                    return "";
            }
        }
    }
    static public string IntervalObjectText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Sonraki zaman aralığı";
                case Language.ENGLISH:
                    return "Count up to";
                default:
                    return "";
            }
        }
    }

}
