using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int damage;

    [SerializeField] float range;

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
        {SoldierSide.Left, 9.5f},
        {SoldierSide.Right, -9.5f},
    };

    enum States
    {
        Searching,
        Attacking,
    }

    States soldierState;
    Vector2 direction;

    Vector3 boxCenter;
    Vector3 boxHalfExtends;

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

    void Update()
    {
        if (healthSystem.IsAlive())
        {
            CheckIfInEnemyBase();

            if(soldierState == States.Searching)
            {
                // Move
                transform.Translate(direction * Time.deltaTime, Space.World);

                // Detect enemy
                Vector3 currentPosition = transform.position;
                currentPosition.x += (soldierSide == SoldierSide.Left ? range: -range ) / 2 ; 
                boxCenter = currentPosition + (soldierSide == SoldierSide.Left ? Vector3.right : Vector3.left);
                boxHalfExtends = new Vector2(range, .5f);
                Collider[] collisions = Physics.OverlapBox(boxCenter, boxHalfExtends);

                if (collisions.Length > 0)
                {
                    soldierState = States.Attacking;
                }
            }
            else if (soldierState == States.Attacking)
            {
                // do nothing
            }
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

    // Use this if knockback is implemented
    void TakeDamageAndKnockback(int damage, float knockback)
    {
        healthSystem.TakeDamage(damage);
        transform.position += new Vector3(0f, soldierSide == SoldierSide.Left ? knockback : -knockback, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCenter, boxHalfExtends);
    }
}
