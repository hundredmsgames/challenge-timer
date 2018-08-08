using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textSecond;
    public TextMeshProUGUI textMillisec;

    private void Start()
    {
        GameController.Instance.UpdateTimeCallback += UpdateTime;
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
