using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge  {

    string name;
    string description;
    int timeInterval;
    float error;
    int lapCountForIncrement;
    int randomL;
    int randomR;
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

    public float Error
    {
        get
        {
            return error;
        }

        set
        {
            error = value;
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
