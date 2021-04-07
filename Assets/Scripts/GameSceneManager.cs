using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private void FixedUpdate()
    {
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
