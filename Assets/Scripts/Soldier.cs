using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int damage;

    Dictionary<bool, Vector2> movingDirection = new Dictionary<bool, Vector2>
    {
        {true, Vector2.right},
        {false, Vector2.left}
    };

    enum SoldierSide
    {
        Left,
        Right
    };

    SoldierSide soldierSide;

    Dictionary<SoldierSide, float> enemyBasePositionX = new Dictionary<SoldierSide, float>
    {
        {SoldierSide.Left, 7.5f},
        {SoldierSide.Right, -7.5f},
    };

    Vector2 direction;

    HealthSystem healthSystem;

    private void OnEnable()
    {
        healthSystem = new HealthSystem(maxHealth);

        if(transform.position.x < 0f) // Left Side
        {
            movingDirection.TryGetValue(true, out direction);
            soldierSide = SoldierSide.Left;
        }
        else if(transform.position.x > 0f) // Right Side
        {
            movingDirection.TryGetValue(false, out direction);
            soldierSide = SoldierSide.Right;
        }
        else
        {
            Debug.LogError("Incorrect spawn position, object will be destroyed!");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.IsAlive())
        {
            CheckIfInEnemyBase();
            transform.Translate(direction * Time.deltaTime, Space.World);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void CheckIfInEnemyBase()
    {
        enemyBasePositionX.TryGetValue(soldierSide, out float position);

        if (
            (soldierSide == SoldierSide.Left && transform.position.x >= position) ||
            (soldierSide == SoldierSide.Right && transform.position.x <= position)
            ) 
        {
            // Score point and destroy soldier
            healthSystem.Kill();
        }
    }
}
