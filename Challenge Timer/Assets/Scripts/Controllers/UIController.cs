using System;
using UnityEngine.UI.Extensions;
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

    Dictionary<string, Sprite> challengeTypeToSprite;

    public GameObject pageMediumPrefab;
    public GameObject pageSmallPrefab;
    public GameObject animatedTextPrefab;

    public Transform challengeTypeContent;
    public Transform[] errorTextContainer;
    public GameObject panel_Menu;
    public GameObject panel_Game;
    public GameObject panel_gameEnd;
    public GameObject[] winloseObjects;
    public GameObject[] failedTextObjects;

    public TextMeshProUGUI[] text_TimeIntervals;
    public TextMeshProUGUI[] text_Countdowns;
    public TextMeshProUGUI[] text_times;
    public TextMeshProUGUI[] text_winlose;
    public TextMeshProUGUI[] text_losetext;
    public TextMeshProUGUI[] text_scores;

    // We should reset this variable when the game is restarted.
    int allTimersStarted;

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
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
        gameController.UpdateCountDownText += UpdateCountDownText;
        gameController.UpdateTimeText += UpdateTime;
        gameController.UpdateWinLoseText += UpdateWinLoseText;
        gameController.UpdateFailedText += UpdateFailedText;
        gameController.HideScorePanel += HideScorePanel;
        gameController.RestartUI += RestartUI;
        FillPickerLists();
    }

    void LoadSprites()
    {
        challengeTypeToSprite = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/challengeType");
        for (int i = 0; i < sprites.Length; i++)
        {
            challengeTypeToSprite.Add(sprites[i].name, sprites[i]);
        }
    }

    private void FillPickerLists()
    {
        // We don't use this. VerticalScrollSnap.RemoveAllChildren need it.
        GameObject[] removed;

        // Fill challenge types.
        string[] challengeTypes = gameController.challengeTypes;
        VerticalScrollSnap challengeTypeSnap = challengeTypeContent.parent.gameObject.GetComponent<VerticalScrollSnap>();
        challengeTypeSnap.RemoveAllChildren(out removed);
        foreach (GameObject go in removed)
            Destroy(go);

        for (int i = 0; i < challengeTypes.Length; i++)
        {
            GameObject page = Instantiate(pageMediumPrefab, challengeTypeContent);
            page.GetComponentInChildren<TextMeshProUGUI>().text = challengeTypes[i];

            // There was a bug before when we use i variable in delegate.
            // I don't know if it's still exist. 
            int index = i;
            page.GetComponent<Button>().onClick.AddListener(delegate
            {
                challengeTypeSnap.ChangePage(index);
                challengeTypeContent.parent.parent.gameObject.GetComponent<ScrollView>().ToggleListSize();
            });
        }
        challengeTypeSnap.UpdateLayout();
    }

    public void ScrollViewChallengeType(int selectedPage)
    {
        for (int i = 0; i < gameController.playerCount; i++)
        {
            gameController.challenges[i].Type = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[selectedPage]));

            switch (gameController.challenges[i].Type)
            {
                case ChallengeType.Infinite:
                    gameController.SetChallengeSettings(ChallengeType.Infinite, 2000, 400, 3);
                    break;
                case ChallengeType.Random:
                    gameController.SetChallengeSettings(ChallengeType.Random, -1, 400, -1, 1, 5);
                    break;
                default:
                    break;
            }
        }
    }


    // BUTTON EVENTS
    public void ButtonPressed_Challenge()
    {
        panel_Menu.SetActive(false);
        panel_Game.SetActive(true);
        RestartUI();
        gameController.StartGame();
    }

    public void ButtonPressed_Github()
    {
        Application.OpenURL("https://github.com/hundredmsgames/challenge-timer");
    }

    public void ButtonPressed_FollowTheNumbers()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.HundredMsGameS.followTheNumbers");
    }
}
