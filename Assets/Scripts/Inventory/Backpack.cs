using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Backpack
{
    public static List<ItemToSave> items = new List<ItemToSave>();

    public static void AddItemToBackpack(ItemToSave itemToSave)
    {
        items.Add(itemToSave);
    }
}

