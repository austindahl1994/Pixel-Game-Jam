using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject holderGameObject;
    private BoxCollider2D bc;
    GameManager gm;
    PuzzleManager pm;

    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        gm = GameManager.instance;
        pm = PuzzleManager.instance;

        if (puzzle == null) {
            puzzle = holderGameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
            gm.puzzleAvailable = true;
            pm.puzzle = puzzle;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.puzzleAvailable = false;
        }
    }

    private void startPuzzling() { 
        
    }
}
