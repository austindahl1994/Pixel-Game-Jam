using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    GameManager gm;
    InputManager im;
    public GameObject puzzle;
    public bool isPuzzlin;
    private void Awake()
    {
        instance = this;
        isPuzzlin = false;

    }

    private void Start()
    {
        gm = GameManager.instance;
        im = InputManager.instance;
    }

    public void startPuzzle()
    {
        isPuzzlin = true;
        gm.player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        Debug.Log("Starting puzzle with name: " + puzzle.name);
        gm.playerBC.enabled = false;
        puzzle.gameObject.SetActive(true);
        puzzle.gameObject.transform.position = gm.player.transform.position;
        new Vector3(gm.player.transform.position.x - 5, gm.player.transform.position.y, 0);
    }

    public void endPuzzle() {
        if (!isPuzzlin) { return; }
        Debug.Log("Ending puzzle with name: " + puzzle.name);
        isPuzzlin = false;
        puzzle.gameObject.SetActive(false);
        gm.player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        gm.playerBC.enabled = true;
    }
}
