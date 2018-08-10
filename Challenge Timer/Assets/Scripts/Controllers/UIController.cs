using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameController gameController;

    public GameObject panel_Menu;
    public GameObject panel_Challenges;
    public GameObject panel_Game;

    public TextMeshProUGUI text_TimeInterval;
    public TextMeshProUGUI text_Error;

    private void Start()
    {
        gameController = GameController.Instance;
        gameController.UpdateTimeInterval += UpdateTimeInterval;
        gameController.UpdateError += UpdateError;
    }

    public void ButtonPressed_OpenChallenges()
    {
        Debug.Log("Open Challenges");
        panel_Menu.SetActive(false);
        panel_Challenges.SetActive(true);
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
