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
                collision.GetComponent<Collider2D>().enabled = false;
                break;
            case "Chest2":
                gsm.Chest(1);
                collision.GetComponent<Collider2D>().enabled = false;
                gsm.playerAnim.SetBool("Death", true);
                gsm.DeathScreen.SetActive(true);
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
                gsm.TipText.text = "Welcome to my little 'town'! The shop is to your right. Hope you like it since you're stuck here as long as I please. Ignore those sounds coming from the grate to the left.";
                gsm.Tip.SetActive(true);
                break;
            case "tip2Shop":
                gsm.TipText.text = "Here's my humble shop. I don't know how you could get injured here, but if you do, that heart will heal you right up";
                gsm.Tip.SetActive(true);
                break;
            case "tip3Mine":
                gsm.TipText.text = "Well it seems you investigated that noise even though I told you not to...though why did I think you'd trust me. I did trap you here. Good think I prepared some riddles for you";
                gsm.Tip.SetActive(true);
                break;
            case "tip4Trap":
                gsm.TipText.text = "Traps do damage or otherwise negatively effect your wellbeing. I've left a few of them around for you. Enjoy :)";
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
