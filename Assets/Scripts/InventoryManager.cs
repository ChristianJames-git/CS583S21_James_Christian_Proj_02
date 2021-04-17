using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameManager gm;
    public List<Transform> itemButtonList = new List<Transform>();
    public Transform ItemTemplate;
    public GameObject potionFloor1;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
        StartItemList();
        ItemTemplate.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    private void StartItemList()
    {
        Item shades = new Item { itemID = 10000, itemCost = 0, itemName = "Cool Sunglasses" };
        AddItem(shades);
    }
    public void AddItem(Item item)
    {
        gm.playerStats.itemList.Add(item);
        AddItemDisplay(item);
    }
    public void RemoveItem(int index)
    {
        gm.playerStats.itemList.RemoveAt(index);
        itemButtonList.RemoveAt(index);
        UpdateItemDisplay();
    }
    private void AddItemDisplay(Item item)
    {
        Transform newItem = Instantiate(ItemTemplate, transform);
        RectTransform newItemRectTransform = newItem.GetComponent<RectTransform>();
        newItemRectTransform.anchoredPosition = new Vector2(0, -30 * itemButtonList.Count);
        Transform newItemButton = newItem.GetChild(0);
        newItemButton.GetChild(0).GetComponent<TMP_Text>().text = item.itemName;
        newItemButton.GetChild(1).GetComponent<TMP_Text>().text = item.itemCost.ToString();
        itemButtonList.Add(newItem);

        newItemButton.GetComponent<Button>().onClick.AddListener(onClick);

        void onClick()
        {
            onItemClick(item.itemID);
        }
    }
    private void UpdateItemDisplay()
    {
        for (int i = 0; i < itemButtonList.Count; i++)
        {
            itemButtonList[i].transform.position = new Vector2(150, -15 - 30 * i);
        }
    }
    private void onItemClick(int itemID)
    {
        Debug.Log(itemID);
    }
}
