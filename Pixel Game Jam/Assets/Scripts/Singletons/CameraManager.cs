using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;

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
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (cam == null) {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        cam.gameObject.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
    }
}
