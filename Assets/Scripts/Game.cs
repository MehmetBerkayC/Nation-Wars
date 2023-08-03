using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;

    SpawnPositions spawnPositionLeft = SpawnPositions.Left5;
    SpawnPositions spawnPositionRight = SpawnPositions.Right5;

    private void Update()
    {
        // Left side controls
        if (Input.GetMouseButtonDown(0))
        {
            spawner.SpawnSoldier(spawnPositionLeft);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Before Left: " + spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(1);
            Debug.Log("After Left: " + spawnPositionLeft.ToString());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Before Left: " + spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(-1);
            Debug.Log("After Left: " + spawnPositionLeft.ToString());
        }

        // Right side controls
        if (Input.GetMouseButtonDown(1))
        {
            spawner.SpawnSoldier(spawnPositionRight);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Before Right: " + spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(1);
            Debug.Log("After Right: " + spawnPositionRight.ToString());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Before Right: " + spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(-1);
            Debug.Log("After Right: " + spawnPositionRight.ToString());
        }
    }

    void ChangeSpawnPositionForLeft(int increment)
    {
        spawnPositionLeft = spawnPositionLeft + increment;
        if(spawnPositionLeft <= SpawnPositions.Left1)
        {
            spawnPositionLeft = SpawnPositions.Left1;
        }else if(spawnPositionLeft >= SpawnPositions.Left9)
        {
            spawnPositionLeft = SpawnPositions.Left9;
        }
    }
    
    void ChangeSpawnPositionForRight(int increment)
    {
        spawnPositionRight = spawnPositionRight + increment;
        if(spawnPositionRight <= SpawnPositions.Right1)
        {
            spawnPositionRight = SpawnPositions.Right1;
        }else if(spawnPositionRight >= SpawnPositions.Right9)
        {
            spawnPositionRight = SpawnPositions.Right9;
        }
    }
}
