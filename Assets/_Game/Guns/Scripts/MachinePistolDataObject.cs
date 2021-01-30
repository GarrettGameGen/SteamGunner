using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MachinePistolDataObject", menuName = "Game Data/MachinePistolDataObject")]
public class MachinePistolDataObject : ScriptableObject
{
    public GameObject bullet;
    
    public float autoFireRate = 5f;
    public int ammoClipSize = 0;
    public float reloadTime = 0.5f;

    public int spread = 1;
    public float spreadAngle = 20f;

    public bool canRotate = true;
    public float rotatationSpeed = 10f;

    [Range(0, 10f)] public float jitterAngle = 5f;
}
