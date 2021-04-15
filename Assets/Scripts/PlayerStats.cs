using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public Sprite playerSprite;
    public float[] position = { 0, 0, 0 };
    public float playerHP = 5;
    public int playerMaxHP = 5;
    public float speed = 3;
    public int defenseLevel = 0; //armor, resists damage
    public int purse = 0;
    public List<string> itemList = new List<string>();
    public bool[] chestCollected = new bool[2];
}
