using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public GameObject player;
    private Rigidbody2D playerRB;

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

        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2 (horizontalInput, 0);
        movement.Normalize();
        movement *= moveSpeed;

        playerRB.MovePosition(playerRB.position + movement * Time.fixedDeltaTime);
    }
}
