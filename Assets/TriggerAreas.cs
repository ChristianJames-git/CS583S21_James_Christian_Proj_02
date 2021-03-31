using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreas : MonoBehaviour
{
    [SerializeField] private GameSceneManager gsm;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "HealTriggerArea")
        //    GameManager.Instance.playerStats.playerHP = GameManager.Instance.playerStats.playerMaxHP;
        //if (collision.tag == "ShopDoorTriggerArea")
        //{
        //    Debug.Log("Fail");
        //    gsm.ToShop();
        //}
    }
}
