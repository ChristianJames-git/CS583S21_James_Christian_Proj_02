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
    private int answerCost = 30;
    public int spikeResCost = 0;
    public int fireResCost = 0;
    private List<Transform> buttonList = new List<Transform>();

    private void Start()
    {
        ps = GameManager.Instance.playerStats;
        AddButton("Potion", potionCost, 001, 0);
        AddButton("Health +10", healthCost, 2, 1);
        AddButton("Armor +2", armorCost, 3, 2);
        AddButton("Speed+", speedCost, 4, 3);
        AddButton("Riddle Answer", answerCost, 5, 4);

        Hide();
    }

    public void AddButton(string iName, int iCost, int iID, int positionIndex)
    {
        Item newItem = new Item() { itemName = iName, itemCost = iCost, itemID = iID };
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

        buttonList.Add(shopItemTransform);
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
            ps.shopPurchases[item.itemID - 1]++;
            switch (item.itemID)
            {
                case 001:
                case 005:
                    invMan.AddItem(item);
                    break;
                case 002:
                    ps.playerMaxHP += 10;
                    break;
                case 003:
                    ps.playerArmor += 2;
                    break;
                case 004:
                    ps.speed += 1;
                    break;
                case 006:
                    invMan.AddItem(item);
                    Destroy(buttonList[item.itemID - 1].gameObject);
                    buttonList.RemoveAt(item.itemID - 1);
                    break;
            }
            item.itemCost += 15;
            buttonList[item.itemID - 1].Find("itemPrice").GetComponent<TextMeshProUGUI>().SetText(item.itemCost.ToString());
        }
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
