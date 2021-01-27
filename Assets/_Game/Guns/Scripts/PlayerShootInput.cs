using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootInput : MonoBehaviour
{
    private GunController guncontroller;

    void Awake()
    {
        guncontroller = gameObject.GetComponent<GunController>();
    }

    void Update()
    {
        guncontroller.isFireInput = Input.GetButton("Fire1");
    }
}
