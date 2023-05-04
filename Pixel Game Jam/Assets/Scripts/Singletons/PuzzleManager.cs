using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    private SpriteRenderer sr;
    GameManager gm;
    public GameObject puzzle;
    public GameObject puzzleHolder;
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
        disableChildren(puzzleHolder);
    }

    private void disableChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform) { 
            child.gameObject.SetActive(false);
        }
    }

    public void startPuzzle()
    {
        sr = puzzle.gameObject.GetComponent<SpriteRenderer>();
        gm.isPuzzlin = true;
        puzzle.gameObject.transform.position = gm.player.transform.position;
        sr.sortingLayerName = "Puzzle";
        puzzle.gameObject.SetActive(true);
    }

    public void endPuzzle() { 
        gm.isPuzzlin = false;
        puzzle.gameObject.SetActive(false);
    }
}
