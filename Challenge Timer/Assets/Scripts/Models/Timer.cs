using System;
using System.Collections.Generic;

public class Timer
{
    private long currTime;
    private long startTime;
    private long elapsedTime;
    private float maxTimeOutValue = 10000;
    private float timeOutValue=0;
    private List<int> lapTimes;

    private bool timerStopped;

    public Timer()
    {
        lapTimes = new List<int>();
        timerStopped = true;
    }

    public void Start()
    {
        startTime = CurrTime;
        timerStopped = false;
    }
    //LT8H1Q3CAZPU
    public void Stop()
    {
        long deltaTime = CurrTime - startTime;
        elapsedTime += deltaTime;
        timerStopped = true;
    }
    public bool TimeOutCheck(float time)
    {
        timeOutValue += time;
        if (timeOutValue >= maxTimeOutValue)
            return true;

        return false;
    }
    public int Lap()
    {
        long deltaTime = CurrTime - startTime;
        lapTimes.Add((int) deltaTime);

        elapsedTime += deltaTime;
        startTime = CurrTime;

        timeOutValue = 0;
        return (int) deltaTime;
    }
    
    public long ElapsedTime
    {
        get
        {
            long deltaTime;
            if (timerStopped == false)
            {
                deltaTime = CurrTime - startTime;
                return elapsedTime + deltaTime;
            }
            else
                return elapsedTime;
        }
    }

    public List<int> LapTimes
    {
        get
        {
            return lapTimes;
        }
    }

    public long CurrTime
    {
        get
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalMilliseconds;
        }
    }

}
