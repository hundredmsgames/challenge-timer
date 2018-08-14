using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameController gameController;

    public GameObject panel_Menu;
    public GameObject panel_Game;

    public TextMeshProUGUI text_TimeInterval;
    public TextMeshProUGUI text_Error;

    public GameObject cardPrefab;
    public Transform cardParent;
    public Transform[] cardUIElements;

    Dictionary<string, Sprite> challengeTypeToSprite;

    private void Start()
    {

        LoadSprites();

        gameController = GameController.Instance;
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;

        ///FOR TESTING
        CreateUICards();
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
}
