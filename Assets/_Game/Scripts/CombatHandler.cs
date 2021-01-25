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
    public int healthBarLength = 345;
    public int atbBarLength = 348;
    private float damagedHP;
    public float damageSpeed;
    private float damageDelay = 0;
    public float damageDelayTime;
    RectTransform splash;

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
        data.maxHP = data.dataObject.maxHP;

        data.hp = data.dataObject.hp;

        SetHealthBar();
        damagedHP = data.hp;
    }

    private void Update()
    {
        if (damagedHP != data.hp)
        {
            damageDelay += Time.deltaTime;
            if (damageDelay >= damageDelayTime)
            {
                damagedHP -= damageSpeed;
                SetDamageBar();
                if (damagedHP - data.hp < 1)
                {
                    damagedHP = data.hp;
                    damageDelay = 0;
                    splash.GetComponent<Image>().enabled = false;
                }
            }
        }
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
        SetHealthBar();
    }

    public void Heal(int healAmount)
    {
        data.hp += healAmount;
        data.hp = Mathf.Clamp(data.hp, 0, data.dataObject.maxHP);
        OnHealEvent.Invoke();
    }

    private void SetHealthBar()
    {
    }

    private void SetDamageBar()
    {
    }
}