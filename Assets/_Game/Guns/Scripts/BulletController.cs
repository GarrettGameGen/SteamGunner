﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    [System.NonSerialized] public Quaternion gunRotationOnFire;

    [SerializeField] private float speed = 10f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;


    // Start is called before the first frame update
    public virtual void Start()
    {
        m_Rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 targetVelocity)
	{
        targetVelocity *= speed;
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
}
