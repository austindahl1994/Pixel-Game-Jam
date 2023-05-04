using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;
    public GameObject path;
    public Dictionary<Vector3Int, Vector3Int> tilePairs = new Dictionary<Vector3Int, Vector3Int>();
    public Vector3Int doorPos;
    Vector3 toBeMoved;
    GameManager gm;
    UIManager ui;
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
        ui = UIManager.instance;
        Vector3Int tile1Position = new Vector3Int(-11, -8, 0);
        Vector3Int tile2Position = new Vector3Int(-8, -0, 0);
        Vector3Int tile3Position = new Vector3Int(-4, 0, 0);
        Vector3Int tile4Position = new Vector3Int(15, -8, 0);
        tilePairs.Add(tile1Position, tile2Position);
        tilePairs.Add(tile3Position, tile4Position);
        showPaths();
    }

    private void showPaths() { 
        if (!gm.showPaths) { return; }
        foreach (KeyValuePair<Vector3Int, Vector3Int> pair in tilePairs) {
            GameObject testPath = Instantiate(path);
            testPath.gameObject.transform.GetChild(0).transform.position 
                = new Vector2(pair.Key.x + 0.5f, pair.Key.y);
            testPath.gameObject.transform.GetChild(1).transform.position 
                = new Vector2(pair.Value.x + 0.5f, pair.Value.y);
        }
    }

    public void teleport()
    {
        if (gm.playerIsTeleporting) {
            return;
        }
        if (!gm.hasLeftDoorway) {
            if (gm.whereToTeleportPlayer == doorPos)
            {
                gm.whereToTeleportPlayer = toBeMoved;
            }
            else {
                gm.whereToTeleportPlayer = doorPos;
            }
            StartCoroutine(teleportPause());
            return;
        }
        if (tilePairs.ContainsKey(doorPos)) {
            toBeMoved = new Vector3(tilePairs[doorPos].x, tilePairs[doorPos].y, 0);
            gm.whereToTeleportPlayer = toBeMoved;
            StartCoroutine(teleportPause());
        } else if (tilePairs.ContainsValue(doorPos)) {
            foreach (KeyValuePair<Vector3Int, Vector3Int> pair in tilePairs) { 
                if (pair.Value == doorPos)
                {
                    toBeMoved = new Vector3(pair.Key.x, pair.Key.y, 0);
                    gm.whereToTeleportPlayer = toBeMoved;
                    StartCoroutine(teleportPause());
                }
            }
        }
    }

    private IEnumerator teleportPause()
    {
        gm.playerIsTeleporting = true;
        gm.playerCanMove = false;
        yield return StartCoroutine(ui.FadeScreen());
    }
}
