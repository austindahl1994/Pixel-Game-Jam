using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    private Animator playerAnim;
    private SpriteRenderer playerSprite;
    public BoxCollider2D playerBC;

    public Vector3 whereToTeleportPlayer;
    public bool playerIsTeleporting;
    public bool showPaths;
    public bool playerCanMove;
    public bool isMovingright;
    public bool isFacingRight;
    public bool isInDoorway;
    public bool hasLeftDoorway;
    public bool pinSolved;
    public bool textFinished;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        isInDoorway = false;
        isFacingRight = false;
        hasLeftDoorway = true;
        pinSolved = false;
        textFinished = true;
    }

    private void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerBC = player.GetComponent<BoxCollider2D>();
    }

    public void movingRight() {
        if (!playerCanMove) {
            return;
        }
        playerAnim.SetBool("Walking", true);
        playerSprite.flipX = true;
        isFacingRight =true;
        isMovingright=true;
    }

    public void movingLeft() {
        if (!playerCanMove)
        {
            return;
        }
        playerAnim.SetBool("Walking", true);
        playerSprite.flipX = false;
        isFacingRight = false;
        isMovingright = false;
    }

    public void notMoving() {
        playerAnim.SetBool("Walking", false);
        if (isFacingRight) { 
            playerSprite.flipX = true;
        }
    }

    public void teleportPlayer()
    {
        Debug.Log("Teleport called with v3:" + whereToTeleportPlayer);
        player.gameObject.transform.position = new Vector3(whereToTeleportPlayer.x + 0.5f, whereToTeleportPlayer.y, 0);
        hasLeftDoorway = false;
        playerIsTeleporting = false;
    }
}
