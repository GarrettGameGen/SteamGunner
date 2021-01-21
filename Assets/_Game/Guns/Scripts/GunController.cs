using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform reticle;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotatationSpeed;
    [SerializeField] private Transform r_renderedSprite;	
    
    [SerializeField] private float cooldownSeconds = 0.5f;
    private float cooldown;
    [SerializeField] private int maxAmmo = 100;
    private int ammo;
    private float fireRate;
    private float nextFire;

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    void Start()
    {
        
    }

    private void Update() 
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
        if (fireRate == 0 && Input.GetButtonDown("Fire1"))
        {   //Reduced to a single if, cause It does exactly the same
            //And in my Opinion, looks better. (You might want not to
            //in case you have anything else here that do needs the if)
            Shoot ();
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire && fireRate > 0)
            {
            //I added "&& fireRate > 0", because if not, this will run if the user decides
            //to hold the button, as "GetButtonDown" only returns true the frame the button
            //is pressed, and while its hold, is false, so the "else" will run, and so will this.
                if (ammo > 0)
                {   //If you have ammo
                   nextFire = Time.time + fireRate;
                   Shoot ();
                   ammo--; //Explained by itself
                }
                if (ammo == 0)
                {   //If you no longer have ammo
                   if (cooldown > Time.time)
                   {   //If there is no cooldown (relatively)
                      cooldown = Time.time + cooldownSeconds;
                   }
                }
            }
        }
 
       if (Time.time > cooldown && ammo == 0)
       {   //If the cooldown is over, and you have no ammo cause else this will run kinda always,
           //as here we set the ammo to maxAmmo, and cooldown only happens when you run out
           //of ammo, then you will be constantly fulling the ammo.
           ammo = maxAmmo;
       }
    }

    public void Shoot() 
    {
        GameObject bulletInstatiation = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstatiation.GetComponent<BulletController>().gunRotationOnFire = transform.rotation;
        Destroy(bulletInstatiation,5f);
    }

    private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 theScale = r_renderedSprite.localScale;
		theScale.y *= -1;
		r_renderedSprite.localScale = theScale;
	}
}
