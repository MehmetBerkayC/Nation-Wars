using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    Spawner spawner;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            spawner.SpawnSoldier(SpawnPositions.Left1);
        }
    }
}
