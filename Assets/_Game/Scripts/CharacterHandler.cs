using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CombatHandler))]
[RequireComponent(typeof(CharacterData))]
public abstract class CharacterHandler : MonoBehaviour
{
    CombatHandler damageHandler;
    [NonSerialized]
    public CharacterData data;

    public virtual void Awake()
    {
        data = GetComponent<CharacterData>();
        damageHandler = GetComponent<CombatHandler>();
        damageHandler.OnDeathEvent += OnDeath;
        damageHandler.OnDamageEvent += OnDamage;
        damageHandler.OnHealEvent += OnHeal;
    }

    public virtual void OnEnable()
    {
        
    }

    public virtual void OnDeath()
    {
        ScoreManager.Instance.AddScore(data.score); 
        Destroy(gameObject);
    }

    public virtual void OnDamage()
    {
    }

    public virtual void OnHeal()
    {
    }
}