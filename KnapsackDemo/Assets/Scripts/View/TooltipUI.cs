using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public Text tooltip;
    public Text content;

    //更改数据
    public void UpdateTooltip(string text)
    {
        tooltip.text = text;
        content.text = text;
    }

    //显示
    public void Show()
    {
        gameObject.SetActive(true);
    }

    //隐藏
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetTooltipPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
