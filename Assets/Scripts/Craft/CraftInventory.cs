using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventory
//public static class CraftInventory :
{
    public Dictionary<ItemType, int> inventory;


    public CraftInventory(int turret, int mine)
    {
        inventory = new Dictionary<ItemType, int>();
        inventory.Add(ItemType.Turret, turret);
        inventory.Add(ItemType.Mine, mine);

        PlayerHUDUI.TurretChanged(turret);
        PlayerHUDUI.MineChanged(mine);
    }
    
    public void AddItem(ItemType item)
    {
        inventory[item] += 1;

        if(item == ItemType.Turret)
        {
            PlayerHUDUI.TurretChanged(inventory[item]);
        } else if (item == ItemType.Mine)
        {
            PlayerHUDUI.MineChanged(inventory[item]);
        }
    }

    public void UseItem(ItemType item)
    {
        inventory[item] -= 1;
        if (item == ItemType.Turret)
        {
            PlayerHUDUI.TurretChanged(inventory[item]);
        }
        else if (item == ItemType.Mine)
        {
            PlayerHUDUI.MineChanged(inventory[item]);
        }
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
