using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public Item item;
    public int level;
    public int count;
    public void Pickup()
    {
        //Inventory.Instance.AddItem(this);
        ItemToSave itemRef = new ItemToSave();
        itemRef.item = item;
        itemRef.count = count;
        itemRef.level = level;
        Backpack.AddItemToBackpack(itemRef);
    }
}
