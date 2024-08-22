using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("Interactble canvas")]
    public GameObject interactbleCanvas;
    public Text interactableText;
    [Header("Equipping Tool")]
    public Image toolEquipSlot;
    public Text toolQuantityText;

    [Header("Inventory System")]
    public GameObject inventoryPanel;
    public HandInventorySlot toolHandSlot;
    public InventorySlot[] toolSlots;
    public HandInventorySlot itemHandSlot;
    public InventorySlot[] itemSlots;
    public GameObject endTextUI;

    [Header("Item Info Box")]
    public GameObject itemInfoBox;
    public Text itemName;
    public Text itemDescription;

    [Header("Yes No Prompt")]
    public YesNoPrompt yesNoPrompt;

    [Header("Player Stats")]
    public Text moneyText;

    [Header("Shop")]
    public ShopListingManager shopListingManager;




    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            //if there is more than one instance, destroy extra 
            Destroy(this);
        }
        else
        {
            //Set the static instace to this instance
            Instance = this;
        }
    }



    private void Start()
    {
        RenderInventory();
        AssignSlotIndex();
        RenderPlayerStats();
        DisplayItemInfo(null);
        interactbleCanvas.SetActive(false);
    }

    public void AssignSlotIndex()
    {
        for (int i = 0; i < toolSlots.Length; i++)
        {
            toolSlots[i].AssignIndex(i);
        }
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].AssignIndex(i);
        }
    }

    public void SetInteractCanvasFalse()
    {
        interactbleCanvas.SetActive(false);
    }

    public void RenderInteracCanvas(string other)
    {
        if ((other == "Item" || other == "Tool") && other != "soil")
        {
            interactbleCanvas.SetActive(true);
            if (other == "Item" || other == "Tool")
                interactableText.text = $"Right click to collect";
        }
        else
            SetInteractCanvasFalse();
    }

    public void RenderInventory()
    {
        //Get the respective slots to process
        ItemSlotData[] inventoryToolSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);
        ItemSlotData[] inventoryItemSlots = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Item);

        //Render the Tool section
        RenderInventoryPanel(inventoryToolSlots, toolSlots);

        //Render the Item section
        RenderInventoryPanel(inventoryItemSlots, itemSlots);

        //Render the equipped slots
        toolHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        itemHandSlot.Display(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item));

        //Get Tool Equip from InventoryManager
        ItemData equippedTool = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);
        //Get Item Equip from InventoryManager
        ItemData equippedItem = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Item);
        //Text should be empty by default
        toolQuantityText.text = "";
        //Check if there is an item to display
        if (equippedTool != null || equippedItem != null)
        {
            if (equippedTool != null)
            {
                toolEquipSlot.sprite = equippedTool.itemSprite;

                toolEquipSlot.gameObject.SetActive(true);

                //Get quantity 
                int quantity = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool).quantity;
                if (quantity > 1)
                {
                    toolQuantityText.text = quantity.ToString();
                }
                return;
            }
            else if (equippedItem != null)
            {
                toolEquipSlot.sprite = equippedItem.itemSprite;

                toolEquipSlot.gameObject.SetActive(true);

                //Get quantity 
                int quantity = InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Item).quantity;
                if (quantity > 1)
                {
                    toolQuantityText.text = quantity.ToString();
                }
                return;
            }
        }
        toolEquipSlot.gameObject.SetActive(false);
    }


    public void RenderInventoryPanel(ItemSlotData[] slots, InventorySlot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }

    public void toggleInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        RenderInventory();
    }


    public void DisplayItemInfo(ItemData data)
    {
        if (data == null)
        {
            itemName.text = "";
            itemDescription.text = "";
            itemInfoBox.SetActive(false);
            return;
        }
        itemInfoBox.SetActive(true);
        itemName.text = data.name;
        itemDescription.text = data.itemDescription;
    }

    public void TriggerYesNoPrompt(string message, System.Action onYesCallback)
    {
        yesNoPrompt.gameObject.SetActive(true);
        yesNoPrompt.CreatePrompt(message, onYesCallback);
    }

    public void RenderPlayerStats()
    {
        moneyText.text = PlayerStats.Money.ToString();
    }

    public void OpenShop(List<ItemData> shopItems)
    {
        shopListingManager.gameObject.SetActive(true);
        shopListingManager.RenderShop(shopItems);
    }
    public void TriggerEndText()
    {
        endTextUI.SetActive(true);
    }
}
