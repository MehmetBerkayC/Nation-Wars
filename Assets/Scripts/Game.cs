using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;

    [SerializeField] FactionInfoHolder management;

    // PlayerInfos
    SpawnPositions spawnPositionLeft = SpawnPositions.Left5;
    SpawnPositions spawnPositionRight = SpawnPositions.Right5;

    int soldierIndexLeft = 0;
    int soldierIndexRight = 0;

    [SerializeField] Kingdoms kingdomLeft = Kingdoms.NotSelected;
    [SerializeField] Kingdoms kingdomRight = Kingdoms.NotSelected;

    GameObject[] soldierOfLeft;
    GameObject[] soldierOfRight;

    int quickfix = 0;

    private void Update()
    {
        if(kingdomLeft != Kingdoms.NotSelected && kingdomRight != Kingdoms.NotSelected)
        {
            if(quickfix == 0)
            {
                soldierOfLeft = management.GetSoldierArray(kingdomLeft);
                soldierOfRight = management.GetSoldierArray(kingdomRight);
                quickfix++;
            }

            // Spawn Selection
            SpawnSelection();

            // Soldier Selection
            SoldierSelection();
        }
        else
        {
            KingdomSelection();
        }
    }


    void KingdomSelection()
    {
        if (kingdomLeft == Kingdoms.NotSelected)
        {
            Debug.Log("Kingdom Selection for Left Side not implemented");
        }
        else if (kingdomLeft != Kingdoms.NotSelected && kingdomRight == Kingdoms.NotSelected)
        {
            Debug.Log("Kingdom Selection for Right Side not implemented");
        }
    }

    void SpawnSelection()
    {
        // Left Side
        if (Input.GetMouseButtonDown(0))
        {
            spawner.SpawnSoldierLeftSide(soldierOfLeft, soldierIndexLeft, spawnPositionLeft);
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
            spawner.SpawnSoldierRightSide(soldierOfRight, soldierIndexRight, spawnPositionRight);
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

        if (soldierIndexLeft >= soldierOfLeft.Length - 1)
        {
            soldierIndexLeft = soldierOfLeft.Length - 1;
        }
        else if (soldierIndexLeft <= 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            soldierIndexLeft = 0;
        }
    }

    void ChangeSoldierIndexForRight(int increment)
    {
        soldierIndexRight += increment;

        if (soldierIndexRight >= soldierOfRight.Length - 1)
        {
            soldierIndexRight = soldierOfRight.Length - 1;
        }
        else if (soldierIndexRight <= 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            soldierIndexRight = 0;
        }
    }
}