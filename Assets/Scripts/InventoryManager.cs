using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //private GameManager gm;
    private PlayerStats ps;
    public List<Transform> itemButtonList = new List<Transform>();
    public Transform ItemTemplate;
    public GameObject potionFloor1;
    private bool clickedGlasses;

    // Start is called before the first frame update
    private void Start()
    {
        ps = GameManager.Instance.playerStats;
        StartItemList();
        ItemTemplate.gameObject.SetActive(false);
        Hide();
    }
    private void Update()
    {
        
    }

    private void StartItemList()
    {
        Item shades = new Item { itemID = 100, itemCost = 0, itemName = "Cool Sunglasses" };
        AddItem(shades);
    }
    public void AddItem(Item item)
    {
        ps.itemList.Add(item);
        AddItemDisplay(item);
    }
    public void RemoveItem(int index)
    {
        ps.itemList.RemoveAt(index);
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
        itemButtonList.Add(newItem);

        newItemButton.GetComponent<Button>().onClick.AddListener(onClick);

        void onClick()
        {
            OnItemClick(item.itemID, y);
        }
    }
    private void UpdateItemDisplay()
    {
        for (int i = 0; i < itemButtonList.Count; i++)
        {
            itemButtonList[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(150, -30 * i);
        }
    }
    private void OnItemClick(int itemID, int y)
    {
        int index = y / -30;
        switch (itemID)
        {
            case 100: //glasses
                if (!clickedGlasses)
                {
                    ps.playerMaxHP++;
                    clickedGlasses = true;
                    UpdateItemDisplay();
                }
                break;
            case 001: //potion
                if (ps.playerHP != ps.playerMaxHP)
                {
                    ps.playerHP += 50;
                    if (ps.playerHP > ps.playerMaxHP)
                        ps.playerHP = ps.playerMaxHP;
                    RemoveItem(index);
                }
                break;
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
