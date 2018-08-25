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

    public Transform errorTextContainer;

    public TextMeshProUGUI text_TimeInterval_TopPlayer;
    public TextMeshProUGUI text_TimeInterval_BottomPlayer;
    public TextMeshProUGUI[] text_Countdowns;
    public TextMeshProUGUI[] text_times;

    // We should reset this variable when the game is restarted.
    bool isTimerStarted;

    Dictionary<string, Sprite> challengeTypeToSprite;

    private void Start()
    {
        // LoadSprites();

        gameController = GameController.Instance;
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
        gameController.UpdateCountDownText += UpdateCountDownText;
        gameController.UpdateTimeText += UpdateTime;
        FillPickerLists();
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

        #region OLD CODES
        /*
         * WE DONT NEED THESE CODES RIGHT NOW
        // Fill absolute errors.
        int[] errors = gameController.absoluteErrors;
        VerticalScrollSnap errorSnap = errorContent.parent.gameObject.GetComponent<VerticalScrollSnap>();
        errorSnap.RemoveAllChildren(out removed);
        foreach (GameObject go in removed)
            Destroy(go);

        for (int i = 0; i < errors.Length; i++)
        {
            GameObject page = Instantiate(pageSmallPrefab, errorContent);
            page.GetComponentInChildren<TextMeshProUGUI>().text = (errors[i] / 1000f).ToString();

            int index = i;
            page.GetComponent<Button>().onClick.AddListener(delegate
            {
                errorSnap.ChangePage(index);
                errorContent.parent.parent.gameObject.GetComponent<ScrollView>().ToggleListSize();
            });
        }
        errorSnap.UpdateLayout();


        // Fill time intervals.
        int[] timeIntervals = gameController.timeIntervals;
        VerticalScrollSnap timeIntervalSnap = timeIntervalContent.parent.gameObject.GetComponent<VerticalScrollSnap>();
        timeIntervalSnap.RemoveAllChildren(out removed);
        foreach (GameObject go in removed)
            Destroy(go);

        for (int i = 0; i < timeIntervals.Length; i++)
        {
            GameObject page = Instantiate(pageSmallPrefab, timeIntervalContent);
            page.GetComponentInChildren<TextMeshProUGUI>().text = (timeIntervals[i] / 1000f).ToString();

            int index = i;
            page.GetComponent<Button>().onClick.AddListener(delegate
            {
                timeIntervalSnap.ChangePage(index);
                timeIntervalContent.parent.parent.gameObject.GetComponent<ScrollView>().ToggleListSize();
            });
        }
        timeIntervalSnap.UpdateLayout();


        // Fill lap counts.
        int[] lapCounts = gameController.lapCounts;
        VerticalScrollSnap lapCountSnap = lapCountContent.parent.gameObject.GetComponent<VerticalScrollSnap>();
        lapCountSnap.RemoveAllChildren(out removed);
        foreach (GameObject go in removed)
            Destroy(go);

        for (int i = 0; i < lapCounts.Length; i++)
        {
            GameObject page = Instantiate(pageSmallPrefab, lapCountContent);
            page.GetComponentInChildren<TextMeshProUGUI>().text = lapCounts[i].ToString();

            int index = i;
            page.GetComponent<Button>().onClick.AddListener(delegate
            {
                lapCountSnap.ChangePage(index);
                lapCountContent.parent.parent.gameObject.GetComponent<ScrollView>().ToggleListSize();
            });
        }
        lapCountSnap.UpdateLayout();


        // Fill lap counts for increment
        int[] incrementCounts = gameController.lapCountsForIncrement;
        VerticalScrollSnap incrementCountsSnap = incrementLapContent.parent.gameObject.GetComponent<VerticalScrollSnap>();
        incrementCountsSnap.RemoveAllChildren(out removed);
        foreach (GameObject go in removed)
            Destroy(go);

        for (int i = 0; i < incrementCounts.Length; i++)
        {
            GameObject page = Instantiate(pageSmallPrefab, incrementLapContent);
            page.GetComponentInChildren<TextMeshProUGUI>().text = incrementCounts[i].ToString();

            int index = i;
            page.GetComponent<Button>().onClick.AddListener(delegate
            {
                incrementCountsSnap.ChangePage(index);
                incrementLapContent.parent.parent.gameObject.GetComponent<ScrollView>().ToggleListSize();
            });
        }
        incrementCountsSnap.UpdateLayout();
        */
        #endregion
    }

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

    private void UpdateCountDownText(object value)
    {
        foreach(TextMeshProUGUI countdown in text_Countdowns)
        {
            if (countdown.gameObject.activeSelf == false)
                countdown.gameObject.SetActive(true);

            countdown.text = value.ToString();
        }
    }

    private void UpdateTimeInterval(object timeInterval)
    {
        // FIXME:
        if (text_TimeInterval_TopPlayer.text != timeInterval.ToString())
            text_TimeInterval_TopPlayer.gameObject.SetActive(false);

        text_TimeInterval_BottomPlayer.text = text_TimeInterval_TopPlayer.text = "Count up to " + ((int)timeInterval / 1000).ToString();
        
        text_TimeInterval_TopPlayer.gameObject.SetActive(true);
        text_TimeInterval_BottomPlayer.gameObject.SetActive(true);
    }

    private void UpdateError(object error)
    {
        GameObject go = Instantiate(animatedTextPrefab, errorTextContainer);
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

    private void UpdateTime(object obj)
    {
        foreach(TextMeshProUGUI t in text_times)
        {
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
            if (isTimerStarted == false)
            {
                StartCoroutine(FadeTextToZeroAlpha(.7f, t));
            }
        }
        isTimerStarted = true;
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
    public void ScrollViewChallengeType(int selectedPage)
    {
        gameController.currChallenge.Type = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[selectedPage]));

        switch (gameController.currChallenge.Type)
        {
            case ChallengeType.Infinite:
                SetChallengeSettings(2000, 400, 3);
                Debug.Log(gameController.currChallenge.TimeInterval);
                break;
            case ChallengeType.Random:
                //
                SetChallengeSettings(-1, 400, -1, 1, 5);
                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeInterval"></param>
    /// <param name="error"></param>
    /// <param name="lapCountForInc"></param>
    /// <param name="rl">random lower limit</param>
    /// <param name="ru">random upper limit</param>
    private void SetChallengeSettings(int timeInterval, int error, int lapCountForInc, int rl = 0, int ru = 0)
    {
        if (error != -1)
            gameController.currChallenge.AbsoluteError = error;
        if (timeInterval != -1)
            gameController.currChallenge.StartInterval = gameController.currChallenge.TimeInterval = timeInterval;
        if (lapCountForInc != -1)
            gameController.currChallenge.LapCountForIncrement = lapCountForInc;

        gameController.currChallenge.RandomLowerBound = rl;
        gameController.currChallenge.RandomUpperBound = ru;

    }

}
