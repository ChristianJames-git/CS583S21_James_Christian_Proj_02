using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    public Transform container;
    public Transform shopItemTemplate;

    private void Awake()
    {
        //container = transform.Find("container");
        //shopItemTemplate = container.Find("shopItemTemplate");
    }

    private void Start()
    {
        CreateButtons("Armor", 100, 0);
        CreateButtons("Mining", 75, 1);
        CreateButtons("Damage", 150, 2);
        CreateButtons("Speed", 200, 3);
        CreateButtons("Potion", 25, 4);
        CreateButtons("Difficulty Upgrade", 10000, 5);

        Hide();
    }

    private void CreateButtons (/*Sprite itemSprite,*/ string itemName, int itemPrice, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTemplate.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("itemPrice").GetComponent<TextMeshProUGUI>().SetText(itemPrice.ToString());
        //shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;
        shopItemTransform.GetComponent<Button>().onClick.AddListener(test);
        void test()
        {
            Debug.Log("Hello " + itemName);
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
