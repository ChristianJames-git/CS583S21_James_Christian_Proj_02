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
    private List<Transform> buttonList = new List<Transform>();

    private void Start()
    {
        ps = GameManager.Instance.playerStats;
        CreateButton(invMan.potion, 0);
        CreateButton(invMan.healthUp, 1);
        CreateButton(invMan.armorUp, 2);
        CreateButton(invMan.speedUp, 3);
        CreateButton(invMan.riddleAns, 4);

        Hide();
    }

    public void CreateButton (Item item, int positionIndex)
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
            item.itemCost += 15;
            buttonList[item.itemID - 1].Find("itemPrice").GetComponent<TextMeshProUGUI>().SetText(item.itemCost.ToString());
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
                    Destroy(buttonList[5].gameObject);
                    buttonList.RemoveAt(5);
                    break;
            }
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
