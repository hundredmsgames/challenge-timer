using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private List<Challenge> challengeList;
    private Timer timer;

    private bool isGameStarted;

    public delegate void UpdateTimeHandler(int second, int millisec);
    public event UpdateTimeHandler UpdateTimeCallback;

	void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;
        timer = new Timer();

        CreateChallenges();	
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
                TimeInterval = 1000,
                AbsoluteError = 200
            },

            new Challenge()
            {
                Name = "Finite 2",
                Description = "...",
                Type = ChallengeType.Finite,
                TimeInterval = 2000,
                AbsoluteError = 300
            }
        };
    }
	
	void Update ()
    {
        long time = timer.ElapsedTime;
        UpdateTimeCallback((int)(time / 1000), (int)(time % 1000));
	}

    public void ButtonPressed_StartStop()
    {
        if (isGameStarted == true)
            timer.Stop();
        else
            timer.Start();

        isGameStarted = !isGameStarted;
    }
}
