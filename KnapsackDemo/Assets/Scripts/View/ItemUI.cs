using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public void UpdateItem(string s)
    {
        Sprite sp = Resources.Load<Sprite>(s);  //加载Sprite
        this.GetComponent<Image>().sprite = sp; //设置Sprite
    }
}
