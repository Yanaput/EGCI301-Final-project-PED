using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public ItemData item;

    public virtual void Pickup()
    {
        //Set the player's inventory to the item
        if (InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool) == null && InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Item) == null)
        {
            InventoryManager.Instance.EquipHandSlot(item);
            //Update the changes in the scene
            InventoryManager.Instance.RenderHand();
            UIManager.Instance.RenderInventory();
            //Destroy this instance so as to not have multiple copies
            Destroy(gameObject);
        }
    }
}
