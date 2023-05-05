using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    GameManager gm;
    public GameObject puzzle;
    private bool isPuzzlin;
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
        isPuzzlin = false;
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    public void startPuzzle()
    {
        isPuzzlin = true;
        Debug.Log("Starting puzzle with name: " + puzzle.name);
        puzzle.gameObject.SetActive(true);
        puzzle.gameObject.transform.position = gm.player.transform.position;
    }

    public void endPuzzle() {
        if (!isPuzzlin) { return; }
        Debug.Log("Ending puzzle with name: " + puzzle.name);
        isPuzzlin = false;
        puzzle.gameObject.SetActive(false);
    }
}
