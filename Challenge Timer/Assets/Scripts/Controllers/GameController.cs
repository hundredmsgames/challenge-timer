using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private List<Challenge> challengeList;
    private Challenge currChallenge;
    private Timer timer;

    private bool isGameStarted;

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

        currChallenge = challengeList[0];
	}

    private void CreateChallenges()
    {
        challengeList = new List<Challenge>()
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
            }
        };
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
        }
        else
        {
            timer.Start();
            isGameStarted = true;
        }

        UpdateTimeInterval(currChallenge.GetNextTimeInterval());
    }
}
