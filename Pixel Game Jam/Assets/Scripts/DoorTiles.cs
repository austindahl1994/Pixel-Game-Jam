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
            Vector3Int tilePosition = tilemap.WorldToCell(collision.transform.position);
            if (gm.isMovingright)
            {
                tilePosition.x++;
            }
            else if (!gm.isMovingright)
            {
                tilePosition.x--;
            }
            gm.isInDoorway = true;
            dm.doorPos = tilePosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.isInDoorway = false;
            gm.hasLeftDoorway = true;
        }
    }

}
