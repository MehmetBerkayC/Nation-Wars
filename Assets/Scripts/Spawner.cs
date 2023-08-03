using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPositions
{
    Left1,
    Left2,
    Left3,
    Left4,
    Left5,
    Left6,
    Left7,
    Left8,
    Left9,
    Right1,
    Right2,
    Right3,
    Right4,
    Right5,
    Right6,
    Right7,
    Right8,
    Right9
}

public class Spawner : MonoBehaviour
{

    Dictionary<SpawnPositions, Vector2> spawnPositions = new Dictionary<SpawnPositions, Vector2> {
        {SpawnPositions.Left1, new Vector2(-7.5f, 4)},
        {SpawnPositions.Left2, new Vector2(-7.5f, 3)},
        {SpawnPositions.Left3, new Vector2(-7.5f, 2)},
        {SpawnPositions.Left4, new Vector2(-7.5f, 1)},
        {SpawnPositions.Left5, new Vector2(-7.5f, 0)},
        {SpawnPositions.Left6, new Vector2(-7.5f,-1)},
        {SpawnPositions.Left7, new Vector2(-7.5f,-2)},
        {SpawnPositions.Left8, new Vector2(-7.5f,-3)},
        {SpawnPositions.Left9, new Vector2(-7.5f,-4)},
        {SpawnPositions.Right1, new Vector2(7.5f, 4)},
        {SpawnPositions.Right2, new Vector2(7.5f, 3)},
        {SpawnPositions.Right3, new Vector2(7.5f, 2)},
        {SpawnPositions.Right4, new Vector2(7.5f, 1)},
        {SpawnPositions.Right5, new Vector2(7.5f, 0)},
        {SpawnPositions.Right6, new Vector2(7.5f,-1)},
        {SpawnPositions.Right7, new Vector2(7.5f,-2)},
        {SpawnPositions.Right8, new Vector2(7.5f,-3)},
        {SpawnPositions.Right9, new Vector2(7.5f,-4)},
    };
    
    [SerializeField] GameObject soldierPrefab;
    public void SpawnSoldier(/*Takes Soldier Type?*/ SpawnPositions position) // Soldier Type and Pos
    {
        spawnPositions.TryGetValue(position, out Vector2 spawnPos); // Get spawn position

        Debug.Log(spawnPos);  //Spawns Soldier in said position
        Instantiate(soldierPrefab, spawnPos, Quaternion.identity);
    }
}
