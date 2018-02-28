using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class GridUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Enter & Exit
    public static Action<Transform> OnEnter;
    public static Action OnExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //监听进入的是不是tag为Grid的gameObject
        if (eventData.pointerEnter.tag == "Grid")
        {
            //检测OnEnter事件是否注册
            if (OnEnter != null)          //如果注册了
            {
                OnEnter(transform);       //调用OnEnter()
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (OnExit != null)
            {
                OnExit();
            }
        }
    }
    #endregion

    #region 拖拽
    public static Action<Transform> OnLeftBeginDrag;
    public static Action<Transform, Transform> OnLeftEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)//当鼠标左键按下
        {
            if (OnLeftBeginDrag != null)
            {
                OnLeftBeginDrag(transform);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (OnLeftEndDrag != null)
            {
                if (eventData.pointerEnter == null)
                {
                    OnLeftEndDrag(transform, null);
                }
                else
                {
                    OnLeftEndDrag(transform, eventData.pointerEnter.transform);
                }
            }
        }
    }
    #endregion
}
