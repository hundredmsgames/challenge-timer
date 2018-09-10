using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoPanelScript : MonoBehaviour
{
    public GameObject panel;

    public void AnimationEnded()
    {
        Destroy(panel);
    }
}
