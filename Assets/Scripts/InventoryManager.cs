using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private PlayerStats ps;
    [SerializeField] private GameSceneManager gsm;
    public List<Transform> itemButtonList = new List<Transform>();
    public Transform ItemTemplate;
    private bool clickedGlasses;
    private int potionBoost = 50;
    public float spikeDamageMult = 1;
    public float fireDamageMult = 1;

    // Start is called before the first frame update
    private void Start()
    {
        ps = GameManager.Instance.playerStats;
        StartItemList();
        ItemTemplate.gameObject.SetActive(false);
        Hide();
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
            case 100: //glasses
                if (!clickedGlasses)
                {
                    ps.playerMaxHP++;
                    clickedGlasses = true;
                }
                break;
            case 001: //potion
                if (ps.playerHP != ps.playerMaxHP)
                {
                    ps.playerHP += potionBoost;
                    if (ps.playerHP > ps.playerMaxHP)
                        ps.playerHP = ps.playerMaxHP;
                    RemoveItem(index);
                }
                break;
            case 005: //Riddle Answer
                if (gsm.InUnsolvedRiddle())
                {
                    gsm.RiddleAnswer.text = gsm.currentRiddleAnswers[0];
                    RemoveItem(index);
                }
                break;
            case 006: //Spike Res
                spikeDamageMult = 0.2f;
                break;
            case 007: //Fire Res
                fireDamageMult = 0.2f;
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
