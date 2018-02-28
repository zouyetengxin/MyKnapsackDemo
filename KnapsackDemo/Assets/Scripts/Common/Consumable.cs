using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Consumable : Item
{
    public int BackHP { get; private set; }
    public int BackMP { get; private set; }

    public Consumable(int id, string name, string description,
        int buyPrice, int sellPrice, string icon, string itemType, int backHP, int backMP) :
        base(id, name, description, buyPrice, sellPrice, icon, itemType)
    {
        this.BackHP = backHP;
        this.BackMP = backMP;
    }
}
