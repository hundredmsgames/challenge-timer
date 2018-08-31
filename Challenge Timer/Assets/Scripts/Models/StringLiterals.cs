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
    static public string HowToPlayText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Nasıl Oynanır";
                case Language.ENGLISH:
                    return "How To Play";
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
                    return "Grafiker";
                case Language.ENGLISH:
                    return "Graphicer";
                default:
                    return "";
            }

        }
    }
    static public string CreditsJoinUsText
    {
        get
        {
            switch (language)
            {
                case Language.TURKISH:
                    return "Eğer bize katılmak istiyorsanız, iletişime geçin.\n" + 
                           "100msgamestudio@gmail.com\n\n" +
                           "Umarım eğlendiniz :)";
                case Language.ENGLISH:
                    return "If you want to join us, please contact.\n" + 
                           "100msgamestudio@gmail.com\n\n" +
                           "Hope you enjoyed :)";
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
                    return "Rastgele";
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
                    return "Yarım saniye hata yapma şansınız var. İçinizden saymanız gereken sürenin rasgele değişken " +
                            "olması konusunda dikkat edin."; ;
                case Language.ENGLISH:
                    return "You have a chance to make mistake half a second.Be careful time will be changing randomly that you have to follow during the game.";
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
                    return"Yarım saniye hata yapma şansınız var. İçinizden saymanız gereken sürenin zamanla " +
                            "artacağına dikkat edin.";
                case Language.ENGLISH:
                    return "You have a chance to make mistake half a second.Be careful time will be increasing that you have to follow during the game.";
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
                    return "Yarım saniye hata yapma şansınız var. İçinizden saymanız gereken sürenin zamanla " +
                            "artacağına dikkat edin. Bu challenge küçük arkadaşlarımız için olduğundan zaman açık olarak oynanacaktır.";
                case Language.ENGLISH:
                    return "You have a chance to make mistake half a second.Be careful time will be increasing that you have to follow during the game. This challenge for our little friends so timer will be on.";
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
                    return "Language";
                default:
                    return "";
            }

        }
    }


}
