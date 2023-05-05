using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class pinPad : MonoBehaviour
{
    public Transform reader;
    [SerializeField] private List<int> nums = new List<int>();
    [SerializeField] private List<Sprite> spriteNumbers = new List<Sprite>();
    [SerializeField] private List<int> correctPin = new List<int>();
    [SerializeField] private List<int> evilPin = new List<int>();
    [SerializeField] private float flashDuration;
    public GameObject stupidGreenCover;
    public bool animationFinished;
    public bool animationStarted;
    public bool evilBeenPlayed = false;
    Color originalColor;
    private bool isFlashing;

    private void Start()
    {
        nullObjectsCheck();
        if (flashDuration == 0) {
            flashDuration = 0.1f;
        }
        originalColor = new Color(0, 255, 0, 255);
        evilBeenPlayed = false;
        animationFinished = false;
        animationStarted = false;
        stupidGreenCover.SetActive(false);
    }
    public void buttonPushed(int buttonNumber) { 
        if (buttonNumber < 9) {
            if (nums.Count == 4) {
                return; 
            }
            buttonNumber++;
            updateList(buttonNumber);
        } else if (buttonNumber == 9) {
            clearButton();
        } else if (buttonNumber == 10) {
            if (nums.Count == 4)
            {
                return;
            }
            updateList(0);
        } else if(buttonNumber == 11) {
            submitButton();
        }
    }

    private void submitButton() {
        if (animationStarted || nums.Count == 0) {
            return;
        }

        bool matchesGood = true;
        bool matchesEvil = true;
        for (int i = 0; i < nums.Count; i++)
        {
            if (correctPin[i] != nums[i])
            {
                matchesGood = false;
                break;
            }
        }

        for (int i = 0; i < evilPin.Count; i++)
        {
            if (evilPin[i] != nums[i])
            {
                matchesEvil = false;
                break;
            }
        }

        if (matchesGood)
        {
            correct();
        }
        else if (matchesEvil)
        {
            if (evilBeenPlayed) {
                StartCoroutine(FlashSprite(originalColor));
                return;
            }
            StartCoroutine(playEvil());
        }
        else {
            StartCoroutine(FlashSprite(originalColor));
        }

    }

    private void clearButton() { 
        nums.Clear();
        for (int i = 0; (i < reader.childCount - 2); i++) { 
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private void updateList(int number)
    {
        if (animationStarted) { return; }
        nums.Add(number);
        updateSpritesReader(number);
    }

    private void updateSpritesReader(int buttonNumber) {
        if (buttonNumber != 0)
        {
            reader.GetChild(nums.Count - 1).gameObject.GetComponent<SpriteRenderer>().sprite = spriteNumbers[buttonNumber - 1];
        }
        else {
            reader.GetChild(nums.Count - 1).gameObject.GetComponent<SpriteRenderer>().sprite = spriteNumbers[spriteNumbers.Count - 1];
        }
    }

    private void nullObjectsCheck() {
        if (reader == null)
        {
            reader = this.gameObject.transform.GetChild(0);
        }
        if (correctPin.Count != 4)
        {
            for (int i = 1; i <= 4; i++)
            {
                correctPin.Add(i);
            }
        }
        if (evilPin.Count != 3)
        {
            for (int i = 0; i < 3; i++)
            {
                evilPin.Add(6);
            }
        }
        for (int i = 0; (i < reader.childCount - 2); i++)
        {
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    private IEnumerator FlashSprite(Color originalColor)
    {
        if (isFlashing)
        {
            yield break;
        }

        isFlashing = true;
        originalColor = reader.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;

        for (int i = 0; (i < reader.childCount - 2); i++)
        {
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        yield return new WaitForSeconds(flashDuration);
        for (int i = 0; (i < reader.childCount - 2); i++)
        {
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        }

        yield return new WaitForSeconds(flashDuration);
        for (int i = 0; (i < reader.childCount - 2); i++)
        {
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        yield return new WaitForSeconds(flashDuration);
        for (int i = 0; (i < reader.childCount - 2); i++)
        {
            reader.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = originalColor;
        }
        clearButton();
        isFlashing = false;
    }

    private void correct() {
        stupidGreenCover.SetActive(true);
        GameManager.instance.pinSolved = true;
    }

    private IEnumerator playEvil() {
        animationStarted = true;
        evilBeenPlayed = true;
        gameObject.GetComponentInChildren<pinButton>().canBePressed = false;
        clearButton();

        Animator anim = reader.GetChild(reader.childCount - 2).gameObject.GetComponent<Animator>();
        SpriteRenderer sr = reader.GetChild(reader.childCount - 2).gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        anim.Play("eyeReader");
        yield return new WaitForSeconds(1f);
        gameObject.GetComponentInChildren<pinButton>().canBePressed = true;
        animationStarted = false;
        yield return null;
    }

    public void setBlack() {
        SpriteRenderer sr = reader.GetChild(reader.childCount - 2).gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.black;
        Debug.Log("Set black called");
    }
}
