using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge  {

    string name;
    ChallengeLevel challengeLevel;
    int timeInterval;
    float error;
    public delegate void ChallengeHandler(object sender, string name);
    public event ChallengeHandler ChallengeCompleted;

}
