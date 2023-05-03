using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private Rigidbody2D playerRB;
    GameManager gm = GameManager.instance;
    DoorManager dm;
    public Vector3Int doorPosition;

    [SerializeField] private float moveSpeed = 5;
    private float horizontalInput;
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
    }

    private void Start()
    {
        gm = GameManager.instance;
        dm = DoorManager.instance;
        playerRB = gm.player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        gm.isMovingright = horizontalInput > 0 ? true : false;
        if (Input.GetKeyDown(KeyCode.E)) {
            interact();
        }
    }

    private void FixedUpdate()
    {
        movePlayer();
    }

    private void movePlayer() {
        if (gm != null && gm.playerCanMove) { 
            Vector2 movement = new Vector2 (horizontalInput, 0);
            movement.Normalize();
            movement *= moveSpeed;
            playerRB.MovePosition(playerRB.position + movement * Time.fixedDeltaTime);
        }
    }

    private void interact() {
        if (gm.isInDoorway && (dm.doorPos != null)) {
            dm.teleport();
        }
    }
}
