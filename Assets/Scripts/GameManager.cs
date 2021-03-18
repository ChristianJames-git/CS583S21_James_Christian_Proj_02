using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Sprite whiteMalePickSpear;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        playerStats = new PlayerStats();
        playerStats.playerSprite = whiteMalePickSpear;
    }

    public class PlayerStats
    {
        public string playerName;
        public Sprite playerSprite;
        public List<int> playerMobKillStats = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public List<int> playerMiningStats = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
        public float playerHP = 5;
        public int playerMaxHP = 5;
        public int miningLevel = 1;  //pick , impoves mining damage
        public int attackLevel = 1;  //sword, impoves attack damage
        public int defenseLevel = 0; //armor, resists damage
        public int purse = 0;
        public List<string> itemList = new List<string>();
    }
}

public class MobEntity
{
    public int mobNum;
    public string mobName;
    public float currHP;
    public int maxHP;
    public float speed = 1;
    public int coinWorth = 1;
    public float damage = 1;
    
    public MobEntity(int mobType, float multiplier)
    {
        mobNum = mobType;
        switch(mobType)
        {
            case 0:
                mobName = "zombie";
                currHP = maxHP = 4;
                speed = 0.75f;
                break;
            case 1:
                mobName = "bat";
                currHP = maxHP = 2;
                speed = 3;
                damage = 0.5f;
                break;
            case 2:
                mobName = "skeletonsoldier";
                currHP = maxHP = 4;
                speed = 1.5f;
                coinWorth = 2;
                damage = 1.5f;
                break;
            case 3:
                mobName = "skeletonarcher";
                currHP = maxHP = 2;
                coinWorth = 2;
                break;
            case 4:
                mobName = "greenskeleton";
                currHP = maxHP = 12;
                coinWorth = 10;
                break;
            case 5:
                mobName = "orc";
                currHP = maxHP = 6;
                coinWorth = 3;
                speed = 1.5f;
                damage = 1.5f;
                break;
            case 6:
                mobName = "reptilesoldier";
                currHP = maxHP = 6;
                coinWorth = 3;
                damage = 2;
                break;
            case 7:
                mobName = "flyingreptilesoldier";
                currHP = maxHP = 4;
                coinWorth = 4;
                speed = 2;
                damage = 2;
                break;
            case 8:
                mobName = "wolf";
                currHP = maxHP = 3;
                coinWorth = 3;
                speed = 3;
                break;
            case 9:
                mobName = "goldworlf";
                currHP = maxHP = 6;
                coinWorth = 8;
                speed = 3;
                damage = 2.5f;
                break;
            case 10:
                mobName = "minotaur";
                currHP = maxHP = 20;
                coinWorth = 20;
                speed = 0.5f;
                damage = 5;
                break;
            case 11:
                mobName = "wizard";
                currHP = maxHP = 2;
                coinWorth = 5;
                speed = 2;
                damage = 4;
                break;
            default:
                Debug.Log(mobType + "is not a valid Mob Type");
                break;
        }
        maxHP = (int)(maxHP * 2 * multiplier);
        currHP = maxHP;
        coinWorth = (int)(coinWorth * 0.5 * multiplier);
        damage = damage * multiplier;
    }
}

public class rockEntity
{
    public int rockNum;
    public float currHP;
    public int maxHP;

    public rockEntity(int rockType) //stone, copper, iron, gold, diamond, iridium, gem
    {
        rockNum = rockType;
        currHP = maxHP = rockType + 1;
    }
}