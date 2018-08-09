﻿using System.Collections;
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

    // Start interval of challenge
    int startInterval;

    // Time interval for finite and infinite challenges.
    // Time unit is milliseconds.
    int timeInterval;

    // Absolute error. In milliseconds.
    int absoluteError;

    // This is for finite challenges. We need to know
    // total number of lap that player should play.
    int numberOfLap;

    // This is for infinite challenges. We will increase the
    // time interval by a certain amount of lap count.
    int lapCountForIncrement;

    // This is for random challenges. Inclusive left bound.
    int randomL;

    // This is for random challenges. Inclusive right bound.
    int randomR;

    int currLap;

    public int GetNextTimeInterval()
    {
        switch (type)
        {
            case ChallengeType.Finite:
                //if we are in finite game mode so we will have the same timeInterval always
                break;
            case ChallengeType.Infinite:
                //if we are in infinite mode we will change interval every X lap
                if (currLap != 0 && currLap % lapCountForIncrement == 0)
                {
                    //increment timeInterval 1
                    timeInterval++;
                }
                break;
            case ChallengeType.Random:
                System.Random rnd = new System.Random();
                int randomInterval = rnd.Next(randomL, randomR + 1);
                timeInterval = randomInterval * 1000;
                break;
            default:
                break;
        }

        currLap++;
        return timeInterval;
    }

    //if we make a big mistake we get bigger errors so game will end
    public bool IsFailed(int lapTime)
    {
        return Math.Abs(lapTime - timeInterval) >= absoluteError;
    }

    public int GetError(int lapTime)
    {
        return lapTime - timeInterval;
    }

    public void Reset()
    {
        timeInterval = startInterval;
        currLap = 0;
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

    public int StartInterval
    {
        get
        {
            return startInterval;
        }

        set
        {
            startInterval = value;
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

    public int NumberOfLap
    {
        get
        {
            return numberOfLap;
        }

        set
        {
            numberOfLap = value;
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