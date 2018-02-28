using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class KnapsackManager : MonoBehaviour
{
    public static KnapsackManager _instance;
    public static KnapsackManager Instance { get { return _instance; } }
    public Dictionary<int, Item> itemList;  //模拟系统数据库

    public KnapsackUI knapsack;
    public TooltipUI tooltipUI;
    public DragItemUI dragItem;

    private bool isShow = false;
    private bool isDrag = false;

    void Awake()
    {
        //单例
        _instance = this;

        //模拟加载数据库
        Load();

        //事件
        GridUI.OnEnter += GridUI_OnEnter;
        GridUI.OnExit += GridUI_OnExit;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
    }

    void Update()
    {
        Vector2 position;
        //屏幕坐标转化为Canvas相对坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find
            ("Canvas").transform as RectTransform, Input.mousePosition, null, out position);
        if (isShow)
        {
            tooltipUI.Show();
            tooltipUI.SetTooltipPosition(position);
        }
        if (isDrag)
        {
            dragItem.Show();
            dragItem.SetDragItemPosition(position);
        }
    }

    public void StoreItem(int itemID)
    {
        //对比数据库中是否拥有该种数据
        if (!itemList.ContainsKey(itemID))  //没有，跳过
        {
            return;
        }
        Item item = itemList[itemID];       //有，提取

        //求取一个空格子
        Transform emptyGrid = knapsack.GetEmptyGrid();
        if (emptyGrid == null)              //没有，提示并跳过
        {
            Debug.LogWarning("背包已满！！！");
            return;
        }
        //有，加载一个Item
        CreateItem(item, emptyGrid);
    }

    private void Load()
    {
        itemList = new Dictionary<int, Item>();
        Consumable c1 = new Consumable(0, "红瓶", "普通的血瓶，价格便宜", 50, 25, "Items/100001", "Consumable", 50, 0);
        Consumable c2 = new Consumable(1, "蓝瓶", "普通的蓝瓶，价格便宜", 50, 25, "Items/100002", "Consumable", 0, 50);

        Weapon w1 = new Weapon(2, "圣剑", "拥有神圣力量的武器", 10000, 100, "Items/300001", "Weapon", 1000, 100, 100);
        Weapon w2 = new Weapon(3, "匕首", "普通的水果刀？", 100, 50, "Items/300002", "Weapon", 10, 1, 1);
        Weapon w3 = new Weapon(4, "巨剑", "重剑无锋，大巧不工！仔细想想，只凭借肉体力量，能够挥动的都是……", 1000, 500, "Items/300003", "Weapon", 100, 10, 0);

        Armor a1 = new Armor(5, "全身铠", "披上它还能动的人没有几个了", 2000, 200, "Items/310001", "Armor", 200, 40, 0);
        Armor a2 = new Armor(6, "戒指", "附魔戒指", 4000, 2000, "Items/320001", "Armor", 100, 20, 20);
        Armor a3 = new Armor(7, "项链", "附魔项链", 4000, 2000, "Items/330001", "Armor", 100, 20, 20);

        itemList.Add(c1.ID, c1);
        itemList.Add(c2.ID, c2);
        itemList.Add(w1.ID, w1);
        itemList.Add(w2.ID, w2);
        itemList.Add(w3.ID, w3);
        itemList.Add(a1.ID, a1);
        itemList.Add(a2.ID, a2);
        itemList.Add(a3.ID, a3);
    }

    #region 事件回调
    private void GridUI_OnEnter(Transform transform)
    {
        Item item = ItemModel.GetItem(transform.name);
        if (item == null)
        {
            return;
        }
        string content = GetTooltipContent(item);
        tooltipUI.UpdateTooltip(content);
        isShow = true;
    }

    private void GridUI_OnExit()
    {
        isShow = false;
        tooltipUI.Hide();
    }

    private void GridUI_OnLeftBeginDrag(Transform transform)
    {
        if (transform.childCount == 0)
        {
            return;
        }
        Item item = ItemModel.GetItem(transform.name);
        dragItem.UpdateItem(item.Icon);
        Destroy(transform.GetChild(0).gameObject);  //销毁子物体
        isDrag = true;
    }

    private void GridUI_OnLeftEndDrag(Transform preTransform, Transform nowTransform)
    {
        isDrag = false;
        dragItem.Hide();

        if (nowTransform == null)              //拖到背包外面，扔掉
        {
            ItemModel.DelItem(preTransform.name);//删掉背包数据库中原来格子里的数据
        }
        else if (nowTransform.tag == "Grid")   //拖到了另一个格子
        {
            if (nowTransform.childCount == 0)  //是空格子，放入
            {
                Item preItem = ItemModel.GetItem(preTransform.name);
                CreateItem(preItem, nowTransform);
                ItemModel.DelItem(preTransform.name);
            }
            else                               //不是空格子，交换
            {
                Destroy(nowTransform.GetChild(0).gameObject);
                Item preItem = ItemModel.GetItem(preTransform.name);
                Item nowItem = ItemModel.GetItem(nowTransform.name);

                CreateItem(preItem, nowTransform);
                CreateItem(nowItem, preTransform);
            }
        }
        else                                //不在背包的格子里面，放回去
        {
            Item item = ItemModel.GetItem(preTransform.name);
            CreateItem(item, preTransform);
        }
    }
    #endregion

    private string GetTooltipContent(Item item)
    {
        StringBuilder sb = new StringBuilder();
        switch (item.ItemType)
        {
            case "Consumable":
                Consumable consumable = item as Consumable;
                sb.AppendFormat("<color=green><size=25>{0}</size></color>\n\n", consumable.Name);
                sb.AppendFormat("<color=white><size=20>回复HP:{0}\n回复MP:{1}</size></color>\n\n", consumable.BackHP, consumable.BackMP);
                break;
            case "Weapon":
                Weapon weapon = item as Weapon;
                sb.AppendFormat("<color=red><size=25>{0}</size></color>\n\n", weapon.Name);
                sb.AppendFormat("<color=white><size=20>攻击:{0}\n力量:{1}\n敏捷:{2}</size></color>\n\n", weapon.ATK, weapon.STR, weapon.AGI);
                break;
            case "Armor":
                Armor armor = item as Armor;
                sb.AppendFormat("<color=yellow><size=25>{0}</size></color>\n\n", armor.Name);
                sb.AppendFormat("<color=white><size=20>防御:{0}\n力量:{1}\n敏捷:{2}</size></color>\n\n", armor.DEF, armor.STR, armor.AGI);
                break;
            default:
                break;
        }
        sb.AppendFormat("<color=white><size=20>购买价格:{0}\n售卖价格:{1}</size></color>\n\n<color=blue><size=16>{2}</size></color>",
            item.BuyPrice, item.SellPrice, item.Description);

        return sb.ToString();
    }

    private void CreateItem(Item item, Transform emptyGrid)
    {
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
        itemPrefab.GetComponent<ItemUI>().UpdateItem(item.Icon);//设置Sprite图片
        GameObject itemGO = GameObject.Instantiate(itemPrefab);//实例化
        itemGO.transform.SetParent(emptyGrid);//设置父物体
        itemGO.transform.localPosition = Vector3.zero;//设置位置
        itemGO.transform.localScale = Vector3.one;//设置大小
        ItemModel.StoreItem(emptyGrid.name, item);//存储到背包数据库
    }
}
