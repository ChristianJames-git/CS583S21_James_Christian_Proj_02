using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private UI_Shop uiShop;
    public GameObject player;
    public GameObject Riddle;
    public TMP_Text RiddleText;
    public TMP_InputField RiddleAnswer;
    public List<Riddle> riddleList = new List<Riddle>();
    private string[] currentRiddleAnswers;
    private int currentRiddle;
    public Vector3 pos;
    public Animator playerAnim;
    public List<GameObject> doorList;
    public GameObject door1, door2, door3, door4;
    bool[] doorUnlocked;
    public Sprite unlockedDoor;
    public Sprite lockedDoor;
    public TMP_Text healthDisplay;
    public TMP_Text purseDisplay;
    private GameManager gm;
    private PlayerStats ps;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    int facingDir;
    bool attacking = false;


    private void Start()
    {
        gm = GameManager.Instance;
        ps = gm.playerStats;

        doorList.Add(door1); doorList.Add(door2); doorList.Add(door3); doorList.Add(door4);
        doorUnlocked = new bool[doorList.Count];

        InstantiateRiddles();
        Riddle.SetActive(false);

        pos.x = 0; pos.y = 0;
        transform.position = pos;
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
            Move(horizontalMove, verticalMove);

        if (Riddle.activeSelf)
            for (int i = 0; i < currentRiddleAnswers.Length; i++)
                if (RiddleAnswer.text.ToLower() == currentRiddleAnswers[i])
                    RiddleComplete(currentRiddle);

        healthDisplay.text = ps.playerHP + " / " + ps.playerMaxHP;
        purseDisplay.text = "" + ps.purse;
    }

    public void Move(float horizontal, float vertical)
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

    public void ReturnFromShop()
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

    public void LockAll()
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorUnlocked[i] = false;
            doorList[i].GetComponent<BoxCollider2D>().enabled = true;
            doorList[i].GetComponent<SpriteRenderer>().sprite = lockedDoor;
        }
        for (int j = 0; j < riddleList.Count; j++)
            riddleList[j].riddleComplete = false;
    }

    public void Unlock(int doorNum)
    {
        doorUnlocked[doorNum] = true;
        doorList[doorNum].GetComponent<BoxCollider2D>().enabled = false;
        doorList[doorNum].GetComponent<SpriteRenderer>().sprite = unlockedDoor;
    }

    public void Chest(int chestNum)
    {
        if (!ps.chestCollected[chestNum])
        {
            ps.purse += 50;
            ps.chestCollected[chestNum] = true;
        }
        Debug.Log(ps.purse);
    }

    public void ShowRiddle(int category)
    {
        RiddleAnswer.interactable = true;
        if (riddleList[category].riddleComplete)
        {
            RiddleText.text = "Riddle Completed";
            RiddleAnswer.interactable = false;
        }
        else
        {
            RiddleText.text = riddleList[category].riddleText;
            riddleList[category].riddleViewed = true;
            currentRiddleAnswers = riddleList[category].riddleAnswer;
            currentRiddle = category;
        }
        Riddle.SetActive(true);
    }

    public void RiddleComplete(int riddle)
    {
        riddleList[currentRiddle].riddleComplete = true;
        RiddleText.text = "Correct! :)";
        switch (riddle)
        {
            case 0:
                Unlock(0);
                Unlock(1);
                break;
            case 1:
                Unlock(2);
                Unlock(3);
                break;
        }
    }

    public void InstantiateRiddles()
    {
        //Tutorial Floor Riddles
        riddleList.Add(new Riddle() { riddleText = "What object resides behind the doors to the northwest?", riddleAnswer = new string[] { "chest", "crate" } });
        riddleList.Add(new Riddle() { riddleText = "How many rocks are in the shop area?", riddleAnswer = new string[] { "fourteen", "14" } });
        //Floor 1 Riddles
        riddleList.Add(new Riddle() { riddleText = "I follow you all the time and copy your every move, but you can’t touch me or catch me. What am I?", riddleAnswer = new string[] { "shadow" } });
        riddleList.Add(new Riddle() { riddleText = "What has many keys but can’t open a single lock?", riddleAnswer = new string[] { "piano" } });
        riddleList.Add(new Riddle() { riddleText = "Where does today come before yesterday?", riddleAnswer = new string[] { "dictionary" } });
        riddleList.Add(new Riddle() { riddleText = "What invention lets you look right through a wall?", riddleAnswer = new string[] { "window" } });
        riddleList.Add(new Riddle() { riddleText = "What begins with an 'e', ends with an 'e', and only contains one letter?", riddleAnswer = new string[] { "envelope" } });
        riddleList.Add(new Riddle() { riddleText = "If you’re running in a race and you pass the person in second place, what place are you in?", riddleAnswer = new string[] { "second", "2nd" } });
        riddleList.Add(new Riddle() { riddleText = "What has to be broken before you can use it?", riddleAnswer = new string[] { "egg" } });
        //Floor 2 Riddles
        riddleList.Add(new Riddle() { riddleText = "What has a head and a tail but no body?", riddleAnswer = new string[] { "coin", "penny", "nickle", "dime", "quarter" } });
        riddleList.Add(new Riddle() { riddleText = "It stalks the countryside with ears that can’t hear. What is it?", riddleAnswer = new string[] { "corn" } });
        riddleList.Add(new Riddle() { riddleText = "What kind of coat is best put on wet?", riddleAnswer = new string[] { "paint" } });
        riddleList.Add(new Riddle() { riddleText = "I am an odd number. Take away a letter and I become even. What number am I?", riddleAnswer = new string[] { "seven", "7" } });
        riddleList.Add(new Riddle() { riddleText = "Mary has four daughters, and each of her daughters has a brother. How many children does Mary have?", riddleAnswer = new string[] { "five", "5" } });
        riddleList.Add(new Riddle() { riddleText = "What five-letter word becomes shorter when you add two letters to it?", riddleAnswer = new string[] { "short" } });
        //Item Shop Riddles
        riddleList.Add(new Riddle() { riddleText = "How many doors are on the first floor of the mine?", riddleAnswer = new string[] { "" } });
        riddleList.Add(new Riddle() { riddleText = "How many doors are on the second floor of the mine?", riddleAnswer = new string[] { "" } });
        riddleList.Add(new Riddle() { riddleText = "What accessory adorns your face?", riddleAnswer = new string[] { "sunglasses", "shades" } });
    }
    public void SavePlayer()
    {
        ps.position[0] = pos.x;
        ps.position[1] = pos.y;
        gm.SavePlayer();
    }

    public void LoadPlayer()
    {
        ps = gm.LoadPlayer();
        pos.x = ps.position[0];
        pos.y = ps.position[1];
    }
}

public class Riddle
{
    public string riddleText { get; set; }
    public string[] riddleAnswer { get; set; }
    public bool riddleComplete = false;
    public bool riddleViewed = false;
}
