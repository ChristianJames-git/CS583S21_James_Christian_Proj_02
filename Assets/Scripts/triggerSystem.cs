using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSystem : MonoBehaviour
{
    [SerializeField] private GameSceneManager gsm;
    [SerializeField] private UI_Shop uishop;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "doorTrigger":
                gsm.ToShop();
                break;
            case "returnTrigger":
                gsm.ReturnFromShop();
                break;
            case "shopArea":
                uishop.Show();
                break;
            case "healArea":
                GameManager.Instance.playerStats.playerHP = GameManager.Instance.playerStats.playerMaxHP;
                break;
            case "mineEntrance":
                gsm.ToMine();
                break;
            case "Chest1":
                gsm.Chest(0);
                break;
            case "Chest2":
                gsm.Chest(1);
                break;
            case "mineExit":
                gsm.FromMine();
                break;
            case "riddle1Chest":
                gsm.ShowRiddle(0);
                break;
            case "riddle2Doors":
                gsm.ShowRiddle(1);
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "shopArea":
                uishop.Hide();
                break;
            case "riddle1Chest":
            case "riddle2Doors":
                gsm.Riddle.SetActive(false);
                gsm.RiddleAnswer.text = "";
                break;
        }
    }
}
