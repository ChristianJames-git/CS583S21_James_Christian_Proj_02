using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                gsm.returnFromShop();
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
            case "mineExit":
                gsm.FromMine();
                gsm.LockAll();
                break;
            case "Chest1Riddle":
                gsm.ShowRiddle(0);
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
            case "Chest1Riddle":
                gsm.Riddle.SetActive(false);
                break;
        }
    }
}
