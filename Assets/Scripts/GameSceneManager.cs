using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;
    public Animator playerAnim;
    private GameManager gm;
    private PlayerStats ps;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    int facingDir;
    bool attacking;

    bool inShopArea;

    private void Start()
    {
        gm = GameManager.Instance;
        ps = gm.playerStats;
        inShopArea = false;
        attacking = false;
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
        if (inShopArea && pos.x > 29.5)
        {
            pos.x = 10.5f;
            pos.y = 2.5f;
            transform.position = pos;
            inShopArea = false;
        }
    }

    private void FixedUpdate()
    {
        if (!attacking)
            move(horizontalMove, verticalMove);
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
        inShopArea = true;
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
