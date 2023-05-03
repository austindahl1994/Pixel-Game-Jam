using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTiles : MonoBehaviour
{
    Tilemap tilemap;
    GameManager gm;
    DoorManager dm;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        gm = GameManager.instance;
        dm = DoorManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player moved in front of door");
            Vector3Int tilePosition = tilemap.WorldToCell(collision.transform.position);
            // Adjust the tile position based on the player's position relative to the door
            if (gm.isMovingright)
            {
                tilePosition.x++;
            }
            else if (!gm.isMovingright)
            {
                tilePosition.x--;
            }
            Debug.Log("tilePosition position: " + tilePosition);
            gm.isInDoorway = true;
            dm.doorPos = tilePosition;
            //dm.teleport(tilePosition);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited door");
            gm.isInDoorway = false;
        }
    }

}
