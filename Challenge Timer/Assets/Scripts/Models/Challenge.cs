using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        switch (type)
        {
            case ChallengeType.Finite:
                //if we are in finite game mode so we will have the same timeInterval always
                currTimeInterval = timeInterval;
                break;
            case ChallengeType.Infinite:
                //if we are in infinite mode we will change interval every X lap
                if (currLap >= lapCountForIncrement)
                {
                    //increment timeInterval 1
                    timeInterval++;
                    //set currTimeInterval equal to timeInterval
                    currTimeInterval = timeInterval;
                    //reset current lap count
                    currLap = 0;
                }
                break;
            case ChallengeType.Random:
                System.Random rnd = new System.Random();
                int randomInterval = rnd.Next(randomL, randomR);
                timeInterval = randomInterval;
                currTimeInterval = timeInterval;
                break;
            default:
                break;
        }

        if (currLap == 0)
            currTimeInterval = timeInterval;

        currLap++;
        return currTimeInterval;
    }

    //if we make a big mistake we get bigger errors so game will end
    public bool CheckGameState(float lapTime)
    {
        return !((lapTime - timeInterval) > absoluteError);
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
