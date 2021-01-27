using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletController : MonoBehaviour
{
    [System.NonSerialized] public float bulletAngle;

    [SerializeField] private float speed = 10f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

    [Range(0, 1f)] [SerializeField] private float critChance = 0.5f;
    [SerializeField] private float critsizeBonus = 2f;
    [SerializeField] private float defaultDamage = 1f;
    [System.NonSerialized] public bool isCritical = false;
    [SerializeField] private bool isPhasing = false;                            //Can pass through objects

    [SerializeField] CharacterDataObject.Faction faction;
    
    private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;

    void Awake()
    {
        m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        if(rand <= critChance)
        {
            transform.localScale *= critsizeBonus;
            defaultDamage *= 2f;
            isCritical = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground") && !isPhasing) 
        {
            Destroy(gameObject);
        }

        Debug.Log("collision");
        if (other.gameObject.GetComponent<CharacterData>())
        {
            Debug.Log("with character");
            if (faction != other.gameObject.GetComponent<CharacterData>().faction)
            {
                Debug.Log("of opposing faction");
                other.gameObject.GetComponent<CombatHandler>().TakeDamage((int)defaultDamage);
            }
        }
    }

    public void Update()
	{
        float angle = bulletAngle;
        angle *= Mathf.Deg2Rad;
        Vector2 targetVelocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

        targetVelocity *= speed;
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public virtual void OnDestroy() 
    {
        
    }
}