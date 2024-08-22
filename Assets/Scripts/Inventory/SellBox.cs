using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBox : InteractableObject
{
    
    public static List<ItemSlotData> itemsToSell = new List<ItemSlotData>();
    public override void Pickup(){
        //get holding item data
        ItemData handSlotItem = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Item);
        ItemSlotData handSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item);
        //if not holding, nothing happens
        if(handSlotItem == null){
            Debug.Log("No items in hand to sell");
            return;
        }
        //open yes no prompt if the player want to sell the itemn they're holding
        Debug.Log("Trigger Sell Prompt");
        UIManager.Instance.TriggerYesNoPrompt($"Do you want to sell {handSlotItem.name} x {handSlot.quantity}?", SellItems);
    }
    public static void SellItems(){
        //sell items
        Debug.Log("Sell Item trigger");
        ItemSlotData handSlot = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item);
        //add item to sell list
        itemsToSell.Add(new ItemSlotData(handSlot));
        //quantity
        foreach(ItemSlotData item in itemsToSell){
            Debug.Log($"Sold {item.itemData.name} x {item.quantity} for {item.itemData.cost * item.quantity}");
            PlayerStats.Earn(item.itemData.cost * item.quantity);
        }
        //empty out slot
        itemsToSell.Clear();
        handSlot.Empty();
        UIManager.Instance.RenderInventory();
        //update change
        InventoryManager.Instance.RenderHand();
        
        
    } 
    
}
