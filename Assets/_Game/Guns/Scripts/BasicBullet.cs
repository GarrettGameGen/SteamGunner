using System;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : BulletController
{
   
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        angle = gunRotationOnFire.eulerAngles.z;
        angle = angle * Mathf.Deg2Rad;
        Vector2 velocityVector = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        Move(velocityVector);
    }
}
