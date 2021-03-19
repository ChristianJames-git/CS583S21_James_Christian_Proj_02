using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

    public void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.hello";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, playerStats);
        stream.Close();
    }

    public PlayerStats LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.hello";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerStats stats = formatter.Deserialize(stream) as PlayerStats;
            stream.Close();

            return stats;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
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