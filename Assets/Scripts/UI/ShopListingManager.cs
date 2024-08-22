using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopListingManager : MonoBehaviour
{
    public GameObject shopListing;
    public Transform listingGrid;

    ItemData itemToBuy;
    int quantity;

    [Header("Confirmation Screen")]
    public GameObject confirmationScreen;
    public Text confirmationPrompt;
    public Text quantityText;
    public Text costCalculationText;
    public Button purchaseButton;

    public void RenderShop(List<ItemData> shopItems)
    {

        UIManager.Instance.SetInteractCanvasFalse();
        //reset listing if there was a previous one
        if (listingGrid.childCount > 0)
        {
            foreach (Transform child in listingGrid)
            {
                Destroy(child.gameObject);
            }
        }

        // Initial Y position for the first listing
        float initialYPosition = 34.1342f;

        //create new listing for evey item
        foreach (ItemData shopItem in shopItems)
        {
            //instantiate shop listing prefab for the item
            GameObject listingGameObject = Instantiate(shopListing, listingGrid);
            // Adjust Y position of the instantiated listing
            listingGameObject.transform.localPosition = new Vector3(1.9073e-06f, initialYPosition, 0f);
            // Increment Y position for the next listing
            initialYPosition -= 22.03f; // You can adjust this value based on your UI layout
            //assign to shop item and display them
            listingGameObject.GetComponent<ShopListing>().Display(shopItem);
        }
    }
    public void OpenConfirmationScreen(ItemData item)
    {
        itemToBuy = item;
        quantity = 1;
        RenderConfirmationScreen();

    }
    public void RenderConfirmationScreen()
    {
        confirmationScreen.SetActive(true);
        confirmationPrompt.text = $"Buy {itemToBuy.name} ?";
        quantityText.text = "x" + quantity;
        int cost = itemToBuy.cost * quantity;
        int playerMoneyLeft = PlayerStats.Money - cost;

        if (playerMoneyLeft < 0)
        {
            costCalculationText.text = "You are too broke";
            purchaseButton.interactable = false;
            return;
        }
        purchaseButton.interactable = true;
        costCalculationText.text = $"{PlayerStats.Money} > {playerMoneyLeft}";
    }
    public void AddQuantity()
    {
        quantity++;
        RenderConfirmationScreen();
    }
    public void SubtractQuantity()
    {
        if (quantity > 1)
        {
            quantity--;
        }
        RenderConfirmationScreen();
    }
    public void ConfirmPurchase()
    {
        Shop.Purchase(itemToBuy, quantity);
        confirmationScreen.SetActive(false);
    }
    public void CancelPurchase()
    {
        confirmationScreen.SetActive(false);
    }
}
