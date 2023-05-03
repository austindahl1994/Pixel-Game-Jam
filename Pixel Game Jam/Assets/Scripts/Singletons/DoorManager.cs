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
    GameManager gm;
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
        Vector3Int tile1Position = new Vector3Int(-11, -8, 0);
        Vector3Int tile2Position = new Vector3Int(-8, -0, 0);
        tilePairs.Add(tile1Position, tile2Position);
        showPaths();
        //GameObject testPath = Instantiate(path);
        //testPath.gameObject.transform.GetChild(0).transform.position = tile1Position;
        //testPath.gameObject.transform.GetChild(1).transform.position = tile2Position;
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

    public void teleport() {
        Debug.Log("Teleport called with position: " + doorPos);
        if (tilePairs.ContainsKey(doorPos)) {
            Debug.Log("Dictionary contains key!");
            Vector3 toBeMoved = new Vector3(tilePairs[doorPos].x + 0.5f, tilePairs[doorPos].y, 0);
            Debug.Log("Tobemoved v3 is: " + toBeMoved);
            gm.player.gameObject.transform.position = toBeMoved;
        } else if (tilePairs.ContainsValue(doorPos)) {
            Debug.Log("Dictionary contains Value!");
            foreach (KeyValuePair<Vector3Int, Vector3Int> pair in tilePairs) { 
                if (pair.Value == doorPos)
                {
                    Vector3 toBeMoved = new Vector3(pair.Key.x + 0.5f, pair.Key.y, 0);
                    Debug.Log("Tobemoved v3 is: " + toBeMoved);
                    gm.player.gameObject.transform.position = toBeMoved;
                }
            }
        }
    }
}
