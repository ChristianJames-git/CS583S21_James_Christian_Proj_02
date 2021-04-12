using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private UI_Shop uiShop;
    public GameObject player;
    public Vector3 pos;
    public Animator playerAnim;
    public Sprite unlockedDoor;
    public TMP_Text healthDisplay;
    public TMP_Text purseDisplay;
    private GameManager gm;
    private PlayerStats ps;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    int facingDir;
    bool attacking;
    public GameObject door1, door2, door3, door4;

    public List<GameObject> doorList;
    bool[] chestCollected = new bool[1];
    bool[] doorUnlocked;
    public List<Riddle> easyRiddles;
    public List<Riddle> mediumRiddles;
    public List<Riddle> hardRiddles;

    private void Start()
    {
        gm = GameManager.Instance;
        ps = gm.playerStats;
        attacking = false;
        doorList.Add(door1); doorList.Add(door2); doorList.Add(door3); doorList.Add(door4);
        Debug.Log(doorList.Count);
        doorUnlocked = new bool[doorList.Count];
        Debug.Log(doorUnlocked[0]);
        pos.x = 0; pos.y = 0;
    }
    private void Update()
    {
        pos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("Attack?", true);
            attacking = true;
            playerAnim.SetInteger("AttackDir", facingDir);
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            playerAnim.SetBool("Attack?", false);
            attacking = false;
        }
        if (!attacking)
            move(horizontalMove, verticalMove);

        healthDisplay.text = ps.playerHP + " / " + ps.playerMaxHP;
        purseDisplay.text = "" + ps.purse;
    }

    public void move(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            pos.x += ps.speed * Time.deltaTime;
            facingDir = 1;
        }
        if (horizontal < 0)
        {
            pos.x -= ps.speed * Time.deltaTime;
            facingDir = 3;
        }
        if (vertical > 0)
        {
            pos.y += ps.speed * Time.deltaTime;
            facingDir = 0;
        }
        if (vertical < 0)
        {
            pos.y -= ps.speed * Time.deltaTime;
            facingDir = 2;
        }
        transform.position = pos;
        playerAnim.SetInteger("Horizontal", (int)horizontal);
        playerAnim.SetInteger("Vertical", (int)vertical);
    }

    public void ToShop()
    {
        pos.x = 25;
        pos.y = 0;
        transform.position = pos;
    }

    public void returnFromShop()
    {
        pos.x = 10.5f;
        pos.y = 1;
        transform.position = pos;
    }

    public void ToMine()
    {
        pos.x = -52;
        pos.y = 1;
        transform.position = pos;
    }

    public void FromMine()
    {
        pos.x = -7;
        pos.y = 0;
        transform.position = pos;
    }

    public void Unlock(int doorNum)
    {
        doorUnlocked[doorNum] = true;
        doorList[doorNum].GetComponent<BoxCollider2D>().enabled = false;
        doorList[doorNum].GetComponent<SpriteRenderer>().sprite = unlockedDoor;
    }

    public void Chest(int chestNum)
    {
        if (!chestCollected[chestNum])
        {
            ps.purse += 50;
            chestCollected[chestNum] = true;
        }
        Debug.Log(ps.purse);
    }

    public void InstantiateRiddles()
    {
        easyRiddles.Add(new Riddle("I follow you all the time and copy your every move, but you can’t touch me or catch me. What am I?", new string[] { "shadow" }, false));
        easyRiddles.Add(new Riddle("What has many keys but can’t open a single lock?", new string[] { "piano" }, false));
        easyRiddles.Add(new Riddle("Where does today come before yesterday?", new string[] { "dictionary" }, false));
        easyRiddles.Add(new Riddle("What invention lets you look right through a wall?", new string[] { "window" }, false));
        easyRiddles.Add(new Riddle("What begins with an 'e', ends with an 'e', and only contains one letter?", new string[] { "envelope" }, false));
        easyRiddles.Add(new Riddle("If you’re running in a race and you pass the person in second place, what place are you in?", new string[] { "second", "second place" }, false));
        easyRiddles.Add(new Riddle("What has to be broken before you can use it?", new string[] { "egg" }, false));
        mediumRiddles.Add(new Riddle("What has a head and a tail but no body?", new string[] { "coin" , "penny" , "nickle" , "dime" , "quarter" }, false));
        mediumRiddles.Add(new Riddle("It stalks the countryside with ears that can’t hear. What is it?", new string[] { "corn" }, false));
        mediumRiddles.Add(new Riddle("What kind of coat is best put on wet?", new string[] { "paint" }, false));
        mediumRiddles.Add(new Riddle("I am an odd number. Take away a letter and I become even. What number am I?", new string[] { "seven" , "7" }, false));
        mediumRiddles.Add(new Riddle("Mary has four daughters, and each of her daughters has a brother. How many children does Mary have?", new string[] { "five" , "5" }, false));
        mediumRiddles.Add(new Riddle("What five-letter word becomes shorter when you add two letters to it?", new string[] { "short" }, false));

    }
    public void SavePlayer()
    {
        ps.position[0] = pos.x;
        ps.position[1] = pos.y;
        gm.SavePlayer();
    }

    public void LoadPlayer()
    {
        gm.LoadPlayer();
        pos.x = ps.position[0];
        pos.y = ps.position[1];
    }
}

public class Riddle
{
    string riddleText;
    string[] riddleAnswer;
    bool riddleComplete;

    public Riddle (string text, string[] answer, bool complete)
    {
        riddleText = text;
        riddleAnswer = answer;
        riddleComplete = complete;
    }
}
