using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kingdoms
{// Name        -- Tier      -    HP     -   DMG      -  Speed    -  Range  -  Notes
    Human,      // Mid Tier  -  Mid HP   -  Mid       -  Mid      -  Mixed  -  Mid overall and no magic
    Elf,        // High Tier -  Mid HP   -  High      -  Mid      -  Mixed  -  High Ranged DPS -> archer and mage?
    Orc,        // High Tier -  High HP  -  High      -  Slow     -  Melee  -  AOE Melee       -> all melee?
    Halfling,   // Low Tier  -  Low HP   -  Low-Mid   -  Fast     -  Melee  -  Single-Target   -> wtf?
    Demon,      // High Tier -  Mid-High -  High-Mid  -  Mid-Slow -  Mixed  -  AOE Magic       -> all ranged magic?
    NotSelected
}

enum SoldierTypes
{
    Swordsman,  // Mid-High HP,         Mid Dmg,          Mid Speed,      Low Range,        AOE Melee  
    Spearman,   // Low HP,              High Dmg,         Low Speed,      Mid Range,        Melee
    Knight,     // High HP,             Mid Dmg,          Mid Speed,      Mid-Low Range,    Melee -> Sword Shield and Armor
    Cleric,     // Mid-Low HP,          Mid Dmg,          Mid Speed,      Mid-High Range,   AOE Melee -> Probably Healer or Melee Mace user with dmg/heal spells
    Spy,        // Very Low HP,         Nearly No Dmg,    Fastest Speed,  Low Range,        Melee
    Mage,       // Very Low Hp,         Highest Dmg,      Fast Speed,     High Range,       AOE Ranged -> Magic
    Archer,     // Low Hp,              Mid Dmg,          Fast Speed,     High Range,       Ranged
}

// To learn about Scriptable Objects, make sure to write the same class using them
public class FactionInfoHolder : MonoBehaviour
{
    [SerializeField] GameObject[] HumanKingdom;
    [SerializeField] GameObject[] ElfKingdom;
    [SerializeField] GameObject[] OrcKingdom;
    [SerializeField] GameObject[] HalflingKingdom;
    [SerializeField] GameObject[] DemonKingdom;

    GameObject[] currentSoldierGroup;

    void FindSoldierGroupofKingdom(Kingdoms kingdomName)
    {
        switch (kingdomName)
        {
            case Kingdoms.Human:
                currentSoldierGroup = HumanKingdom;
                break;
            case Kingdoms.Elf:
                currentSoldierGroup = ElfKingdom;
                break;
            case Kingdoms.Orc:
                currentSoldierGroup = OrcKingdom;
                break;
            case Kingdoms.Halfling:
                currentSoldierGroup = HalflingKingdom;
                break;
            case Kingdoms.Demon:
                currentSoldierGroup = DemonKingdom;
                break;
            case Kingdoms.NotSelected:
                Debug.Log("Kingdom is Not Selected");
                break;
            default:
                Debug.LogError("Cannot Find Soldier Group of: " + kingdomName);
                break;
        }
    }

    public GameObject[] GetSoldierArray(Kingdoms kingdom)
    {
        FindSoldierGroupofKingdom(kingdom);
        return currentSoldierGroup;
    }

}
