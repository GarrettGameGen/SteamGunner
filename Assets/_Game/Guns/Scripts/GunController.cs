using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform reticle;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rotatationSpeed;
    [SerializeField] private Transform r_renderedSprite;						//

    private bool m_FacingRight = true;  // For determining which way the player is currently facing.

    void Start()
    {
        
    }

    private void Update() 
    {
        Aim();  
        float aimPos = reticle.position.x - transform.position.x;
        if (aimPos > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (aimPos < 0 && m_FacingRight)
        {
            Flip();
        }   
    }

    // Update is called once per frame
    public void Aim()
    {
        Vector3 vectorToTarget = reticle.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotatationSpeed);
    }

    public void Shoot() 
    {

    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = r_renderedSprite.localScale;
		theScale.y *= -1;
		r_renderedSprite.localScale = theScale;
	}
}
