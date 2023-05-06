using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private GameObject camObj;
    [SerializeField] private Camera cam;
    GameManager gm;
    
    private void Awake()
    {
        instance = this;
        camObj = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();
    }

    private void Start()
    {
        gm = GameManager.instance;
        cam.transform.position = new Vector3(gm.player.transform.position.x, gm.player.transform.position.y, -10);
    }

    private void Update()
    {
        cam.gameObject.transform.position = new Vector3 (gm.player.transform.position.x, gm.player.transform.position.y, -10);
    }
}
