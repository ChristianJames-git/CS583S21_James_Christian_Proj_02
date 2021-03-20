using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject player;
    public Vector3 pos;
    public Animator playerAnim;
    private GameManager gm = GameManager.Instance;
    private PlayerStats ps = GameManager.Instance.playerStats;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    float mouseX;
    float mouseY;
    bool mouseClick = false;
    bool stopClick = false;
    private void Update()
    {
        pos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        mouseClick = Input.GetMouseButtonDown(0);
        stopClick = Input.GetMouseButtonUp(0);
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
    }

    private void FixedUpdate()
    {
        move(horizontalMove, verticalMove);
        if (mouseClick)
            attack(mouseX, mouseY);
        if (stopClick)
            playerAnim.SetBool("Attack?", false);

    }

    public void move(float horizontal, float vertical)
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

    public void attack(float mouseX, float mouseY)
    {
        float horizontal = mouseX - pos.x;
        float vertical = mouseY - pos.y;
        float compare = Mathf.Abs(vertical) - Mathf.Abs(horizontal);
        Debug.Log("compare:" + compare + " vertical:" + Mathf.Abs(vertical) + " horizontal:" + Mathf.Abs(horizontal) + " Vertical:" + mouseY + " Horizontal:" + mouseX);
        int facingDir;
        if (compare >= 0)
        {
            if (vertical < 0)
                facingDir = 0;
            else
                facingDir = 2;
        } else {
            if (horizontal < 0)
                facingDir = 1;
            else
                facingDir = 3;
        }
        playerAnim.SetBool("Attack?", true);
        playerAnim.SetInteger("Direction", facingDir);
    }

    public void SavePlayer()
    {
        ps.position[0] = pos.x;
        ps.position[1] = pos.y;
        GameManager.Instance.SavePlayer();
    }

    public void LoadPlayer()
    {
        GameManager.Instance.LoadPlayer();
        pos.x = ps.position[0];
        pos.y = ps.position[1];
    }
}
