using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    //References
    private GameManager gm;
    private PlayerStats ps;
    [SerializeField] private UI_Shop uiShop;
    //Player and Movement
    public GameObject player;
    public Animator playerAnim;
    public Vector3 pos;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    //Riddles
    public GameObject Riddle;
    public TMP_Text RiddleText;
    public TMP_InputField RiddleAnswer;
    public List<Riddle> riddleList = new List<Riddle>();
    private string[] currentRiddleAnswers;
    private int currentRiddle;
    private int nextChestRiddle = 0;
    private bool chestRiddleComplete = false;
    public List<GameObject> chestRiddleTraps = new List<GameObject>();
    private int chestNumRand;
    //Tips
    public GameObject Tip;
    public TMP_Text TipText;
    //Doors
    public List<GameObject> doorList;
    public GameObject door1, door2, door3, door4, door5, door6, door7, door8, door9, door10, door11;
    public Sprite unlockedDoor;
    public Sprite lockedDoor;
    //Traps
    public GameObject spikeTrap;
    private int spikeDamage = 40;
    public GameObject fireTrap;
    private int fireDamage = 60;
    //HUD
    public TMP_Text healthDisplay;
    public TMP_Text purseDisplay;
    //Death
    public GameObject DeathScreen;
    private bool dead = false;
    public GameObject tombstone;
    public Sprite tombstone1, tombstone2, tombstone3;

    private void Start()
    {
        gm = GameManager.Instance;
        ps = gm.playerStats;

        doorList = new List<GameObject>{ door1, door2, door3, door4, door5, door6, door7, door8, door9, door10, door11};

        InstantiateRiddles();
        Riddle.SetActive(false);
        DeathScreen.SetActive(false);
        CreateTraps();
        chestNumRand = Random.Range(0, 4);
        Debug.Log(chestNumRand);

        pos.x = 0; pos.y = 0;
        transform.position = pos;
    }
    private void Update()
    {
        pos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (!dead)
            Move(horizontalMove, verticalMove);

        if (Riddle.activeSelf)
            for (int i = 0; i < currentRiddleAnswers.Length; i++)
                if (RiddleAnswer.text.ToLower() == currentRiddleAnswers[i])
                    RiddleComplete(currentRiddle);

        healthDisplay.text = ps.playerHP + " / " + ps.playerMaxHP;
        purseDisplay.text = "" + ps.purse;
    }

    //Movements
    private void Move(float horizontal, float vertical)
    {
        if (horizontal > 0)
            pos.x += ps.speed * Time.deltaTime;
        if (horizontal < 0)
            pos.x -= ps.speed * Time.deltaTime;
        if (vertical > 0)
            pos.y += ps.speed * Time.deltaTime;
        if (vertical < 0)
            pos.y -= ps.speed * Time.deltaTime;
        transform.position = pos;
        playerAnim.SetInteger("Horizontal", (int)horizontal);
        playerAnim.SetInteger("Vertical", (int)vertical);
    }
    public void Teleport(float x, float y)
    {
        pos.x = x;
        pos.y = y;
        transform.position = pos;
    }
    //Doors
    public void LockAll()
    {
        for (int i = 0; i < doorList.Count; i++)
        {
            doorList[i].GetComponent<BoxCollider2D>().enabled = true;
            doorList[i].GetComponent<SpriteRenderer>().sprite = lockedDoor;
        }
        for (int j = 0; j < riddleList.Count; j++)
            riddleList[j].riddleComplete = false;
        chestRiddleComplete = false;
        for (int i = 0; i < chestRiddleTraps.Count; i++)
            GameObject.Destroy(chestRiddleTraps[i]);
        chestRiddleTraps.Clear();
        chestNumRand = Random.Range(0, 4);
    }
    private void Unlock(int doorNum)
    {
        doorList[doorNum].GetComponent<BoxCollider2D>().enabled = false;
        doorList[doorNum].GetComponent<SpriteRenderer>().sprite = unlockedDoor;
    }
    //Chests
    public void Chest(int chestNum)
    {
        if (!ps.chestCollected[chestNum])
        {
            switch (chestNum)
            {
                case 0:
                    ps.purse += 50;
                    break;
                case 1:
                    //Add reward, maybe achievement or +1 to health or accessory (or multiple)
                    break;
            }
            ps.chestCollected[chestNum] = true;
        }
    }
    public void ChestPuzzle(int chestNum)
    {
        chestNum = (chestNum + chestNumRand) % 4;
        Debug.Log(chestNum);
        if (!chestRiddleComplete)
        {
            if (chestNum == nextChestRiddle)
            {
                if (chestNum == 3)
                {
                    ps.purse += 30;
                    nextChestRiddle = 0;
                    chestRiddleComplete = true;
                }
                else
                    nextChestRiddle++;
            }
            else
            {
                chestRiddleTraps.Add(CreateSpikeTrap(-34.5f + Random.Range(0, 2), 14.5f + Random.Range(0, 6)));
                chestRiddleTraps.Add(CreateSpikeTrap(-36.5f + Random.Range(0, 6), 16.5f + Random.Range(0, 2)));
                nextChestRiddle = 0;
            }
        }
    }
    //Riddles
    public void ShowRiddle(int category)
    {
        RiddleAnswer.interactable = true;
        Riddle.SetActive(true);
        if (riddleList[category].riddleComplete)
        {
            RiddleText.text = "Riddle Completed";
            RiddleAnswer.interactable = false;
        }
        else
        {
            RiddleText.text = riddleList[category].RiddleText;
            riddleList[category].riddleViewed = true;
            currentRiddleAnswers = riddleList[category].RiddleAnswer;
            currentRiddle = category;
            RiddleAnswer.Select();
        }
    }
    private void RiddleComplete(int riddle)
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
            case 2:
                Unlock(4);
                break;
            case 3:
                Unlock(5);
                Unlock(6);
                break;
            case 4:
                Unlock(7);
                break;
            case 5:
                Unlock(8);
                break;
            case 6:
                Unlock(9);
                break;
            case 7:
                Unlock(10);
                break;
        }
    }
    public void InstantiateRiddles()
    {
        //Tutorial Floor Riddles
        riddleList.Add(new Riddle() { RiddleText = "What object resides behind the doors to the northwest?", RiddleAnswer = new string[] { "chest", "crate" } });
        riddleList.Add(new Riddle() { RiddleText = "How many rocks are in the shop area?", RiddleAnswer = new string[] { "fourteen", "14" } });
        riddleList.Add(new Riddle() { RiddleText = "What begins with an 'e', ends with an 'e', and only contains one letter?", RiddleAnswer = new string[] { "envelope" } });
        //Floor 1 Riddles
        riddleList.Add(new Riddle() { RiddleText = "I follow you all the time and copy your every move, but you can’t touch me or catch me. What am I?", RiddleAnswer = new string[] { "shadow" } });
        riddleList.Add(new Riddle() { RiddleText = "What has many keys but can’t open a single lock?", RiddleAnswer = new string[] { "piano" } });
        riddleList.Add(new Riddle() { RiddleText = "What invention lets you look right through a wall?", RiddleAnswer = new string[] { "window" } });
        riddleList.Add(new Riddle() { RiddleText = "If you’re running in a race and you pass the person in second place, what place are you in?", RiddleAnswer = new string[] { "second", "2nd" } });
        riddleList.Add(new Riddle() { RiddleText = "What has to be broken before you can use it?", RiddleAnswer = new string[] { "egg" } });
        //Floor 2 Riddles
        riddleList.Add(new Riddle() { RiddleText = "What has a head and a tail but no body?", RiddleAnswer = new string[] { "coin", "penny", "nickle", "dime", "quarter" } });
        riddleList.Add(new Riddle() { RiddleText = "It stalks the countryside with ears that can’t hear. What is it?", RiddleAnswer = new string[] { "corn" } });
        riddleList.Add(new Riddle() { RiddleText = "What kind of coat is best put on wet?", RiddleAnswer = new string[] { "paint" } });
        riddleList.Add(new Riddle() { RiddleText = "I am an odd number. Take away a letter and I become even. What number am I?", RiddleAnswer = new string[] { "seven", "7" } });
        riddleList.Add(new Riddle() { RiddleText = "Mary has four daughters, and each of her daughters has a brother. How many children does Mary have?", RiddleAnswer = new string[] { "five", "5" } });
        riddleList.Add(new Riddle() { RiddleText = "What five-letter word becomes shorter when you add two letters to it?", RiddleAnswer = new string[] { "short" } });
        //Item Shop Riddles
        riddleList.Add(new Riddle() { RiddleText = "How many doors are on the first floor of the mine?", RiddleAnswer = new string[] { "" } });
        riddleList.Add(new Riddle() { RiddleText = "How many doors are on the second floor of the mine?", RiddleAnswer = new string[] { "" } });
        riddleList.Add(new Riddle() { RiddleText = "What accessory adorns your face?", RiddleAnswer = new string[] { "sunglasses", "shades" } });
    }
    //Traps
    private void CreateTraps ()
    {
        //Tutorial Floor Traps
        CreateSpikeTrap(-44.5f, 5.5f);
        CreateSpikeTrap(-44.5f, 6.5f);
        CreateSpikeTrap(-44.5f, 7.5f);
        CreateSpikeTrap(-44.5f, 8.5f);
        CreateSpikeTrap(-44.5f, 9.5f);
    }
    private GameObject CreateSpikeTrap (float x, float y)
    {
        GameObject newSpikeTrap = Instantiate(spikeTrap);
        newSpikeTrap.name = "Spike";
        newSpikeTrap.SetActive(true);
        newSpikeTrap.transform.position = new Vector2(x, y);
        return newSpikeTrap;
    }
    private void CreateFireTrap (float x, float y)
    {
        GameObject newFireTrap = Instantiate(fireTrap);
        newFireTrap.name = "Fire";
        newFireTrap.SetActive(true);
        newFireTrap.transform.position = new Vector2(x, y);
    }
    public void TrapDamage(int damage)
    {
        switch (damage)
        {
            case 0:
                damage = spikeDamage;
                break;
            case 1:
                damage = fireDamage;
                break;
        }
        ps.playerHP -= damage - ps.playerArmor;
        if (ps.playerHP <= 0)
            Death();
    }
    //Save/Load
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
    //Death
    private void Death()
    {
        ps.playerHP = 0;
        playerAnim.SetBool("Death", true);
        DeathScreen.SetActive(true);
        dead = true;
    }
    public void Respawn()
    {
        CreateTombstone(pos.x, pos.y);
        pos.x = 0; pos.y = 0;
        transform.position = pos;
        DeathScreen.SetActive(false);
        playerAnim.SetBool("Death", false);
        dead = false;
        ps.playerHP = 1;
        LockAll();
        //Any punishment? Maybe limited deaths
    }
    private void CreateTombstone(float x, float y)
    {
        GameObject newTombstone = Instantiate(tombstone);
        newTombstone.GetComponent<SpriteRenderer>().sprite = tombstone2;
        newTombstone.SetActive(true);
        newTombstone.transform.position = new Vector2(x, y - 0.2f);
    }
}

public class Riddle
{
    public string RiddleText { get; set; }
    public string[] RiddleAnswer { get; set; }
    public bool riddleComplete = false;
    public bool riddleViewed = false;
}
