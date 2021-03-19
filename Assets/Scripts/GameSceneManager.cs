using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnim;
    public GameManager gm = GameManager.Instance;
    public PlayerStats ps = GameManager.Instance.playerStats;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    private void Update()
    {
        player.transform.position = new Vector3(ps.position[0], ps.position[1], 0);
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        move(horizontalMove, verticalMove);
    }

    public void move(float horizontal, float vertical)
    {
        Debug.Log(horizontal + "and" + vertical);
        playerAnim.SetInteger("Horizontal", (int)horizontal);
        playerAnim.SetInteger("Vertical", (int)vertical);
    }

    public void SavePlayer()
    {
        GameManager.Instance.SavePlayer();
    }

    public void LoadPlayer()
    {
        GameManager.Instance.LoadPlayer();
    }
}
