using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;

    SpawnPositions spawnPositionLeft = SpawnPositions.Left5;
    SpawnPositions spawnPositionRight = SpawnPositions.Right5;

    int soldierIndexLeft = 0;
    int soldierIndexRight = 0;

    private void Update()
    {
        // Spawn Selection
        SpawnSelection();

        // Soldier Selection
        SoldierSelection();
    }

    void SpawnSelection()
    {
        // Left Side
        if (Input.GetMouseButtonDown(0))
        {
            spawner.SpawnSoldierLeftSide(soldierIndexLeft, spawnPositionLeft);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Before Left: " + spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(1);
            Debug.Log("After Left: " + spawnPositionLeft.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Before Left: " + spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(-1);
            Debug.Log("After Left: " + spawnPositionLeft.ToString());
        }

        // Right Side
        if (Input.GetMouseButtonDown(1))
        {
            spawner.SpawnSoldierRightSide(soldierIndexRight, spawnPositionRight);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Before Right: " + spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(1);
            Debug.Log("After Right: " + spawnPositionRight.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Before Right: " + spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(-1);
            Debug.Log("After Right: " + spawnPositionRight.ToString());
        }
    }

    void SoldierSelection()
    {
        // Left Side
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeSoldierIndexForLeft(1);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSoldierIndexForLeft(-1);
        }

        // Right Side
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSoldierIndexForRight(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSoldierIndexForRight(-1);
        }
    }

    void ChangeSpawnPositionForLeft(int increment)
    {
        spawnPositionLeft += increment;
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
        spawnPositionRight += increment;
        if(spawnPositionRight <= SpawnPositions.Right1)
        {
            spawnPositionRight = SpawnPositions.Right1;
        }else if(spawnPositionRight >= SpawnPositions.Right9)
        {
            spawnPositionRight = SpawnPositions.Right9;
        }
    }

    void ChangeSoldierIndexForLeft(int increment)
    {
        soldierIndexLeft += increment;

        if (soldierIndexLeft > spawner.soldierPrefabs.Length)
        {
            soldierIndexLeft = spawner.soldierPrefabs.Length;
        }
        else if(soldierIndexLeft < 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            soldierIndexLeft = 0;
        }
    }

    void ChangeSoldierIndexForRight(int increment)
    {
        soldierIndexRight += increment;

        if (soldierIndexRight > spawner.soldierPrefabs.Length)
        {
            soldierIndexRight = spawner.soldierPrefabs.Length;
        }
        else if(soldierIndexRight < 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            soldierIndexRight = 0;
        }
    }
}