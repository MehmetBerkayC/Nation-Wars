using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Spawner _spawner;

    [SerializeField] FactionInfoHolder _factionInformation;

    // PlayerInfos
    SpawnPositions _spawnPositionLeft = SpawnPositions.Left5;
    SpawnPositions _spawnPositionRight = SpawnPositions.Right5;

    int _soldierIndexLeft = 0;
    int _soldierIndexRight = 0;

    [SerializeField] Kingdoms _kingdomLeft = Kingdoms.NotSelected;
    [SerializeField] Kingdoms _kingdomRight = Kingdoms.NotSelected;

    GameObject[] _soldierOfLeft;
    GameObject[] _soldierOfRight;

    int _tempFix = 0;

    private void Update()
    {
        if(_kingdomLeft != Kingdoms.NotSelected && _kingdomRight != Kingdoms.NotSelected)
        {
            if(_tempFix == 0)
            {
                _soldierOfLeft = _factionInformation.GetSoldierArray(_kingdomLeft);
                _soldierOfRight = _factionInformation.GetSoldierArray(_kingdomRight);
                _tempFix++;
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
        if (_kingdomLeft == Kingdoms.NotSelected)
        {
            Debug.Log("Kingdom Selection for Left Side not implemented");
        }
        else if (_kingdomLeft != Kingdoms.NotSelected && _kingdomRight == Kingdoms.NotSelected)
        {
            Debug.Log("Kingdom Selection for Right Side not implemented");
        }
    }

    void SpawnSelection()
    {
        // Left Side
        if (Input.GetMouseButtonDown(0))
        {
            _spawner.SpawnSoldierLeftSide(_soldierOfLeft, _soldierIndexLeft, _spawnPositionLeft);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Before Left: " + _spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(1);
            Debug.Log("After Left: " + _spawnPositionLeft.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Before Left: " + _spawnPositionLeft.ToString());
            ChangeSpawnPositionForLeft(-1);
            Debug.Log("After Left: " + _spawnPositionLeft.ToString());
        }

        // Right Side
        if (Input.GetMouseButtonDown(1))
        {
            _spawner.SpawnSoldierRightSide(_soldierOfRight, _soldierIndexRight, _spawnPositionRight);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Before Right: " + _spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(1);
            Debug.Log("After Right: " + _spawnPositionRight.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Before Right: " + _spawnPositionRight.ToString());
            ChangeSpawnPositionForRight(-1);
            Debug.Log("After Right: " + _spawnPositionRight.ToString());
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
        _spawnPositionLeft += increment;
        if(_spawnPositionLeft <= SpawnPositions.Left1)
        {
            _spawnPositionLeft = SpawnPositions.Left1;
        }else if(_spawnPositionLeft >= SpawnPositions.Left9)
        {
            _spawnPositionLeft = SpawnPositions.Left9;
        }
    }
    
    void ChangeSpawnPositionForRight(int increment)
    {
        _spawnPositionRight += increment;
        if(_spawnPositionRight <= SpawnPositions.Right1)
        {
            _spawnPositionRight = SpawnPositions.Right1;
        }else if(_spawnPositionRight >= SpawnPositions.Right9)
        {
            _spawnPositionRight = SpawnPositions.Right9;
        }
    }

    void ChangeSoldierIndexForLeft(int increment)
    {
        _soldierIndexLeft += increment;

        if (_soldierIndexLeft >= _soldierOfLeft.Length - 1)
        {
            _soldierIndexLeft = _soldierOfLeft.Length - 1;
        }
        else if (_soldierIndexLeft <= 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            _soldierIndexLeft = 0;
        }
    }

    void ChangeSoldierIndexForRight(int increment)
    {
        _soldierIndexRight += increment;

        if (_soldierIndexRight >= _soldierOfRight.Length - 1)
        {
            _soldierIndexRight = _soldierOfRight.Length - 1;
        }
        else if (_soldierIndexRight <= 0) // soldierPrefabs should be an array(change this if became a list/dictionary)
        {
            _soldierIndexRight = 0;
        }
    }
}