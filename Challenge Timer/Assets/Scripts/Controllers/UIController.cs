using System;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject panel_Menu;
    public GameObject panel_Game;

    public TextMeshProUGUI text_TimeInterval;
    public TextMeshProUGUI text_Error;

    Dictionary<string, Sprite> challengeTypeToSprite;

    private void Start()
    {
       // LoadSprites();

        gameController = GameController.Instance;
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
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


    /*
    public void CreateUICards()
    {
        for (int i = 0; i < gameController.Challenges.Length; i++)
        {
            GameObject go = Instantiate(cardPrefab, cardParent);
            //set the name of the challenge
            go.GetComponentInChildren<TextMeshProUGUI>().text = gameController.Challenges[i].Name;

            Transform parentObject = go.transform.Find("CardUI");
            Transform goFound = parentObject.Find(cardUIElements[0].name);
            //set time interval
            goFound.GetComponentInChildren<TextMeshProUGUI>().text = gameController.Challenges[i].TimeInterval.ToString() + " sec";
            
            goFound = parentObject.Find(cardUIElements[1].name);
            //set absolute error
            goFound.GetComponentInChildren<TextMeshProUGUI>().text = gameController.Challenges[i].AbsoluteError.ToString() + " ms";

            goFound = parentObject.Find(cardUIElements[2].name);
            //set score
            goFound.GetComponentInChildren<TextMeshProUGUI>().text = "26 sec";

            goFound = parentObject.Find(cardUIElements[3].name);
            //set lap count
            goFound.GetComponentInChildren<TextMeshProUGUI>().text = gameController.Challenges[i].NumberOfLap.ToString();

            goFound = parentObject.Find(cardUIElements[4].name);
            //set increment
            goFound.GetComponentInChildren<TextMeshProUGUI>().text = gameController.Challenges[i].LapCountForIncrement.ToString();

            goFound = parentObject.Find(cardUIElements[5].name);
            //set type
            Sprite spriteForType = challengeTypeToSprite[gameController.Challenges[i].Type.ToString()];
            Debug.Log(goFound.name);
            goFound.GetComponentsInChildren<Image>()[1].sprite =spriteForType;
        }
    }
    */


    public void ButtonPressed_OpenChallenges()
    {
        Debug.Log("Open Challenges");
        panel_Menu.SetActive(false);
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


    private void UpdateTimeInterval(int timeInterval)
    {
        text_TimeInterval.text = (timeInterval / 1000).ToString();
    }

    private void UpdateError(int error)
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, Math.Abs(error));

        text_Error.text = error > 0 ? "+" : "-";
        text_Error.text += dt.Second + "." + dt.Millisecond + "";
    }
}
