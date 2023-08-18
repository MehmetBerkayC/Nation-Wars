using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Kingdoms
{// Name        -- Tier      -    HP     -   DMG      -  Speed    -  Range  -  Notes
    Human,      // Mid Tier  -  Mid HP   -  Mid       -  Mid      -  Mixed  -  Mid overall and no magic
    Elf,        // High Tier -  Mid HP   -  High      -  Mid      -  Mixed  -  High Ranged DPS -> archer and mage?
    Orc,        // High Tier -  High HP  -  High      -  Slow     -  Melee  -  AOE Melee       -> all melee?
    Halfling,   // Low Tier  -  Low HP   -  Low-Mid   -  Fast     -  Melee  -  Single-Target   -> wtf?
    Demon       // High Tier -  Mid-High -  High-Mid  -  Mid-Slow -  Mixed  -  AOE Magic       -> all ranged magic?
}

public enum SoldierTypes
{
    Swordsman,  // Mid-High HP,         Mid Dmg,          Mid Speed,      Low Range,        AOE Melee  
    Spearman,   // Low HP,              High Dmg,         Low Speed,      Mid Range,        Melee
    Knight,     // High HP,             Mid Dmg,          Mid Speed,      Mid-Low Range,    Melee -> Sword Shield and Armor
    Cleric,     // Mid-Low HP,          Mid Dmg,          Mid Speed,      Mid-High Range,   AOE Melee -> Probably Healer or Melee Mace user with dmg/heal spells
    Spy,        // Very Low HP,         Nearly No Dmg,    Fastest Speed,  Low Range,        Melee
    Mage,       // Very Low Hp,         Highest Dmg,      Fast Speed,     High Range,       AOE Ranged -> Magic
    Archer,     // Low Hp,              Mid Dmg,          Fast Speed,     High Range,       Ranged
}
public class SoldierPrefabManagement 
{
    // decides which kingdom/race and class the current soldier is in

    // Current soldier's info
    SoldierTypes attachedSoldierType;
    Kingdoms attachedKingdom;

    public void setKingdomAndSoldierType(Kingdoms kingdom, SoldierTypes soldierType)
    {
        attachedKingdom = kingdom;
        attachedSoldierType = soldierType;
    }
}
/*  Notes on how to do this shit
 *  chose from below:
 *  May make a field for each prefab and match the types to those, may be hard
 *  May make a prefab array for every kingdom and customise them on inspector
 * 
 */
