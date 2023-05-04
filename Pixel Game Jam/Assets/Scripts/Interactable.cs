using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public GameObject puzzle;
    public GameObject holderGameObject;
    public Transform parent;
    private Sprite startingSprite;
    public Sprite endSprite;
    private BoxCollider2D bc;

    public bool onlyText;
    public string date;
    [TextArea(5, 10)]
    public string para;
    public string noteName;

    GameManager gm;
    PuzzleManager pm;
    UIManager ui;

    private SpriteRenderer sr;

    private void Awake()
    {
        if (puzzle == null) {
            puzzle = holderGameObject;
        }
        if (!onlyText) { 
            puzzle = Instantiate(puzzle);
            puzzle.gameObject.transform.SetParent(parent);
            puzzle.SetActive(false);
        }
    }
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        gm = GameManager.instance;
        pm = PuzzleManager.instance;
        ui = UIManager.instance;
        sr = gameObject.GetComponent<SpriteRenderer>();
        startingSprite = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            sr.sprite = endSprite;
            if (onlyText) {
                gm.noteAvailable = true;
                ui.noteName = noteName;
                ui.date = date;
                ui.para = para;
                return;
            }
            gm.puzzleAvailable = true;
            pm.puzzle = puzzle;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = startingSprite;
            gm.puzzleAvailable = false;
            pm.puzzle = null;
            gm.noteAvailable = false;
        }
    }

    private void startPuzzling() { 
        
    }
}
