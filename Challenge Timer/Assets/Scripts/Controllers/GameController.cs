using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int[] timeIntervals;
    public int[] absoluteErrors;
    public int[] totalLapCounts;
    public int[] lapCountsForIncrement;

    private Challenge[] challenges;
    private Challenge currChallenge;
    private Timer timer;

    private bool isGameStarted;

    public Challenge[] Challenges
    {
        get
        {
            return challenges;
        }

       protected set
        {
          challenges = value;
        }
    }

    public delegate void UI_EventHandler(int value);
    public event UI_EventHandler UpdateTimeInterval;
    public event UI_EventHandler UpdateError;

	void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        timer = new Timer();

        CreateChallenges();
        InitializeOptions();

        currChallenge = Challenges[1];
	}

    private void CreateChallenges()
    {
        Challenges = new Challenge[]
        {
            new Challenge()
            {
                Name = "Finite 1",
                Description = "...",
                Type = ChallengeType.Finite,

                TimeInterval = 5000,
                AbsoluteError = 1000
            },

            new Challenge()
            {
                Name = "Random",
                Description = "...",
                Type = ChallengeType.Random,
                RandomL = 1,
                RandomR = 5,
                AbsoluteError = 1000
            },
            new Challenge()
            {
                Name="I am Smart",
                Description="...",
                Type=ChallengeType.Infinite,
                StartInterval=2000,
                LapCountForIncrement=3,
                AbsoluteError=300
        
            },

            new Challenge()
            {
                Name = "Random",
                Description = "...",
                Type = ChallengeType.Random,
                RandomL = 3,
                RandomR = 10,
                AbsoluteError = 400
            }
        };
    }

    private void InitializeOptions()
    {
        timeIntervals = new int[] { 1000, 2000, 3000, 5000 };
        absoluteErrors = new int[] { 100, 200, 500, 1000 };
        totalLapCounts = new int[] { 1, 2, 3 };
        lapCountsForIncrement = new int[] { 2, 3, 5 };
    }

    public void ButtonPressed_StartStop()
    {
        if (isGameStarted == true)
        {
            int lapTime = timer.Lap();

            if (currChallenge.IsFailed(lapTime) == false)
            {
                int error = currChallenge.GetError(lapTime);
                UpdateError(error);
            }
            else
            {
                currChallenge.Reset();
                timer.Stop();
                isGameStarted = false;

                // Show that we failed. Update UI.
            }
        }
        else
        {
            timer.Start();
            isGameStarted = true;
        }

        UpdateTimeInterval(currChallenge.GetNextTimeInterval());
    }
}
