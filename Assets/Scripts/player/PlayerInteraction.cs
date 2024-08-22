using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{
    playerController playerControll;
    Land selectedLand = null;

    InteractableObject selectedInteractable = null;


    void Start()
    {
        playerControll = transform.parent.GetComponent<playerController>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            OninteractablHit(hit);
        }
    }

    void OninteractablHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        UIManager.Instance.RenderInteracCanvas(other.tag);
        //land select
        if (other.tag == "soil")
        {
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }
        //land deselect
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }
        //select Item
        if (other.tag == "Item")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            Debug.Log("Selecting Item");
            return;
        }
        //select tool
        if (other.tag == "Tool")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            Debug.Log("Selecting Tool");
            return;
        }
        //select tool
        if (other.tag == "SellBox")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            Debug.Log("Selecting Sell Box");
            return;
        }

        if (other.tag == "Shop")
        {
            selectedInteractable = other.GetComponent<InteractableObject>();
            Debug.Log("Selecting Shop");
            return;
        }

        if (selectedInteractable != null)
        {
            Debug.Log("Not Selecting");
            UIManager.Instance.SetInteractCanvasFalse();
            selectedInteractable = null;
            return;
        }
        else
        {
            UIManager.Instance.SetInteractCanvasFalse();
        }

    }

    void SelectLand(Land land)
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }
        selectedLand = land;
        land.Select(true);
    }

    public void Interact()
    {

        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }
    }

    public void ItemInteract()
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Pickup();
        }
    }

    public void ItemKeep()
    {
        //if player holding something, keep it in inventory
        if (InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Item))
        {
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
            return;
        }
        if (InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Tool);
            return;
        }
    }
}
