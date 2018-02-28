using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Armor : Item
{
    public int DEF { get; private set; }
    public int STR { get; private set; }
    public int AGI { get; private set; }

    public Armor(int id, string name, string description, int buyPrice, 
        int sellPrice, string icon, string itemType, int DEF, int STR, int AGI) : 
        base(id, name, description, buyPrice, sellPrice, icon, itemType)
    {
        this.DEF = DEF;
        this.STR = STR;
        this.AGI = AGI;
    }
}
