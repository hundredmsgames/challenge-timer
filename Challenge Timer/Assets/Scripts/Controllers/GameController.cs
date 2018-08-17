using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;


    public string[] challengeTypes;
    public int[] timeIntervals;
    public int[] absoluteErrors;
    public int[] lapCounts;
    public int[] lapCountsForIncrement;

    float countDown = 4;

    public Challenge currChallenge;
    private Timer timer;

    private bool isGameStarted;



    public delegate void UI_EventHandler(object value);
    public event UI_EventHandler UpdateTimeInterval;
    public event UI_EventHandler UpdateError;
    public event UI_EventHandler UpdateCountDownText;

    void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        timer = new Timer();

        InitializeOptions();

    }
    private void InitializeOptions()
    {
        challengeTypes = new string[] { "Finite", "Infinite", "Random" };
        timeIntervals = new int[] { 1000, 2000, 3000, 5000 };
        absoluteErrors = new int[] { 100, 200, 500 };
        lapCounts = new int[] { 1, 2, 3 };
        lapCountsForIncrement = new int[] { 3, 5 };

        currChallenge.RandomL = 3;
        currChallenge.RandomR = 5;
    }

    public void ButtonPressed_StartStop()
    {
        if (isGameStarted == true)
        {
            int lapTime = timer.Lap();
            if (Mathf.Abs(currChallenge.GetNextTimeInterval() - lapTime) > currChallenge.AbsoluteError)
            {
                //failed
                UpdateCountDownText("failed");

            }
            else
            {
                UpdateError(Mathf.Abs(currChallenge.TimeInterval - lapTime));

                // show what current interval is
                UpdateTimeInterval(currChallenge.TimeInterval);
            }
        }
        else
        {
            timer.Start();
            isGameStarted = true;
        }

        // UpdateTimeInterval(CurrChallenge.GetNextTimeInterval());
    }
    public void StartGame()
    {
        isGameStarted = true;
        UpdateTimeInterval(currChallenge.TimeInterval);
    }
    private void Update()
    {
        if (isGameStarted && countDown > 1)
        {
            //show on screen with Text obj
            UpdateCountDownText((int)countDown);
            countDown -= Time.deltaTime;
            return;
        }
        if (isGameStarted && countDown > 0)
        {

            //write Go on text object
            UpdateCountDownText("GO");

            timer.Start();
            countDown -= Time.deltaTime;
        }

    }
}
