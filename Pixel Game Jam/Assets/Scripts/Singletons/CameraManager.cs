using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private Camera cam;
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
        if (cam == null) {
            cam = Camera.main;
        }
    }

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {
        cam.gameObject.transform.position = new Vector3 (gm.player.transform.position.x, gm.player.transform.position.y, -10);
    }
}
