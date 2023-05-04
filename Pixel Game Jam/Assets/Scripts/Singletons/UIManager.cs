using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    GameManager gm;
    [SerializeField] private Image image;
    public GameObject settingsScreen;
    public GameObject noteScreen;
    public string date;
    public string para;
    public string noteName;
    public float fadeDuration = 0.2f;
    public float fadeWaitTime = 0.05f;
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
        if (image == null) { 
            image = GameObject.Find("ScreenFader").GetComponent<Image>();
        }

        fadeDuration = 0.2f;
        fadeWaitTime = 0.05f;

        if (settingsScreen == null) {
            settingsScreen = GameObject.Find("Canvas/Settings");
            settingsScreen.SetActive(false);
        }

    }

    public IEnumerator FadeScreen()
    {
        // Fade to black
        float t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0.0f, 1.0f, t / fadeDuration);
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }

        // Wait for a moment
        yield return new WaitForSeconds(fadeWaitTime);

        if (gm.playerIsTeleporting) { 
            gm.teleportPlayer();
        }
        
        
        // Fade back in
        t = 0.0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, t / fadeDuration);
            if (t >= fadeDuration / 2) { 
                gm.playerCanMove = true;
            }
            image.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return null;
        }
    }

    public void openSettings() { 
        gm.uiActive = true;
        settingsScreen.SetActive(true);
    }

    public void closeSettings () {
        settingsScreen.SetActive(false);
    }

    public void showNote() {
        noteScreen.transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = date;
        noteScreen.transform.GetChild(1).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = para;
        noteScreen.transform.GetChild(2).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = noteName;
        gm.uiActive = true;
        noteScreen.gameObject.SetActive(true);
    }

    public void closeNote() {
        noteScreen.gameObject.SetActive(false);
    }

    public void closeUI() { 
        closeNote();
        closeSettings();
        gm.uiActive = false;
    }
}
