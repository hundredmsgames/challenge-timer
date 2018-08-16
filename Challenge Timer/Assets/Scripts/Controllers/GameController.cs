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

    int countDown = 3;

    public Challenge currChallenge;
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
        
        InitializeOptions();

	}
    private void InitializeOptions()
    {
        challengeTypes = new string[] { "Finite", "Infinite", "Random" };
        timeIntervals = new int[] { 1000, 2000, 3000, 5000 };
        absoluteErrors = new int[] { 100, 200, 500 };
        lapCounts = new int[] { 1, 2, 3 };
        lapCountsForIncrement = new int[] { 3, 5 };
    }

    public void ButtonPressed_StartStop()
    {
        if (isGameStarted == true)
        {
            int lapTime = timer.Lap();

            
        }
        else
        {
            timer.Start();
            isGameStarted = true;
        }

       // UpdateTimeInterval(CurrChallenge.GetNextTimeInterval());
    }
    private void Update()
    {
        if(isGameStarted && countDown > 0 )
        {
            //show on screen with Text obj
            countDown--;
            return;
        }
        if(isGameStarted && countDown == 0)
        {
            
            //write Go on text object
            timer.Start();
            countDown--;
        }
        if(isGameStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {

            }

        }
    }
}
