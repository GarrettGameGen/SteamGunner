using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootInput : MonoBehaviour
{
    public GunController guncontroller;

    void Update()
    {
        guncontroller.isFireInput = Input.GetButton("Fire1");
    }
}
