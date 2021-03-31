using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform shopItemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
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
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 45f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Show();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Hide();
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
