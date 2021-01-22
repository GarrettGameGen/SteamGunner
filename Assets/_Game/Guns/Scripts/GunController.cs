using System;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform reticle;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotatationSpeed;
    [SerializeField] private Transform r_renderedSprite;	
    
    [SerializeField] private float autoFireRate = 5f;
    [SerializeField] private int ammoClipSize = 0;
    [SerializeField] private float reloadTime = 0.5f;

    [SerializeField] private int spread = 1;
    [SerializeField] private float spreadAngle = 20f;

    [System.NonSerialized]public bool isFireInput = false; 

    private float reloadTimeStore;
    private int ammoInClip;
    private float nextFire;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    public virtual void Start()
    {
        autoFireRate = 1/autoFireRate; //Convert bullets per seconds into fireRate
    }

    public virtual void Update() 
    {
        //AIM logic
        Vector3 vectorToTarget = reticle.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotatationSpeed);

        //Gun Sprite Flipping
        float aimPos = reticle.position.x - transform.position.x;
        if (aimPos > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (aimPos < 0 && m_FacingRight)
        {
            Flip();
        }   

        //FireButtonCode
        if (autoFireRate == 0 && Input.GetButtonDown("Fire1"))
        {   //Reduced to a single if, cause It does exactly the same
            //And in my Opinion, looks better. (You might want not to
            //in case you have anything else here that do needs the if)
            Shoot ();
        }
        else if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            if (ammoInClip > 0 || ammoClipSize == 0)
            {   
                nextFire = Time.time + autoFireRate;
                Shoot ();
                ammoInClip--; 
                if (ammoInClip <= 0)
                {   
                    reloadTimeStore = Time.time + reloadTime;
                }
            }
            
        }
 
       if (Time.time > reloadTimeStore && ammoInClip <= 0 && ammoClipSize != 0)
       {   
           ammoInClip = ammoClipSize;
       }
    }

    public virtual void Shoot() 
    {
        float forwardAngle = transform.rotation.eulerAngles.z;
        for(int i = 0; i < spread; i++) 
        {
            forwardAngle += spreadAngle*i;
            GameObject bulletInstatiation = Instantiate(bullet, transform.position, transform.rotation);
            bulletInstatiation.GetComponent<BulletController>().bulletAngle = forwardAngle;
            Destroy(bulletInstatiation,5f);
        }
    }

    private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = r_renderedSprite.localScale;
		theScale.y *= -1;
		r_renderedSprite.localScale = theScale;
	}
}
