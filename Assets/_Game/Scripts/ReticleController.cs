using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    public float MouseSensitivity = 0.1f;

    private Vector3 MouseCoords;
    private Camera mainCamera; 
    private void Start() {
        mainCamera = Camera.main;
    }
    void Update () {
         MouseCoords = Input.mousePosition;
         MouseCoords = mainCamera.ScreenToWorldPoint (MouseCoords);
         transform.position = Vector2.Lerp (transform.position, MouseCoords, MouseSensitivity);
    }
}
