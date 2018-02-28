using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Weapon : Item
{
    public int ATK { get; private set; }
    public int STR { get; private set; }
    public int AGI { get; private set; }

    public Weapon(int id, string name, string description, int buyPrice, 
        int sellPrice, string icon, string itemType, int ATK, int STR, int AGI) : 
        base(id, name, description, buyPrice, sellPrice, icon, itemType)
    {
        this.ATK = ATK;
        this.STR = STR;
        this.AGI = AGI;
    }
}
