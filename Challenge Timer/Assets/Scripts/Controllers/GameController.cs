using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

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

    float countDown = 4f;
    public int playerCount = 2;

    void Awake()
    {
        if (Instance != null)
            return;

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
                UpdateCountDownText("failed", playerIdx);
            }
            else
            {
                UpdateError(lapTime - currInterval, playerIdx);
            }

            // show what current interval is
            UpdateTimeInterval(challenges[playerIdx].GetNextTimeInterval(), playerIdx);
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
