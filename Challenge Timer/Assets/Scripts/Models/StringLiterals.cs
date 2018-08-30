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
                    return "Kaybettin!";
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
                    return "Kaçırdın";
                case Language.ENGLISH:
                    return "You Missed";
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

    static public string CreditsButtonText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Hakkında";
                case Language.ENGLISH:
                    return "Credits";
                default:
                    return "";
            }

        }
    }
    static public string CreditsCodersButtonText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Kodlayanlar";
                case Language.ENGLISH:
                    return "Coders";
                default:
                    return "";
            }

        }
    }
    static public string CreditsDesignersButtonText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Tasarımcılar";
                case Language.ENGLISH:
                    return "Designers";
                default:
                    return "";
            }

        }
    }
    static public string CreditsGraphicsButtonText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Grafikerler";
                case Language.ENGLISH:
                    return "Graphics";
                default:
                    return "";
            }

        }
    }
    static public string InfoHeaderRandom
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Rasgele";
                case Language.ENGLISH:
                    return "Random";
                default:
                    return "";
            }

        }
    }
    static public string InfoContentRandom
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Randomu anlat";
                case Language.ENGLISH:
                    return "tell about random";
                default:
                    return "";
            }

        }
    }
    static public string InfoHeaderInfinite
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Sonsuz";
                case Language.ENGLISH:
                    return "Infinite";
                default:
                    return "";
            }

        }
    }
    static public string InfoContentInfinite
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "sonsuzu anlat";
                case Language.ENGLISH:
                    return "tell about infinite";
                default:
                    return "";
            }

        }
    }
    static public string InfoHeaderKids
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Çocuk Modu";
                case Language.ENGLISH:
                    return "Kids";
                default:
                    return "";
            }

        }
    }
    static public string InfoContentKids
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "çocuk modunu anlat";
                case Language.ENGLISH:
                    return "tell about kids mode";
                default:
                    return "";
            }

        }
    }
    static public string LanguagesButtonText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Dil";
                case Language.ENGLISH:
                    return "Languages";
                default:
                    return "";
            }

        }
    }


}
