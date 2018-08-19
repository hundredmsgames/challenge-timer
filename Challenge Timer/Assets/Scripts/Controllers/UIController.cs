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
    public Transform errorContent;
    public Transform timeIntervalContent;
    public Transform lapCountContent;
    public Transform incrementLapContent;

    public GameObject pageMediumPrefab;
    public GameObject pageSmallPrefab;
    public GameObject animatedTextPrefab;

    public GameObject panel_Menu;
    public GameObject panel_Game;

    public Transform errorTextContainer;

    public TextMeshProUGUI text_TimeInterval;
    public TextMeshProUGUI text_Countdown;
    public TextMeshProUGUI text_time;

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
        if (text_Countdown.gameObject.activeSelf == false)
            text_Countdown.gameObject.SetActive(true);

        text_Countdown.text = value.ToString();
    }

    private void UpdateTimeInterval(object timeInterval)
    {
        if (text_TimeInterval.text != timeInterval.ToString())
            text_TimeInterval.gameObject.SetActive(false);

        text_TimeInterval.text = "Count up to " + ((int)timeInterval / 1000).ToString();
        text_TimeInterval.gameObject.SetActive(true);
    }

    private void UpdateError(object error)
    {
        GameObject go = Instantiate(animatedTextPrefab, errorTextContainer);
        TextMeshProUGUI textError = go.GetComponentInChildren<TextMeshProUGUI>();
        go.name = "Text_Error";

        int second = Math.Abs((int)error / 1000);
        int millisec = Math.Abs((int)error % 1000);

        string secondStr = "";
        string millisecStr = "";

        if (second > 0 && second < 10)
            secondStr += "0";

        secondStr += second;

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
        
        long time = (long)obj;
        if (text_time.gameObject.activeSelf == false)
            text_time.gameObject.SetActive(true);

        int seconds = (int)(time / 1000);
        int ms = (int)(time - seconds * 1000);

       
        text_time.text = seconds.ToString() + "   :   " + ms.ToString();
        
        //FIXME::
        //I want to start coroutine in a spesific time interval
        //we can find a better way
        if (seconds == 0 && ms > 100 && ms < 120)
        {
            StartCoroutine(FadeTextToZeroAlpha(.7f, text_time, Time.deltaTime));
        }

    }

    //https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    //I had the almost same idea but this looks way better than my idea
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI text, float deltaTime)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (deltaTime / t));
            yield return null;
        }

    }
    public void ScrollViewChallengeType(int selectedPage)
    {
        gameController.currChallenge.Type = (ChallengeType)(Enum.Parse(typeof(ChallengeType), gameController.challengeTypes[selectedPage]));
    }

    public void ScrollViewChallengeError(int selectedPage)
    {
        gameController.currChallenge.AbsoluteError = gameController.absoluteErrors[selectedPage];
    }

    public void ScrollViewChallengeTimeInterval(int selectedPage)
    {
        gameController.currChallenge.TimeInterval = gameController.timeIntervals[selectedPage];
    }

    public void ScrollViewChallengeLapCount(int selectedPage)
    {
        gameController.currChallenge.LapCount = gameController.lapCounts[selectedPage];
    }

    public void ScrollViewChallengeIncrementLapPicker(int selectedPage)
    {
        gameController.currChallenge.LapCountForIncrement = gameController.lapCountsForIncrement[selectedPage];
    }
}
