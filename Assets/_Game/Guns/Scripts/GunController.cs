using System;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private MachinePistolDataObject _MachinePistolDataObject;

    [SerializeField] private Transform reticle;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform r_renderedSprite;	

    private GameObject _bullet;
    
    private float autoFireRate = 5f;
    private int ammoClipSize = 0;
    private float reloadTime = 0.5f;

    private int spread = 1;
    private float spreadAngle = 20f;

    private bool canRotate;
    private float rotatationSpeed;

    private float jitterAngle = 5f;

    [System.NonSerialized] public bool isFireInput = true; 

    private float reloadTimeStore;
    private int ammoInClip;
    private float nextFire;

    private Quaternion aimRotation;

    private bool _FacingRight = true;  // For determining which way the player is currently facing.

    public virtual void Start()
    {
        //Variable Setup
        _bullet = _MachinePistolDataObject.bullet;
        autoFireRate = _MachinePistolDataObject.autoFireRate;
        ammoClipSize = _MachinePistolDataObject.ammoClipSize;
        reloadTime = _MachinePistolDataObject.reloadTime;
        spread = _MachinePistolDataObject.spread;
        spreadAngle = _MachinePistolDataObject.spreadAngle;
        canRotate = _MachinePistolDataObject.canRotate;
        rotatationSpeed = _MachinePistolDataObject.rotatationSpeed;
        jitterAngle = _MachinePistolDataObject.jitterAngle;

        autoFireRate = 1/autoFireRate; //Convert bullets per seconds into fireRate

        if(gameObject.GetComponent<CharacterData>() != null)
        {
            if(gameObject.GetComponent<CharacterData>().dataObject.faction == CharacterDataObject.Faction.enemy)
            {
                reticle = GameObject.Find("Player").transform;
            }
        } else {
            reticle = GameObject.Find("Reticle").transform;
        }
    }

    public virtual void Update() 
    {
        //AIM logic
        Vector3 vectorToTarget = reticle.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        aimRotation = Quaternion.Slerp(aimRotation, q, Time.deltaTime * rotatationSpeed);
        if(canRotate) {
            transform.rotation = aimRotation;
        }

        //Gun Sprite Flipping
        float aimPos = reticle.position.x - transform.position.x;
        if (aimPos > 0 && !_FacingRight)
        {
            Flip();
        }
        else if (aimPos < 0 && _FacingRight)
        {
            Flip();
        }   

        //FireButtonCode
        // if (autoFireRate == 0 && Input.GetButtonDown("Fire1"))
        // {  
        //     Shoot ();
        // } else
        if (isFireInput && Time.time > nextFire) //Input.GetButton("Fire1")
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
        float forwardAngle = aimRotation.eulerAngles.z;
        for(int i = 1; i <= spread; i++) 
        {
            float modI = i%2;
            float factor = 1;
            if(modI == 0) factor = -1;
            float bulletAngle = (int)(i/2)*factor*spreadAngle + forwardAngle;
            GameObject bulletInstatiation = Instantiate(_bullet, barrel.position, aimRotation);
            float jitter = UnityEngine.Random.Range(-jitterAngle, jitterAngle); 
            bulletInstatiation.GetComponent<BulletController>().bulletAngle = bulletAngle+jitter;
            Destroy(bulletInstatiation,2f);
        }
    }

    private void Flip()
	{
		_FacingRight = !_FacingRight;

		Vector3 theScale = r_renderedSprite.localScale;
		theScale.y *= -1;
		r_renderedSprite.localScale = theScale;
	}
}
