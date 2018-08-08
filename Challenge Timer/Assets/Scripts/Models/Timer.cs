using System;
using System.Collections.Generic;

public class Timer
{
    private long currTime;
    private long startTime;
    private long elapsedTime;
    private List<int> lapTimes;

    public Timer()
    {
        lapTimes = new List<int>();
    }

    public void Start()
    {
        startTime = CurrTime;
    }

    public void Stop()
    {
        long deltaTime = CurrTime - startTime;
        elapsedTime += deltaTime;
    }

    public void Lap()
    {
        long deltaTime = CurrTime - startTime;
        lapTimes.Add((int) deltaTime);

        elapsedTime += deltaTime;
        startTime = CurrTime;
    }

    public long ElapsedTime
    {
        get
        {
            long deltaTime = CurrTime - startTime;
            return elapsedTime + deltaTime;
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
