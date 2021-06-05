using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventory
//public static class CraftInventory :
{
    public Dictionary<ItemType, int> inventory;

    public CraftInventory()
    {
        inventory = new Dictionary<ItemType, int>();
        inventory.Add(ItemType.Turret,0);
        inventory.Add(ItemType.Mine,10);
    }
    
    public void AddItem(ItemType item)
    {
        inventory[item] += 1;
    }

    public void UseItem(ItemType item)
    {
        inventory[item] -= 1;
    }

    public bool HaveItem(ItemType item)
    {
        return inventory[item] > 0 ? true : false;
    }

    public bool HaveItemInInvetory()
    {
        if (inventory[ItemType.Turret] > 0 || inventory[ItemType.Mine] > 0)
            return true;
        return false;
    }

    public enum ItemType
    {
        Turret,
        Mine,
    }
}
