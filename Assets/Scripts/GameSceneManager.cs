using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;
    public Animator playerAnim;
    private GameManager gm;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    int facingDir;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        pos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.SetBool("Attack?", true);
            playerAnim.SetInteger("AttackDir", facingDir);
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            playerAnim.SetBool("Attack?", false);
        }
    }

    private void FixedUpdate()
    {
        move(horizontalMove, verticalMove);
    }

    public void move(float horizontal, float vertical)
    {
        if (horizontal > 0)
        {
            pos.x += gm.playerStats.speed * Time.deltaTime;
            facingDir = 1;
        }
        if (horizontal < 0)
        {
            pos.x -= gm.playerStats.speed * Time.deltaTime;
            facingDir = 3;
        }
        if (vertical > 0)
        {
            pos.y += gm.playerStats.speed * Time.deltaTime;
            facingDir = 0;
        }
        if (vertical < 0)
        {
            pos.y -= gm.playerStats.speed * Time.deltaTime;
            facingDir = 2;
        }
        transform.position = pos;
        playerAnim.SetInteger("Horizontal", (int)horizontal);
        playerAnim.SetInteger("Vertical", (int)vertical);
    }

    public void SavePlayer()
    {
        gm.playerStats.position[0] = pos.x;
        gm.playerStats.position[1] = pos.y;
        GameManager.Instance.SavePlayer();
    }

    public void LoadPlayer()
    {
        GameManager.Instance.LoadPlayer();
        pos.x = gm.playerStats.position[0];
        pos.y = gm.playerStats.position[1];
    }
}
