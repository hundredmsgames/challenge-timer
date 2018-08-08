using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge
{
    // Name of the challenge
    string name;

    // Small informative text about the challenge
    string description;

    // Type of the challenge
    ChallengeType type;

    // Time interval for finite and infinite challenges.
    // Time unit is milliseconds.
    int timeInterval;

    // Absolute error. In milliseconds.
    int absoluteError;

    // This is for infinite challenges. We will increase the
    // time interval by a certain amount of lap count.
    int lapCountForIncrement;

    // This is for random challenges. Inclusive left bound.
    int randomL;

    // This is for random challenges. Inclusive right bound.
    int randomR;


    int currLap;
    int currTimeInterval;


    public int GetNextTimeInterval()
    {
        if (currLap == 0)
            currTimeInterval = timeInterval;

        currLap++;
        return currTimeInterval;
    }



    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public ChallengeType Type
    {
        get {
            return type;
        }

        set {
            type = value;
        }
    }

    public int TimeInterval
    {
        get
        {
            return timeInterval;
        }

        set
        {
            timeInterval = value;
        }
    }

    public int AbsoluteError
    {
        get
        {
            return absoluteError;
        }

        set
        {
            absoluteError = value;
        }
    }

    public int LapCountForIncrement
    {
        get
        {
            return lapCountForIncrement;
        }

        set
        {
            lapCountForIncrement = value;
        }
    }

    public int RandomL
    {
        get
        {
            return randomL;
        }

        set
        {
            randomL = value;
        }
    }

    public int RandomR
    {
        get
        {
            return randomR;
        }

        set
        {
            randomR = value;
        }
    }
}
