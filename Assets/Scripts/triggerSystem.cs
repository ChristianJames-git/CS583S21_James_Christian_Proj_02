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
            //Movements
            case "doorTrigger":
                gsm.ToShop();
                break;
            case "returnTrigger":
                gsm.ReturnFromShop();
                break;
            case "mineEntrance":
                gsm.ToMine();
                break;
            case "mineExit":
                gsm.FromMine();
                break;
            //Shop
            case "shopArea":
                uishop.Show();
                break;
            case "healArea":
                GameManager.Instance.playerStats.playerHP = GameManager.Instance.playerStats.playerMaxHP;
                break;
            //Chests
            case "Chest1":
                gsm.Chest(0);
                break;
            case "Chest2":
                gsm.Chest(1);
                break;
            //Riddles
            case "riddle1Chest":
                gsm.ShowRiddle(0);
                break;
            case "riddle2Doors":
                gsm.ShowRiddle(1);
                break;
            //Tips
            case "tip1Start":
                gsm.TipText.text = "After wondering through the forest behind you, you find yourself in this small 'town'. The forest seems to have sealed behind you, resistant to any attempt to leave. The only house has a small sign that simply reads 'Shop' and eerie sounds come from the grate to the west. It's up to you to decide where to go, but for now, back home isn't an option. Good luck";
                gsm.Tip.SetActive(true);
                break;
            case "tip2Shop":
                gsm.TipText.text = "";
                gsm.Tip.SetActive(true);
                break;
            case "tip3Mine":
                gsm.TipText.text = "";
                gsm.Tip.SetActive(true);
                break;
            case "tip4Trap":
                gsm.TipText.text = "";
                gsm.Tip.SetActive(true);
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
            case "tip1Start":
            case "tip2Shop":
            case "tip3Mine":
            case "tip4Trap":
                gsm.Tip.SetActive(false);
                break;
        }
    }
}
