
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
        Move(bulletAngle);
    }
}
