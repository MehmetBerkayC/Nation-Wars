using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int damage;
    [SerializeField] float knockback;

    [SerializeField] float speed = 1f;
    
    [SerializeField] float range = 0.5f;

    Dictionary<bool, Vector2> movingDirection = new Dictionary<bool, Vector2>
    {
        {true, Vector2.right},
        {false, Vector2.left}
    };

    enum SoldierType
    {
        Melee,
        AOEMelee,
        Ranged,
        AOERanged,
    }

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

    SoldierType soldierType;
    States soldierState;
    Vector2 direction;

    Vector3 boxCenter;
    Vector3 boxHalfExtends;

    Collider[] collisions;
    GameObject target;

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

            if (soldierState == States.Attacking)
            {
                if (target != null && target.GetComponent<Soldier>().IsSoldierAlive())
                { // target is alive
                    float distance = (target.transform.position.x - transform.position.x);
                    if (distance > range)
                    { // Get closer if out of range
                        transform.Translate(direction * speed * Time.deltaTime, Space.World);
                    }

                    // Deal Damage
                    target.GetComponent<Soldier>().TakeDamageAndKnockback(damage, knockback);
                }
                else if (target != null && !target.GetComponent<Soldier>().IsSoldierAlive())
                { // target is dead
                    soldierState = States.Searching;
                }
      
                // do nothing
            } 
            else if (soldierState == States.Searching)
            {
                // Move
                transform.Translate(direction * speed * Time.deltaTime, Space.World);

                // Try to Detect enemy
                Vector3 currentPosition = transform.position;
                currentPosition.x += (soldierSide == SoldierSide.Left ? range: -range ) / 2 ; 
                boxCenter = currentPosition + (soldierSide == SoldierSide.Left ? Vector3.right : Vector3.left);
                boxHalfExtends = new Vector2(range, .5f);
                collisions = Physics.OverlapBox(boxCenter, boxHalfExtends);

                // work on progress
                if (collisions.Length > 0) // Check if it has soldier script**
                {
                    soldierState = States.Attacking;

                    /// working on this
                    if (true) // check the soldiertype and figure out how many things will get damaged
                    {

                    }

                    target = collisions[0].gameObject;
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public bool IsSoldierAlive()
    {
        return healthSystem.IsAlive();
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
    void TakeDamageAndKnockback(int damage, float knockback = 0.2f)
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
