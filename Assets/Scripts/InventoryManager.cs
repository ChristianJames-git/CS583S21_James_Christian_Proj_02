using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameManager gm;
    private PlayerStats ps;
    [SerializeField] private UI_Shop uiShop;
    [SerializeField] private GameSceneManager gsm;
    public List<Transform> itemButtonList = new List<Transform>();
    public Transform ItemTemplate;
    private bool clickedGlasses;
    private int potionBoost = 50;
    public float spikeDamageMult = 1;
    public float fireDamageMult = 1;
    public Item shades;
    public Item potion;
    public Item healthUp;
    public Item armorUp;
    public Item speedUp;
    public Item riddleAns;
    public Item spikeRing;
    public Item fireRing;
    public Item gem;
    public Item shield;

    // Start is called before the first frame update
    private void Start()
    {
        gm = GameManager.Instance;
        ps = gm.playerStats;
        InstantiateItems();
        ItemTemplate.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void InstantiateItems()
    {
        shades = new Item { itemName = "Cool Sunglasses", itemCost = 0, itemID = 0 };
        AddItem(shades);
        potion = new Item { itemName = "Potion", itemCost = 20, itemID = 1 };
        healthUp = new Item { itemName = "Health +10", itemCost = 25, itemID = 2 };
        armorUp = new Item { itemName = "Armor +2", itemCost = 25, itemID = 3 };
        speedUp = new Item { itemName = "Speed+", itemCost = 25, itemID = 4 };
        riddleAns = new Item { itemName = "Riddle Answer", itemCost = 30, itemID = 5 };
        spikeRing = new Item { itemName = "Spike Resistance Ring", itemCost = 50, itemID = 6 };
        fireRing = new Item { itemName = "Fire Resistance Ring", itemCost = 75, itemID = 7 };
        gem = new Item { itemName = "Gemstone", itemCost = 100, itemID = 8 };
        shield = new Item { itemName = "Secret Shield Potion", itemCost = 0, itemID = 9 };
        if (gm.secretClicked)
            AddItem(shield);
    }
    public void AddItem(Item item)
    {
        ps.itemList.Add(item);
        AddItemDisplay(item);
    }
    private void RemoveItem(int index)
    {
        ps.itemList.RemoveAt(index);
        Destroy(itemButtonList[index].gameObject);
        itemButtonList.RemoveAt(index);
        UpdateItemDisplay();
    }
    private void AddItemDisplay(Item item)
    {
        Transform newItem = Instantiate(ItemTemplate, transform);
        RectTransform newItemRectTransform = newItem.GetComponent<RectTransform>();
        int y = -30 * itemButtonList.Count;
        newItemRectTransform.anchoredPosition = new Vector2(0, y);
        Transform newItemButton = newItem.GetChild(0);
        newItemButton.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
        newItemButton.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();
        newItem.gameObject.SetActive(true);
        itemButtonList.Add(newItem);

        newItemButton.GetComponent<Button>().onClick.AddListener(onClick);

        void onClick()
        {
            if (uiShop.gameObject.activeSelf)
                SellItem(item.itemID, y);
            else
                OnItemClick(item.itemID, y);
        }
    }
    private void UpdateItemDisplay()
    {
        for (int i = 0; i < itemButtonList.Count; i++)
            itemButtonList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -30 * i);
    }
    private void OnItemClick(int itemID, int y)
    {
        int index = y / -30;
        switch (itemID)
        {
            case 0: //glasses
                if (!clickedGlasses)
                {
                    ps.playerMaxHP++;
                    clickedGlasses = true;
                }
                break;
            case 1: //potion
                if (ps.playerHP != ps.playerMaxHP)
                {
                    ps.playerHP += potionBoost;
                    if (ps.playerHP > ps.playerMaxHP)
                        ps.playerHP = ps.playerMaxHP;
                    RemoveItem(index);
                }
                break;
            case 5: //Riddle Answer
                if (gsm.InUnsolvedRiddle())
                {
                    gsm.RiddleAnswer.text = gsm.currentRiddleAnswers[0];
                    RemoveItem(index);
                }
                break;
            case 6: //Spike Res
                spikeDamageMult = 0.2f;
                break;
            case 7: //Fire Res
                fireDamageMult = 0.2f;
                break;
            case 9: //Shield Potion
                ps.playerArmor += 2;
                RemoveItem(index);
                break;
        }
    }
    private void SellItem(int itemID, int y)
    {
        int index = y / -30;
        switch (itemID)
        {
            case 1:
                ps.purse += potion.itemCost;
                RemoveItem(index);
                break;
            case 5:
                ps.purse += riddleAns.itemCost;
                RemoveItem(index);
                break;
            case 8:
                ps.purse += gem.itemCost;
                RemoveItem(index);
                break;
        }
    }
}
