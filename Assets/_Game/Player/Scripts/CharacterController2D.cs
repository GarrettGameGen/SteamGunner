using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	//https://pastebin.com/aWkD7jgW

	[SerializeField] private float runSpeed = 40f;
	[SerializeField] private float maxSpeed = 100f;

	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private float m_JumpForce = 40f;							// Amount of force added when the player jumps.
    [SerializeField] private float m_Gravity = 8.0f;							// Amount of force added when the player jumps.
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private float m_JumpLength = 1f;								
	[Range(0, .4f)] [SerializeField] private float m_CoyoteTime = 0.1f;			// 

	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_RightCheck;							// 
	[SerializeField] private Transform m_LeftCheck;								//
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings


	[SerializeField] private Transform m_renderedSprite;						//
	[SerializeField] private Transform m_reticle;

	const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private float groundClock;			
	private bool m_RightLock;            // Whether or not the player is next to a wall to thier right
	private bool m_LeftLock;            // Whether or not the player is next to a wall to thier Left.
	const float k_SideLockRadius = .1f; // Radius of the overlap circle to determine if grounded
	const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D k_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
	private Vector3 currentVelocity; 
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private float jumpTime;								

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		k_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
				{
					OnLandEvent.Invoke();
					//StartCoroutine("Roll");
				}
			}
		}
		if(m_Grounded){
			groundClock = 0f;
			k_Rigidbody2D.gravityScale = 1;
		} else {
			groundClock += Time.fixedDeltaTime;
			k_Rigidbody2D.gravityScale = m_Gravity;
		}

		//RightLockCheck
		if (Physics2D.OverlapCircle(m_RightCheck.position, k_SideLockRadius, m_WhatIsGround))
		{
			m_RightLock = true;
		} else {
			m_RightLock = false;
		}
		//RightLockCheck
		if (Physics2D.OverlapCircle(m_LeftCheck.position, k_SideLockRadius, m_WhatIsGround))
		{
			m_LeftLock = true;
		} else {
			m_LeftLock = false;
		}
	}


	public void Move(float move, bool jump)
	{
		move *= runSpeed;

		//only control the player if grounded or airControl is turned on
		float aimPos = m_reticle.position.x - transform.position.x;
		if (aimPos > 0 && !m_FacingRight)
		{
			Flip();
		}
		else if (aimPos < 0 && m_FacingRight)
		{
			Flip();
		}

		//Jump & Gravity
		float yVelocity = k_Rigidbody2D.velocity.y;
		if (groundClock<m_CoyoteTime && jump)
		{
			groundClock = m_CoyoteTime+0.1f;
			yVelocity = m_JumpForce;
			jumpTime = Time.time+m_JumpLength;
		} else if(jump && jumpTime-Time.time > 0)// && jumpSustainLag+Time.time > jumpTime)
		{
			yVelocity = m_JumpForce;
		} else if(!m_Grounded){
			yVelocity = -1*m_Gravity;
		}

		//Move
		if (m_Grounded || m_AirControl)
		{
			if(m_RightLock && move>0) {
				move = 0;
			}
			if(m_LeftLock && move<0) {
				move = 0;
			}
			Vector3 targetVelocity = new Vector2(move * 10f, yVelocity);
			currentVelocity = Vector3.SmoothDamp(currentVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			k_Rigidbody2D.velocity = Vector2.ClampMagnitude(k_Rigidbody2D.velocity, maxSpeed);
			k_Rigidbody2D.velocity = currentVelocity;
		}
	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = m_renderedSprite.localScale;
		theScale.x *= -1;
		m_renderedSprite.localScale = theScale;
	}
	private IEnumerator Roll()
	{
		float rollDirection = 1;
		float aimPos = m_reticle.position.x - transform.position.x;
		if (aimPos > 0 && !m_FacingRight)
		{
			rollDirection = 1;
		}
		else if (aimPos < 0 && m_FacingRight)
		{
			rollDirection = -1;
		}

		int steps = 8;
		float totalTime = 0.25f;
		float angle = 360/steps * rollDirection;
		for(int i = 0; i < steps; i++)
		{
			m_renderedSprite.RotateAround(m_renderedSprite.transform.position,Vector3.forward, angle);
			yield return new WaitForSeconds(totalTime/steps);
		}
	}
}
