using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private Rigidbody2D playerRB;
    GameManager gm = GameManager.instance;
    DoorManager dm;
    PuzzleManager pm;
    UIManager ui;
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
        pm = PuzzleManager.instance;
        ui = UIManager.instance;    
        playerRB = gm.player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (gm.uiActive || gm.isPuzzlin) {
            gm.playerCanMove = false;
        }
        if (horizontalInput > 0) {
            gm.movingRight();
        } else if (horizontalInput < 0) {
            gm.movingLeft();
        } else {
            gm.notMoving();
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            interact();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            exit();
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
        if (gm.noteAvailable && !gm.uiActive)
        {
            ui.showNote();
            gm.uiActive = true;
            return;
        }
        else { 
            ui.closeNote();
            gm.uiActive = false;
            gm.playerCanMove = true; //figure this out later
        }
        if (gm.isPuzzlin) { 
            pm.endPuzzle();
            gm.playerCanMove = true;
            return;
        }
        if (gm.puzzleAvailable) {
            gm.isPuzzlin = true;
            pm.startPuzzle();
            gm.playerCanMove= false;
            return;
        }
        if (gm.isInDoorway && (dm.doorPos != null) && !gm.playerIsTeleporting) {
            dm.teleport();
        }
    }

    private void exit() {
        if (gm.isPuzzlin)
        {
            pm.endPuzzle();
            gm.playerCanMove = true;
            return;
        }
        if (gm.uiActive)
        {
            ui.closeUI();
            gm.playerCanMove = true;
        }
        else {
            ui.openSettings();
            gm.playerCanMove = true;
        }


    }
}
