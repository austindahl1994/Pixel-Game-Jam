using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool showPaths;
    public bool playerCanMove;
    public bool isMovingright;
    public bool isInDoorway;
    public GameObject player;
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
        playerCanMove = true;
        isInDoorway = false;
        showPaths = true;
    }
}
