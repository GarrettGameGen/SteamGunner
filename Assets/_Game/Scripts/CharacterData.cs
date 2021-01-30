using UnityEngine;

[RequireComponent(typeof(CombatHandler))]
public class CharacterData : MonoBehaviour
{
    public CharacterDataObject dataObject;

    [System.NonSerialized] public CharacterDataObject.Faction faction;
    [System.NonSerialized] public string characterName;
    [System.NonSerialized] public int maxHP;
    [System.NonSerialized] public int hp;
    [System.NonSerialized] public int score;
}