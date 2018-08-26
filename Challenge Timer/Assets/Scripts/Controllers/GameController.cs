using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    int maxPoint = 5;
    int p1_points=0;
    int p2_points=0;
    public Challenge[] challenges;
    private Timer[] timer;
    public string[] challengeTypes;

    private bool isGameStarted;
    private bool isCountDownStarted;
    private bool timeIntervalPopUp;

    public delegate void UI_EventHandler(object value, int playerIdx);
    public event UI_EventHandler UpdateTimeInterval;
    public event UI_EventHandler UpdateError;
    public event UI_EventHandler UpdateCountDownText;
    public event UI_EventHandler UpdateTimeText;
    public event UI_EventHandler UpdateWinLoseText;
    public event UI_EventHandler UpdateFailedText;
    public event UI_EventHandler RestartUI;

    float defaultCountDown = 4f;
    float countDown;
    public int playerCount = 2;

    void Awake()
    {
        if (Instance != null)
            return;

        countDown = defaultCountDown;
        Instance = this;
        challenges = new Challenge[2];
        timer = new Timer[playerCount];
        challengeTypes = new string[] { "Infinite", "Random" };

        for (int i = 0; i < timer.Length; i++)
            timer[i] = new Timer();       
    }

    public void StartGame()
    {
        isCountDownStarted = true;
    }

    public void RestartGame()
    {
        isCountDownStarted = true;
        timeIntervalPopUp = false;
        countDown = defaultCountDown;
        RestartUI("", 0);

        for (int i = 0; i < playerCount; i++)
        {
            timer[i] = new Timer();
        }

        switch (challenges[0].Type)
        {
            case ChallengeType.Infinite:
                SetChallengeSettings(ChallengeType.Infinite, 2000, 400, 3);
                break;
            case ChallengeType.Random:
                SetChallengeSettings(ChallengeType.Random, -1, 400, -1, 1, 5);
                break;
            default:
                break;
        }
        
        p1_points = 0;
        p2_points = 0;
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

    // Player 1 -> Top
    // Player 2 -> Bottom
    public void ButtonPressed_Lap(int playerIdx)
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
                    p2_points++;
                else
                    p1_points++;
                //update appropriate players game stat object(UPDATE SPRITE??)
            }
            else
            {
                UpdateError(lapTime - currInterval, playerIdx);
            }

            // show what current interval is
            UpdateTimeInterval(challenges[playerIdx].GetNextTimeInterval(), playerIdx);
        }   
        if(p1_points == maxPoint || p2_points == maxPoint)
        {
            for (int i = 0; i < playerCount; i++)
            {
                timer[i].Stop();
            }
            isGameStarted = false;
            if (p1_points == maxPoint)
                UpdateWinLoseText("", 0);
            else
                UpdateWinLoseText("", 1);
            //end game 
            //show the failed text to appropriate player
            //show the you won text to appropriate player

        }
    }

    private void Update()
    {
        if (isCountDownStarted && countDown > 1f)
        {
            //show on screen with Text obj
            UpdateCountDownText((int)countDown, 0);
            UpdateCountDownText((int)countDown, 1);
            countDown -= Time.deltaTime;
            return;
        }

        if (isCountDownStarted && countDown > 0f)
        {
            //write Go on text object
            UpdateCountDownText("GO", 0);
            UpdateCountDownText("GO", 1);
            countDown -= Time.deltaTime;

            if (timeIntervalPopUp == false && countDown < 0.5f)
            {
                timeIntervalPopUp = true;
                for (int i = 0; i < playerCount; i++)
                    UpdateTimeInterval(challenges[i].GetNextTimeInterval(), i);
            }
        }

        if (isCountDownStarted && countDown <= 0f)
        {
            for (int i = 0; i < playerCount; i++)
                timer[i].Start();

            isCountDownStarted = false;
            isGameStarted = true;
        }

        if (UpdateTimeText != null && timeIntervalPopUp)
        {
            for (int i = 0; i < playerCount; i++)
                UpdateTimeText(timer[i].ElapsedTime, i);
        }
    }
}
