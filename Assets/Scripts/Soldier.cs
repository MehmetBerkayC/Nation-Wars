using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    Dictionary<bool, Vector2> movingDirection = new Dictionary<bool, Vector2>
    {
        {true, Vector2.right},
        {false, Vector2.left}
    };

    bool isAlive;

    Vector2 direction;

    private void OnEnable()
    {
        isAlive = true;

        if(transform.position.x < 0f) // Left Side
        {
            movingDirection.TryGetValue(false, out direction);
        }else if(transform.position.x > 0f) // Right Side
        {
            movingDirection.TryGetValue(true, out direction);
        }
        else
        {
            Debug.LogError("Incorrect Spawn Position");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.Translate(direction, Space.World);
        }
    }
}
