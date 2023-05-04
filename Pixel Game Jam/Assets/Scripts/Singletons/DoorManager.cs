using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public static DoorManager instance;
    public GameObject path;
    public Dictionary<Vector3Int, Vector3Int> tilePairs = new Dictionary<Vector3Int, Vector3Int>();
    public Vector3Int doorPos;
    Vector3 toBeMoved;
    GameManager gm;
    UIManager ui;
    SceneManage sm;
    public FloorPaths firstFloor;
    public FloorPaths secondFloor;
    public FloorPaths thirdFloor;
    public FloorPaths fourthFloor;
    private FloorPaths currentFloor;
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
        sm = SceneManage.instance;
        setPaths();
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

    private void setPaths()
    {
        string sceneName = sm.getSceneName();

        switch (sceneName) {
            case "First Floor":
                //Debug.Log("It is first floor");
                currentFloor = firstFloor;
                break;
            case "Second Floor":
                //Debug.Log("It is second floor");
                currentFloor = secondFloor;
                break;
            case "Third Floor":
                //Debug.Log("It is third floor");
                currentFloor = thirdFloor;
                break;
            case "Fourth Floor":
                //Debug.Log("It is fourth floor");
                currentFloor = fourthFloor;
                break;
            default:
                //Debug.Log("No floor currently set");
                break;
        }

        foreach (Vector2Tuple tuple in currentFloor.path) {
            Vector3Int start = new Vector3Int((int)tuple.start.x, (int)tuple.start.y, 0);
            Vector3Int end = new Vector3Int((int)tuple.end.x, (int)tuple.end.y, 0);
            tilePairs.Add(start, end);
        }
    }
}
