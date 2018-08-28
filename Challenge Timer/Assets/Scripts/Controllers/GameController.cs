using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    int maxPoint = 5;
    int p1_points;
    int p2_points;
    int p1_sets = 0;
    int p2_sets = 0;

    public Challenge[] challenges;
    private Timer[] timer;
    public string[] challengeTypes;

    public bool isGameStarted;
    public bool isCountDownRunning;
    private bool timeIntervalPopUp;

    public delegate void UI_EventHandlerNoParams();
    public delegate void UI_EventHandlerWithParams(object value, int playerIdx);
    public event UI_EventHandlerNoParams HideScorePanel;
    public event UI_EventHandlerNoParams ShowScorePanel;
    public event UI_EventHandlerNoParams RestartUI;
    public event UI_EventHandlerNoParams HideTimers;
    public event UI_EventHandlerWithParams UpdateTimeInterval;
    public event UI_EventHandlerWithParams UpdateError;
    public event UI_EventHandlerWithParams UpdateCountDownText;
    public event UI_EventHandlerWithParams UpdateTimeText;
    public event UI_EventHandlerWithParams UpdateWinLoseText;
    public event UI_EventHandlerWithParams UpdateFailedText;
    public event UI_EventHandlerWithParams UpdateInfoSprites;

    float defaultCountDown = 4f;
    float countDown;
    public int playerCount = 2;

    void Awake()
    {
        if (Instance != null)
            return;

        StringLiterals.language = Language.TURKISH;
        p1_points = p2_points = maxPoint;
        countDown = defaultCountDown;
        Instance = this;
        challenges = new Challenge[2];
        timer = new Timer[playerCount];
        challengeTypes = new string[] { "Infinite", "Random", "Kids" };

        for (int i = 0; i < timer.Length; i++)
            timer[i] = new Timer();
    }

    public void StartGame()
    {
        RestartGame();
        p1_sets = 0;
        p2_sets = 0;
    }

    public void RestartGame()
    {
        isCountDownRunning = true;
        timeIntervalPopUp = false;
        countDown = defaultCountDown;

        for (int i = 0; i < playerCount; i++)
        {
            timer[i] = new Timer();
        }

        switch (challenges[0].Type)
        {
            case ChallengeType.Infinite:
            case ChallengeType.Kids:
                SetChallengeSettings(challenges[0].Type, 2000, 500, 3);
                break;
            case ChallengeType.Random:
                SetChallengeSettings(challenges[0].Type, -1, 400, -1, 1, 5);
                break;
            default:
                break;
        }

        p1_points = maxPoint;
        p2_points = maxPoint;
    }

    // Player 1 -> Top
    // Player 2 -> Bottom
    public void Lap(int playerIdx)
    {
        if (isGameStarted == true)
        {
            int currInterval = challenges[playerIdx].TimeInterval;
            int absError = challenges[playerIdx].AbsoluteError;
            int lapTime = timer[playerIdx].Lap();

            if (Mathf.Abs(lapTime - currInterval) > absError)
            {
                //failed
                UpdateFailedText("", playerIdx);

                if (playerIdx == 0)
                {
                    p1_points--;
                    UpdateInfoSprites(p1_points, 0);
                }
                else
                {
                    p2_points--;
                    UpdateInfoSprites(p2_points, 1);
                }
                //update appropriate players game stat object(UPDATE SPRITE??)

            }
            else
            {
                UpdateError(lapTime - currInterval, playerIdx);
            }

            // show what current interval is
            UpdateTimeInterval(challenges[playerIdx].GetNextTimeInterval(), playerIdx);
        }

        if (p1_points == 0 || p2_points == 0)
        {
            if (isGameStarted == false)
                return;

            for (int i = 0; i < playerCount; i++)
                timer[i].Stop();

            if (p1_points == 0)
                UpdateWinLoseText(++p2_sets, 1);
            else
                UpdateWinLoseText(++p1_sets, 0);

            HideTimers();
            ShowScorePanel();
            isGameStarted = false;

            StartCoroutine(
                WaitForAnims(1.2f, () =>
                {
                    UIController.Instance.nextRound = true;
                })
            );
        }
    }

    private void Update()
    {
        if (isCountDownRunning && countDown > 1f)
        {
            //show on screen with Text obj
            UpdateCountDownText((int)countDown, 0);
            UpdateCountDownText((int)countDown, 1);
            countDown -= Time.deltaTime;
            return;
        }

        if (isCountDownRunning && countDown > 0f)
        {
            //write Go on text object
            UpdateCountDownText(StringLiterals.CountDownObjectText, 0);
            UpdateCountDownText(StringLiterals.CountDownObjectText, 1);
            countDown -= Time.deltaTime;

            if (timeIntervalPopUp == false && countDown < 0.5f)
            {
                timeIntervalPopUp = true;
                for (int i = 0; i < playerCount; i++)
                    UpdateTimeInterval(challenges[i].GetNextTimeInterval(), i);
            }
        }

        if (isCountDownRunning && countDown <= 0f)
        {
            for (int i = 0; i < playerCount; i++)
                timer[i].Start();

            isCountDownRunning = false;
            isGameStarted = true;
        }

        if (UpdateTimeText != null && isCountDownRunning == false)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (challenges[0].Type == ChallengeType.Kids)
                    UpdateTimeText(timer[i].LapTime, i);
                else
                    UpdateTimeText(timer[i].ElapsedTime, i);
            }


        }
        if (isGameStarted && isCountDownRunning == false)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (timer[i].LapTime > challenges[i].TimeInterval + challenges[i].AbsoluteError)
                {
                    Lap(i);
                }
            }
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
    public void SetChallengeSettings(ChallengeType type, int timeInterval, int error, int lapCountForInc, int rl = 0, int ru = 0)
    {
        for (int i = 0; i < playerCount; i++)
        {
            challenges[i] = new Challenge();
            challenges[i].Type = type;

            if (error != -1)
                challenges[i].AbsoluteError = error;
            if (timeInterval != -1)
                challenges[i].StartInterval = challenges[i].TimeInterval = timeInterval;
            if (lapCountForInc != -1)
                challenges[i].LapCountForIncrement = lapCountForInc;

            challenges[i].RandomLowerBound = rl;
            challenges[i].RandomUpperBound = ru;
        }
    }

    IEnumerator WaitForAnims(float time, Action func)
    {
        yield return new WaitForSeconds(time);

        if (func != null)
            func();
    }
}
