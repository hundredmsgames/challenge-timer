using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class UIController : MonoBehaviour
{
    // This variable ensures that lap button pressed
    // one time to pass next round.
    [NonSerialized]
    public bool nextRound = false;

    // BUTTON EVENTS
    public void ButtonPressed_Lap(int playerIdx)
    {
        if(nextRound == true)
        {
            HideScorePanel();
            nextRound = false;

            StartCoroutine(
                WaitForAnims(1.2f, () => {
                    RestartUI();
                    gameController.RestartGame();
                })
            );
        }   
        else
        {
            gameController.Lap(playerIdx);
        }
    }

    public void RestartButtonPressed()
    {
        HideScorePanel();
        nextRound = false;
        StopAllCoroutines();
        StartCoroutine(
            WaitForAnims(1.2f, () => {
                RestartUI();
                ResetScores();
                gameController.StartGame();
            })
        );
    }

    public void ButtonPressed_MainMenu()
    {
        HideScorePanel();
        nextRound = false;
        StopAllCoroutines();
        StartCoroutine(
            WaitForAnims(1.2f, () => {
                RestartUI();
                ResetScores();
                panel_Game.SetActive(false);
                panel_Menu.SetActive(true);
            })
        );
    }


    // RESTART UI
    private void RestartUI()
    {
        // Reset UI elements
        for (int i = 0; i < gameController.playerCount; i++)
        {
            text_times[i].color = new Color(text_times[i].color.r, text_times[i].color.g, text_times[i].color.b, 1);
            text_times[i].text = "0.000";
            text_times[i].gameObject.SetActive(false);
            winloseObjects[i].SetActive(false);
        }

        //reset Info images
        for (int i = 0; i < spriteObjectContainers.Length; i++)
        {
            Image[] images = spriteObjectContainers[i].GetComponentsInChildren<Image>();
            for (int j = 0; j < images.Length; j++)
            {
                images[j].sprite = nameToSpriteMap["check"];
            }
        }

        allTimersStarted = 0;
    }

    private void ResetScores()
    {
        for (int i = 0; i < gameController.playerCount; i++)
            text_scores[i].text = "0";
       
    }

    private void ShowScorePanel()
    {
        // Show score panel
        showScoreAnimator.SetBool("open", true);
    }

    private void HideScorePanel()
    {
        // Hide score panel
        showScoreAnimator.SetBool("open", false);
    }


    // UPDATE METHODS
    private void UpdateFailedText(object value, int playerIdx)
    {
        failedTextObjects[playerIdx].SetActive(true);
    }

    private void UpdateWinLoseText(object value, int playerIdx)
    {
        int otherPlayer = (playerIdx + 1) % gameController.playerCount;
        text_winlose[playerIdx].text = "You Won";
        text_winlose[otherPlayer].text = "You Lose";

        for (int i = 0; i < gameController.playerCount; i++)
        {
            winloseObjects[i].SetActive(true);
            text_TimeIntervals[i].gameObject.SetActive(false);
        }

        text_losetext[otherPlayer].gameObject.SetActive(true);
        text_scores[playerIdx].text = value.ToString();
    }

    private void UpdateCountDownText(object value, int playerIdx)
    {
        TextMeshProUGUI countdown = text_Countdowns[playerIdx];

        if (countdown.gameObject.activeSelf == false)
            countdown.gameObject.SetActive(true);

        countdown.text = value.ToString();
    }

    private void UpdateTimeInterval(object timeInterval, int playerIdx)
    {
        TextMeshProUGUI interval = text_TimeIntervals[playerIdx];

        // FIXME: If same number comes consecutively in random challenge
        // It will not pop up. Find better way!
        if (interval.text != timeInterval.ToString())
            interval.gameObject.SetActive(false);

        interval.text = "Count up to " + ((int)timeInterval / 1000).ToString();
        interval.gameObject.SetActive(true);
    }

    private void UpdateError(object error, int playerIdx)
    {
        GameObject go = Instantiate(animatedTextPrefab, errorTextContainer[playerIdx]);
        TextMeshProUGUI textError = go.GetComponentInChildren<TextMeshProUGUI>();
        go.name = "Text_Error";

        int seconds = Math.Abs((int)error / 1000);
        int millisec = Math.Abs((int)error % 1000);

        string secondStr = "";
        string millisecStr = "";

        if (seconds > 0 && seconds < 10)
            secondStr += "0";

        secondStr += seconds;

        if (millisec < 10)
            millisecStr += "00";
        else if (millisec < 100)
            millisecStr += "0";

        millisecStr += millisec;

        textError.text = (int)error > 0 ? "+" : "-";
        textError.text += secondStr + "." + millisecStr + "";
    }

    private void UpdateTime(object obj, int playerIdx)
    {
        TextMeshProUGUI t = text_times[playerIdx];
        long time = (long)obj;

        if (t.gameObject.activeSelf == false)
            t.gameObject.SetActive(true);

        int seconds = (int)(time / 1000);
        int millisec = (int)(time - seconds * 1000);

        string secondStr = "";
        string millisecStr = "";

        if (seconds > 0 && seconds < 10)
            secondStr += "0";

        secondStr += seconds;

        if (millisec < 10)
            millisecStr += "00";
        else if (millisec < 100)
            millisecStr += "0";

        millisecStr += millisec;

        t.text = secondStr + "." + millisecStr;

        //FIXME:
        //I want to start coroutine in a spesific time interval
        //we can find a better way
        if (allTimersStarted < gameController.playerCount)
            StartCoroutine(FadeTextToZeroAlpha(.62f, t));

        allTimersStarted++;
    }

    //https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    //I had the almost same idea but this looks way better than my idea
    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * t));
            yield return null;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

    }

    IEnumerator WaitForAnims(float time, Action func)
    {
        yield return new WaitForSeconds(time);

        if (func != null)
            func();

    }
}
