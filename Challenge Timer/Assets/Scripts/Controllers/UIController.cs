using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textTimeInterval;
    public TextMeshProUGUI textError;
    public TextMeshProUGUI textSecond;
    public TextMeshProUGUI textMillisec;

    private void Start()
    {
        GameController.Instance.UpdateTimeInterval += UpdateTimeInterval;
        GameController.Instance.UpdateError += UpdateError;
    }

    private void UpdateTimeInterval(int timeInterval)
    {
        textTimeInterval.text = (timeInterval / 1000).ToString();
    }

    private void UpdateError(int error)
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, Math.Abs(error));

        textError.text = error > 0 ? "+" : "-";
        textError.text += dt.Second + "." + dt.Millisecond + "";
    }


    private void UpdateTime(int second, int millisec)
    {
        string secondStr = "";
        string millisecStr = "";

        if (second < 10)
            secondStr += "0";

        secondStr += second;

        if (millisec < 10)
            millisecStr += "00";
        else if (millisec < 100)
            millisecStr += "0";

        millisecStr += millisec;

        textSecond.text = secondStr;
        textMillisec.text = "." + millisecStr;
    }
}
