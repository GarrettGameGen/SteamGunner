using UnityEngine;
using System;
using System.Collections.Generic;

//[CreateAssetMenu(fileName = "CharacterDataObject", menuName = "Game Data/CharacterDataObject")]
public abstract class CharacterDataObject : ScriptableObject
{

    public enum Faction
    {
        ally,
        enemy
    }

    //[NonSerialized]
    public Faction faction;
    public string characterName;
    public int maxHP;
}