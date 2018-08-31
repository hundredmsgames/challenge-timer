using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel100msScript : MonoBehaviour
{
    public void AnimationEnded()
    {
        Destroy(this.gameObject);
    }
}
