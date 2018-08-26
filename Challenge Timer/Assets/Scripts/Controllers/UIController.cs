using System;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
    private GameController gameController;

    public Transform challengeTypeContent;

    public GameObject pageMediumPrefab;
    public GameObject pageSmallPrefab;
    public GameObject animatedTextPrefab;


    public GameObject panel_Menu;
    public GameObject panel_Game;
    public GameObject panel_gameEnd;
    public GameObject[] winloseObjects;
    public GameObject[] failedTextObjects;

    public Transform[] errorTextContainer;

    public TextMeshProUGUI[] text_TimeIntervals;
    public TextMeshProUGUI[] text_Countdowns;
    public TextMeshProUGUI[] text_times;

    public TextMeshProUGUI[] text_winlose;
    public TextMeshProUGUI[] text_losetext;

    // We should reset this variable when the game is restarted.
    int allTimersStarted;

    Dictionary<string, Sprite> challengeTypeToSprite;
    private void Start()
    {
        // LoadSprites();

        gameController = GameController.Instance;
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
        gameController.UpdateCountDownText += UpdateCountDownText;
        gameController.UpdateTimeText += UpdateTime;
        gameController.UpdateWinLoseText += UpdateWinLoseText;
        gameController.UpdateFailedText += UpdateFailedText;
        gameController.RestartUI += RestartUI;
        FillPickerLists();
    }

    private void RestartUI(object value, int playerIdx)
    {
        //disable game end panel
        panel_gameEnd.SetActive(false);

        //reset challenges
        for (int i = 0; i < gameController.playerCount; i++)
        {
            gameController.challenges[i].TimeInterval = gameController.challenges[i].StartInterval;
        }

        //reset win lose texts
        int otherPlayer = (playerIdx + 1) % gameController.playerCount;
        text_winlose[playerIdx].gameObject.SetActive(false);
        text_winlose[otherPlayer].gameObject.SetActive(false);

        text_losetext[playerIdx].gameObject.SetActive(false);
        text_losetext[otherPlayer].gameObject.SetActive(false);

        for (int i = 0; i < gameController.playerCount; i++)
        {
            text_times[i].color = new Color(text_times[i].color.r, text_times[i].color.g, text_times[i].color.b, 1);
            text_times[i].text = "0.000";
            text_times[i].gameObject.SetActive(false);
        }

        allTimersStarted = 0;
    }

    private void UpdateFailedText(object value, int playerIdx)
    {
        failedTextObjects[playerIdx].SetActive(true);
    }

    private void UpdateWinLoseText(object value, int playerIdx)
    {
        int otherPlayer = (playerIdx + 1) % gameController.playerCount;
        text_winlose[playerIdx].text = "You Won";
        text_winlose[otherPlayer].text = "You Lose";
        for (int i = 0; i < gameController.playerCount; i++)
        {
            winloseObjects[i].SetActive(true);
            text_TimeIntervals[i].gameObject.SetActive(false);
        }

        text_losetext[otherPlayer].gameObject.SetActive(true);

        if (panel_gameEnd.activeSelf == false)
            panel_gameEnd.SetActive(true);

    }
    /*
     Game Screen Color
     FF006C => pinkish
         */

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
        gameController.StartGame();
    }

    public void ButtonPressed_OpenLeaderboard()
    {
        Debug.Log("Open Leaderboard");
    }

    public void ButtonPressed_Github()
    {
        Debug.Log("Github link has not implemented yet.");
    }

    public void ButtonPressed_FollowTheNumbers()
    {
        Debug.Log("Follow The Numbers link has not implemented yet.");
    }

    // UPDATE METHODS

    private void UpdateCountDownText(object value, int playerIdx)
    {
        TextMeshProUGUI countdown = text_Countdowns[playerIdx];

        if (countdown.gameObject.activeSelf == false)
            countdown.gameObject.SetActive(true);

        countdown.text = value.ToString();
    }

    private void UpdateTimeInterval(object timeInterval, int playerIdx)
    {
        TextMeshProUGUI interval = text_TimeIntervals[playerIdx];

        // FIXME: If same number comes consecutively in random challenge
        // It will not pop up. Find better way!
        if (interval.text != timeInterval.ToString())
            interval.gameObject.SetActive(false);

        interval.text = "Count up to " + ((int)timeInterval / 1000).ToString();
        interval.gameObject.SetActive(true);
    }

    private void UpdateError(object error, int playerIdx)
    {
        GameObject go = Instantiate(animatedTextPrefab, errorTextContainer[playerIdx]);
        TextMeshProUGUI textError = go.GetComponentInChildren<TextMeshProUGUI>();
        go.name = "Text_Error";

        int seconds = Math.Abs((int)error / 1000);
        int millisec = Math.Abs((int)error % 1000);

        string secondStr = "";
        string millisecStr = "";

        if (seconds > 0 && seconds < 10)
            secondStr += "0";

        secondStr += seconds;

        if (millisec < 10)
            millisecStr += "00";
        else if (millisec < 100)
            millisecStr += "0";

        millisecStr += millisec;

        textError.text = (int)error > 0 ? "+" : "-";
        textError.text += secondStr + "." + millisecStr + "";
    }

    private void UpdateTime(object obj, int playerIdx)
    {
        TextMeshProUGUI t = text_times[playerIdx];
        long time = (long)obj;

        if (t.gameObject.activeSelf == false)
            t.gameObject.SetActive(true);

        int seconds = (int)(time / 1000);
        int millisec = (int)(time - seconds * 1000);

        string secondStr = "";
        string millisecStr = "";

        if (seconds > 0 && seconds < 10)
            secondStr += "0";

        secondStr += seconds;

        if (millisec < 10)
            millisecStr += "00";
        else if (millisec < 100)
            millisecStr += "0";

        millisecStr += millisec;

        t.text = secondStr + "." + millisecStr;

        //FIXME:
        //I want to start coroutine in a spesific time interval
        //we can find a better way
        if (allTimersStarted < gameController.playerCount)
            StartCoroutine(FadeTextToZeroAlpha(.62f, t));

        allTimersStarted++;
    }

    //https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    //I had the almost same idea but this looks way better than my idea
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * t));
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

    }
}
