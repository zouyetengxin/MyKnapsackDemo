﻿using UnityEngine;
using System.Collections;

public class DragItemUI : ItemUI
{
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

    //设置坐标
    public void SetDragItemPosition(Vector2 position)
    {
        transform.localPosition = position;
    }
}
