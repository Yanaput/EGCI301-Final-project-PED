using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : InteractableObject
{
    public List<ItemData> shopItems;

    public static void Purchase(ItemData item, int quantity){
        int totalCost = item.cost * quantity;
        if(PlayerStats.Money >= totalCost){
            //deduct player money
            PlayerStats.Spend(totalCost); 
            //create ItemSlotData for purchased item
            ItemSlotData purchasedItem = new ItemSlotData(item, quantity);
            //send to player inventory
            InventoryManager.Instance.ShopToInventory(purchasedItem);

        }
    }

    public override void Pickup(){
        Debug.Log("open shop ui");
        UIManager.Instance.OpenShop(shopItems);
    }
}
