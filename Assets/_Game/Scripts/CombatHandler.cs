using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatHandler : MonoBehaviour
{
    //[System.NonSerialized]
    public event System.Action OnDeathEvent;
    public event System.Action OnDamageEvent;
    public event System.Action OnHealEvent;
    private CharacterData data;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
    }

    void Start()
    {
        data.faction = data.dataObject.faction;

        data.characterName = data.dataObject.characterName;

        if (data.dataObject.maxHP < 1)
        {
            data.dataObject.maxHP = 1;
        }
        data.hp = data.maxHP = data.dataObject.maxHP;
    }

    private void Update()
    {
    }

    public void TakeDamage(int damage)
    {
        data.hp -= damage;
        data.hp = Mathf.Clamp(data.hp, 0, data.dataObject.maxHP);
        if (data.hp <= 0)
        {
            OnDeathEvent.Invoke();
        }
        else
        {
            OnDamageEvent.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        data.hp += healAmount;
        data.hp = Mathf.Clamp(data.hp, 0, data.dataObject.maxHP);
        OnHealEvent.Invoke();
    }
}