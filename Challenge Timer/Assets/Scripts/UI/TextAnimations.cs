﻿using UnityEngine;

public class TextAnimations : MonoBehaviour
{
    public void Deactive()
    {
        if (this.transform.parent.name == "CountDown")
            this.gameObject.SetActive(false);
        else
            Destroy(this.gameObject);
    }
}
