using UnityEngine;
using UnityEngine.UI.Extensions;

public class ScrollView : MonoBehaviour
{
    VerticalScrollSnap scrollSnap;
    RectTransform scrollViewPanel;
    bool showListPanel = false;
	
    // Use this for initialization
	void Start ()
    {
        scrollViewPanel = this.GetComponent<RectTransform>();
        scrollSnap = this.GetComponentInChildren<VerticalScrollSnap>();
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
        showListPanel = !showListPanel;
    }

    public void SelectPage(int index)
    {
        scrollSnap.ChangePage(index);
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
