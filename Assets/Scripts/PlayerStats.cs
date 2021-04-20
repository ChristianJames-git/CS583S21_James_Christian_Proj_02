using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string playerName;
    public Sprite playerSprite;
    public float[] position = { 0, 0, 0 };
    public float playerHP = 100;
    public int playerMaxHP = 100;
    public float speed = 4;
    public int playerArmor = 0; //armor, resists damage
    public int purse = 0;
    public List<Item> itemList = new List<Item>();
    public int[] shopPurchases = new int[6];
    public bool[] doorUnlocked = new bool[16];
    public bool[] riddleComplete = new bool[13];
    public bool[] lootCollected = new bool[6];
    public bool spikeRingAdded;
}
