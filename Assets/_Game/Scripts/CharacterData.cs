using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CombatHandler))]
public class CharacterData : MonoBehaviour
{
    public CharacterDataObject dataObject;

    public CharacterDataObject.Faction faction;
    public string characterName;
    public int maxHP;
    public int hp;
}