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

    public enum SoldierSide
    {
        Left,
        Right
    };

    public SoldierSide P_SoldierSide
    {
        get
        {
            return soldierSide;
        }
        private set
        {
            soldierSide = value;
        }
    }

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

    [SerializeField] SoldierType soldierType;

    SoldierSide soldierSide;
    States soldierState;
    Vector2 direction;

    Vector3 boxCenter;
    Vector3 boxHalfExtends;

    Collider2D[] collisions;

    List<GameObject> validTargets = new List<GameObject>();

    HealthSystem healthSystem;

    private void OnEnable()
    {
        healthSystem = new HealthSystem(maxHealth);

        if(transform.position.x < 0f) // Left Side
        {
            movingDirection.TryGetValue(true, out direction);
            P_SoldierSide = SoldierSide.Left;
        }
        else if(transform.position.x > 0f) // Right Side
        {
            movingDirection.TryGetValue(false, out direction);
            P_SoldierSide = SoldierSide.Right;
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
                if (!CheckIsTargetArrayEmpty())
                {
                    AttackTargets();
                }
                else
                {
                    soldierState = States.Searching;
                }
            } 
            else if (soldierState == States.Searching)
            {
                // Move
                transform.Translate(direction * speed * Time.deltaTime, Space.World);

                DetectEnemies();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void DetectEnemies()
    {
        // Try to Detect enemy
        Vector2 currentPosition = transform.position;
        currentPosition.x += (P_SoldierSide == SoldierSide.Left ? range : -range) / 2;
        boxCenter = currentPosition + (P_SoldierSide == SoldierSide.Left ? Vector2.right : Vector2.left);
        boxHalfExtends = new Vector2(range, .5f);

        // checking layer for enemies
        collisions = Physics2D.OverlapBoxAll(boxCenter, boxHalfExtends, 0);

        Dictionary<int, int> validTargetIndexes = new Dictionary<int, int>();

        // if enemy present switch to attack and perform the first
        if (collisions.Length > 0)
        {
            Debug.Log("Collision Detected, Array Length:" + collisions.Length);

            for (int i = 0; i < collisions.Length; i++)
            {
                if(collisions[i].gameObject.GetComponent<Soldier>().IsSoldierAlive() && collisions[i].gameObject.GetComponent<Soldier>().P_SoldierSide != P_SoldierSide) // if enemy
                {
                    validTargets.Add(collisions[i].gameObject);
                }
            }

            if (validTargets.Count > 0)
            {
                // Start Attacking
                soldierState = States.Attacking;
                AttackTargets();
            }
        }
    }

    void AttackTargets()
    {
        foreach (GameObject target in validTargets)
        {
            if (target != null)
            {
                if (target.GetComponent<Soldier>().IsSoldierAlive())
                { // target is alive
                  // Deal Damage
                    target.GetComponent<Soldier>().TakeDamageAndKnockback(damage, knockback);
                    
                    #region Probably not necessary, distance check after knockback
                    //float distance = (target.transform.position.x - transform.position.x);
                    //if (distance > range)
                    //{ // Get closer if out of range
                    //    transform.Translate(direction * speed * Time.deltaTime, Space.World);
                    //}
                    #endregion
                }
            }
        }
    }

    bool CheckIsTargetArrayEmpty()
    {
        int validCount = 0;

        foreach (GameObject target in validTargets)
        {
            validCount += (target == null) ? 0 : 1;
        }

        return validCount > 0 ? false: true;
    }

    public bool IsSoldierAlive()
    {
        return healthSystem.IsAlive();
    }

    void CheckIfInEnemyBase() // Positions ARE HARDCODED! 
    {
        enemyBasePositionX.TryGetValue(P_SoldierSide, out float position);

        if (
            (P_SoldierSide == SoldierSide.Left && transform.position.x >= position) ||
            (P_SoldierSide == SoldierSide.Right && transform.position.x <= position)
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
        transform.position += new Vector3(0f, P_SoldierSide == SoldierSide.Left ? knockback : -knockback, 0f);

        Debug.Log(gameObject.name + " Damage Taken: " + damage + " Current HP:" + healthSystem.GetHealth());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCenter, boxHalfExtends);
    }
}
