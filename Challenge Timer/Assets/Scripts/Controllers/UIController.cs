using System;

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class UIController : MonoBehaviour
{
    public static UIController Instance;
    private GameController gameController;
    public Animator showScoreAnimator;

    public GameObject animatedTextPrefab;

    public GameObject panel_Menu;
    public GameObject panel_Game;
    public GameObject panel_Scores;
    public GameObject panel_Settings;
    public GameObject panel_Credits;
    public GameObject panel_HowToPlay;
    public GameObject panel_Languages;
    public GameObject[] panel_GameInfos;
    public GameObject[] winloseObjects;
    public GameObject[] failedTextObjects;

    public Transform[] errorTextContainer;
    public TextMeshProUGUI[] text_TimeIntervals;
    public TextMeshProUGUI[] text_Countdowns;
    public TextMeshProUGUI[] text_times;
    public TextMeshProUGUI[] text_winlose;
    public TextMeshProUGUI[] text_losetext;
    public TextMeshProUGUI[] text_scores;

    public GameObject[] spriteObjectContainers;
    Dictionary<string, Sprite> nameToSpriteMap;

    public TextMeshProUGUI languageText;
    public TextMeshProUGUI CreditsText;
    public TextMeshProUGUI CreditsCoderText;
    public TextMeshProUGUI CreditsDesignerText;
    public TextMeshProUGUI CreditsGraphicsText;
    public TextMeshProUGUI CreditsJoinUsText;
    public TextMeshProUGUI HowToPlayText;

    public TextMeshProUGUI[] InfoHeaderText;
    public TextMeshProUGUI[] InfoContentText;
    public TextMeshProUGUI InfoHeaderMainMenuText;
    public TextMeshProUGUI InfoContentMainMenuText;

    // We should reset this variable when the game is restarted.
    private int allTimersStarted;
    private int gameInfoClosedCounter;
    private bool settingsPanelShow = false;

    /*
     Game Screen Color
     FF006C => pinkish
         */


    // SETUP GAME
    private void Start()
    {
        if (Instance != null)
            return;

        Instance = this;
        gameController = GameController.Instance;
        LoadSprites();

        if (PlayerPrefs.HasKey("lang"))
        {
            int lan = PlayerPrefs.GetInt("lang");
            ButtonPressed_SelectLanguage(lan);
        }
        else
        {
            PlayerPrefs.SetInt("lang", 1);
            ButtonPressed_SelectLanguage(1);
        }

        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
        gameController.UpdateCountDownText += UpdateCountDownText;
        gameController.UpdateTimeText += UpdateTime;
        gameController.UpdateWinLoseText += UpdateWinLoseText;
        gameController.UpdateFailedText += UpdateFailedText;
        gameController.HideScorePanel += HideScorePanel;
        gameController.RestartUI += RestartUI;
        gameController.ShowScorePanel += ShowScorePanel;
        gameController.UpdateInfoSprites += UpdateInfoSprites;
        gameController.HideTimers += HideTimers;
        gameController.HideIntervals += HideIntervals;

        INFO_TOP(0);
        INFO_BOTTOM(0);
    }

    void LoadSprites()
    {
        nameToSpriteMap = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Icons");
        for (int i = 0; i < sprites.Length; i++)
        {
            nameToSpriteMap.Add(sprites[i].name, sprites[i]);
        }
    }

    public void INFO_TOP(int type)
    {
        ChallengeType challengeType = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[type]));
        string header = "";
        string content = "";
        switch (challengeType)
        {
            case ChallengeType.Infinite:
                header = StringLiterals.InfoHeaderInfinite;
                content = StringLiterals.InfoContentInfinite;
                break;
            case ChallengeType.Kids:

                header = StringLiterals.InfoHeaderKids;
                content = StringLiterals.InfoContentKids;
                break;
            case ChallengeType.Random:

                header = StringLiterals.InfoHeaderRandom;
                content = StringLiterals.InfoContentRandom;
                break;
            default:
                break;
        }

        if(panel_Menu.activeSelf == true)
        {
            InfoHeaderMainMenuText.text = header;
            InfoContentMainMenuText.text = content;
        }
        else
        {
            InfoHeaderText[0].text = header;
            InfoContentText[0].text = content;
        }
    }

    public void INFO_BOTTOM(int type)
    {
        ChallengeType challengeType = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[type]));
        string header = "";
        string content = "";
        switch (challengeType)
        {
            case ChallengeType.Infinite:
                header = StringLiterals.InfoHeaderInfinite;
                content = StringLiterals.InfoContentInfinite;
                break;
            case ChallengeType.Kids:

                header = StringLiterals.InfoHeaderKids;
                content = StringLiterals.InfoContentKids;
                break;
            case ChallengeType.Random:

                header = StringLiterals.InfoHeaderRandom;
                content = StringLiterals.InfoContentRandom;
                break;
            default:
                break;
        }

        if (panel_Menu.activeSelf == true)
        {
            InfoHeaderMainMenuText.text = header;
            InfoContentMainMenuText.text = content;
        }
        else
        {
            InfoHeaderText[1].text = header;
            InfoContentText[1].text = content;
        }
    }

    public void ChallengeTypeSelectorButtons_OnClick(int selectedPage)
    {
        for (int i = 0; i < gameController.playerCount; i++)
        {
            gameController.challenges[i].Type = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[selectedPage]));

            switch (gameController.challenges[i].Type)
            {
                case ChallengeType.Infinite:
                case ChallengeType.Kids:
                    gameController.SetChallengeSettings(gameController.challenges[i].Type, 2000, 500, 3);
                    break;
                case ChallengeType.Random:
                    gameController.SetChallengeSettings(gameController.challenges[i].Type, -1, 400, -1, 1, 5);
                    break;
                default:
                    break;
            }
        }
        //Color color = new Color();
        //if (selectedPage == 0)
        //{
        //    color.r = 0;
        //    color.g = 167;
        //    color.b = 204;
        //    color.a = 1;
        //}
        //else if(selectedPage == 1)
        //{
        //    color.r = 249;
        //    color.g = 119;
        //    color.b = 154;
        //    color.a = 1;
        //}
        //else if (selectedPage == 2)
        //{
        //    color.r = 194;
        //    color.g = 169;
        //    color.b = 149;
        //    color.a = 1;
        //}
        //panel_Menu.GetComponent<Image>().color = color;
    }

    private void UpdateInfoSprites(object value, int playerIdx)
    {
        int index = (int)value;
        Image image = spriteObjectContainers[playerIdx].GetComponentsInChildren<Image>()[index];

        image.sprite = nameToSpriteMap["wrong"];
    }


    // BUTTON EVENTS
    public void ButtonPressed_Challenge()
    {
        panel_Menu.SetActive(false);
        panel_Game.SetActive(true);
        RestartUI();
        settingsPanelShow = false;

        // We call this two after setting menu panel as active "false"
        // It's important.
        INFO_TOP(0);
        INFO_BOTTOM(0);

        if (PlayerPrefs.HasKey("game_info") == false)
        {
            PlayerPrefs.SetInt("game_info", 1);
            foreach (GameObject go in panel_GameInfos)
                go.SetActive(true);
        }
        else
        {
            gameController.StartGame();
        }
    }

    public void ButtonPressed_ShowLanguage()
    {
        panel_Languages.GetComponent<Animator>().SetBool("open", true);
    }

    public void ButtonPressed_SelectLanguage(int language)
    {
        StringLiterals.language = (Language)language;
        panel_Languages.GetComponent<Animator>().SetBool("open", false);

        INFO_TOP(0);
        INFO_BOTTOM(0);
        SetTextsForLanguages();
        PlayerPrefs.SetInt("lang", language);
    }
    
    private void SetTextsForLanguages()
    {
        CreditsText.text = StringLiterals.CreditsButtonText;
        languageText.text = StringLiterals.LanguagesButtonText;
        CreditsCoderText.text = StringLiterals.CreditsCodersButtonText;
        CreditsDesignerText.text = StringLiterals.CreditsDesignersButtonText;
        CreditsGraphicsText.text = StringLiterals.CreditsGraphicsButtonText;
        CreditsJoinUsText.text = StringLiterals.CreditsJoinUsText;
        HowToPlayText.text = StringLiterals.HowToPlayText;
    }

    public void ButtonPressed_Settings()
    {
        settingsPanelShow = !settingsPanelShow;
        panel_Settings.GetComponent<Animator>().SetBool("panelShow", settingsPanelShow);
        
        // If Settings Panel closed, close all panels too.
        if(settingsPanelShow == false)
        {
            panel_Languages.GetComponent<Animator>().SetBool("open", false);
            panel_Credits.SetActive(false);
            panel_HowToPlay.SetActive(false);
        }
    }

    public void ButtonPressed_HowToPlay()
    {
        panel_HowToPlay.SetActive(!panel_HowToPlay.activeSelf);
        panel_Credits.SetActive(false);
    }

    public void ButtonPressed_Credits()
    {
        panel_Credits.SetActive(!panel_Credits.activeSelf);
        panel_HowToPlay.SetActive(false);

    }

    public void ButtonPressed_Github()
    {
        Application.OpenURL("https://github.com/hundredmsgames/challenge-timer");
    }

    public void ButtonPressed_FollowTheNumbers()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.HundredMsGameS.followTheNumbers");
    }

    public void ButtonPressed_Samil()
    {
        Application.OpenURL("http://samilsoftsam.com");
    }

    public void ButtonPressed_Melih()
    {
        Application.OpenURL("http://nobrainexception.com");
    }

    public void ButtonPressed_Murat()
    {
        Application.OpenURL("https://www.mehmetmuratyilmaz.com/");
    }
}
