using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    List<Challenge> challengeList;
    
	void Start ()
    {
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
