using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleInteractable : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject holderGameObject;
    private Color originalColor;
    private SpriteRenderer sr;

    GameManager gm;
    InputManager im;
    PuzzleManager pm;

    private void Awake()
    {
        if (puzzle == null) {
            puzzle = holderGameObject;
        }
        puzzle = Instantiate(puzzle);
        puzzle.SetActive(false);
    }
    private void Start()
    {
        gm = GameManager.instance;
        pm = PuzzleManager.instance;
        im = InputManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            sr.color = new Color(133, 133, 133, 255);
            pm.puzzle = puzzle;
            im.state = "puzzle";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.color = originalColor;
            pm.puzzle = null;
            im.state = "none";
        }
    }

    private void startPuzzling() { 
        
    }
}
