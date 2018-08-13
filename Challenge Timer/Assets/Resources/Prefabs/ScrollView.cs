using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ScrollView : MonoBehaviour
{
    VerticalScrollSnap verticalScrollSnap;
    RectTransform scrollViewPanel;
    bool showListPanel = false;
	
    // Use this for initialization
	void Start () {
        scrollViewPanel = this.GetComponent<RectTransform>();
        verticalScrollSnap = this.GetComponent<VerticalScrollSnap>();
	}
	
    // Update is called once per frame
	void Update ()
    {
        if (showListPanel)
        {
            ShowListPanel();
        }
        else
        {
            HideListPanel();
        }
    }

    public void ShowListPanelbool()
    {
        if (showListPanel == true)
        {
            verticalScrollSnap.enabled = true;
            
        }
        else
            verticalScrollSnap.enabled = false;

        showListPanel = !showListPanel;
    }

    public void ShowListPanel()
    {
        scrollViewPanel.sizeDelta = new Vector2(
            scrollViewPanel.sizeDelta.x,
            Mathf.Lerp(scrollViewPanel.sizeDelta.y, 500, Time.deltaTime * 5f)
        );
    }

    public void HideListPanel()
    {
        scrollViewPanel.sizeDelta = new Vector2(
            scrollViewPanel.sizeDelta.x,
            Mathf.Lerp(scrollViewPanel.sizeDelta.y, 90, Time.deltaTime * 5f)
        );
    }
}
