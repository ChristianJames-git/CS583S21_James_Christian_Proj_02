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
                SceneManager.LoadScene("MineScene");
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
        }
    }
}
