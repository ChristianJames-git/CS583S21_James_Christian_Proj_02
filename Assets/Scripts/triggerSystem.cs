using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSystem : MonoBehaviour
{
    [SerializeField] private GameSceneManager gsm;
    [SerializeField] private UI_Shop uishop;
    [SerializeField] private InventoryManager invMan;
    private Item potion;
    private Item spikeRes;
    private Item fireRes;

    private void Start()
    {
        potion = new Item { itemCost = 25, itemID = 001, itemName = "Potion" };
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            //Movements
            case "doorTrigger":
                gsm.Teleport(25, 0);
                break;
            case "returnTrigger":
                gsm.Teleport(10.5f, 1);
                break;
            case "mineEntrance":
                gsm.Teleport(-52, 1);
                break;
            case "mineExit":
                gsm.Teleport(-7, 0);
                gsm.LockAll();
                break;
            case "toFloor1":
                gsm.Teleport(-25, 28);
                break;
            case "backToFloor1":
                gsm.Teleport(-43.5f, -5.5f);
                break;
            case "toFloor2":
                //Location TBD
                gsm.Teleport(0, 0);
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
                break;
            case "Chest3A":
                gsm.ChestPuzzle(0);
                break;
            case "Chest3B":
                gsm.ChestPuzzle(1);
                break;
            case "Chest3C":
                gsm.ChestPuzzle(2);
                break;
            case "Chest3D":
                gsm.ChestPuzzle(3);
                break;
            //Riddles
            case "riddle1Chest":
                gsm.ShowRiddle(0);
                break;
            case "riddle2Doors":
                gsm.ShowRiddle(1);
                break;
            case "riddle3ToFloor1":
                gsm.ShowRiddle(2);
                break;
            case "riddle4AtFloor1":
                gsm.ShowRiddle(3);
                break;
            case "riddle5ToChestRoom":
                gsm.ShowRiddle(4);
                break;
            case "riddle6ToChestRoom":
                gsm.ShowRiddle(5);
                break;
            case "riddle7LeftRoom":
                gsm.ShowRiddle(6);
                break;
            case "riddle8ToFloor2":
                gsm.ShowRiddle(7);
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
            //Traps
            case "Spike":
                gsm.TrapDamage(0);
                break;
            case "Fire":
                gsm.TrapDamage(1);
                break;
            //Items
            case "Potion":
                collision.gameObject.SetActive(false);
                invMan.AddItem(potion);
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
            case "riddle3ToFloor1":
            case "riddle4AtFloor1":
            case "riddle5ToChestRoom":
            case "riddle6ToChestRoom":
            case "riddle7LeftRoom":
            case "riddle8ToFloor2":
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
