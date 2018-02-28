using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//模拟背包数据库
public class ItemModel
{
    private static Dictionary<string, Item> knapsackItem = new Dictionary<string, Item>();

    //存
    public static void StoreItem(string name, Item item)
    {
        if (knapsackItem.ContainsKey(name))
        {
            DelItem(name);
        }
        knapsackItem.Add(name, item);
    }

    //取
    public static Item GetItem(string name)
    {
        if (knapsackItem.ContainsKey(name))
        {
            return knapsackItem[name];
        }
        return null;
    }

    //删
    public static void DelItem(string name)
    {
        if (knapsackItem.ContainsKey(name))
        {
            knapsackItem.Remove(name);
        }
    }
}
