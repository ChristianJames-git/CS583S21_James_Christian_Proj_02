using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private PlayerStats ps;
    [SerializeField] private InventoryManager invMan;
    public Transform container;
    public Transform shopItemTemplate;
    private int healthCost = 25;
    private int armorCost = 25;
    private int potionCost = 20;
    private int speedCost = 25;
    public int spikeResCost = 50;
    public int fireResCost = 50;
    private Transform[] tempButtons = new Transform[2];

    private void Start()
    {
        ps = GameManager.Instance.playerStats;
        AddButton("Health +10", healthCost, 4, 0);
        AddButton("Armor +2", armorCost, 5, 1);
        AddButton("Potion", potionCost, 001, 2);
        AddButton("Speed", speedCost, 6, 3);

        Hide();
    }

    public void AddButton(string iName, int iCost, int iID, int positionIndex)
    {
        Item newItem = new Item() { itemCost = iCost, itemName = iName, itemID = iID };
        CreateButton(newItem, positionIndex);
    }

    private void CreateButton (Item item, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTemplate.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(item.itemName);
        shopItemTransform.Find("itemPrice").GetComponent<TextMeshProUGUI>().SetText(item.itemCost.ToString());
        shopItemTransform.GetComponent<Button>().onClick.AddListener(onClick);

        if (item.itemID == 002 || item.itemID == 003)
            tempButtons[item.itemID - 2] = shopItemTransform;
        void onClick()
        {
            BuyItem(item);
        }
    }

    private void BuyItem(Item item)
    {
        if (item.itemCost <= ps.purse)
        {
            ps.purse -= item.itemCost;
            invMan.AddItem(item);
            ps.shopPurchases[item.itemID - 1]++;
            item.itemCost = (int)(item.itemCost * 1.5f);
        }
        if (item.itemID == 002 || item.itemID == 003)
            Destroy(tempButtons[item.itemID - 2]);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
