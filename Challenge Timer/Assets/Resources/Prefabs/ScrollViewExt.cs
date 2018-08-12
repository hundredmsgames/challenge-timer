using UnityEngine;
using UnityEngine.UI;
public static class ScrollRectExtensions
{
    public static void ScrollToTop(this ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, .5f);
    }
    public static void ScrollToBottom(this ScrollRect scrollRect,float value)
    {
        scrollRect.normalizedPosition = new Vector2(0, value);
    }

}

