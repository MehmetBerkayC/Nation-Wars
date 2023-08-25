using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoldierSide
{
    Left,
    Right
};

public class Soldier : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] int _damage;
    [SerializeField] float _knockback;

    [SerializeField] float _speed = 1f;
    [SerializeField] float _range = 0.5f;
    [SerializeField] float _attackRate = 0.5f;

    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Transform _projectileSpawnPosition;

    Dictionary<bool, Vector2> _movingDirection = new Dictionary<bool, Vector2>
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

    public SoldierSide SoldierSide
    {
        get
        {
            return _soldierSide;
        }
        private set
        {
            _soldierSide = value;
        }
    }

    Dictionary<SoldierSide, float> _enemyBasePositionX = new Dictionary<SoldierSide, float>
    {
        {SoldierSide.Left, 9.5f},
        {SoldierSide.Right, -9.5f},
    };

    enum States
    {
        Searching,
        Attacking,
    }

    [SerializeField] SoldierType _soldierType;

    SoldierSide _soldierSide;
    States _soldierState;
    Vector2 _direction;

    Vector3 _boxCenter;
    Vector3 _boxHalfExtends;

    Collider2D[] _collisions;

    List<GameObject> _validTargets = new List<GameObject>();

    HealthSystem _healthSystem;

    float _attackTimer = 0;

    
    private void OnEnable()
    {
        _healthSystem = new HealthSystem(_maxHealth);

        if(transform.position.x < 0f) // Left Side
        {
            _movingDirection.TryGetValue(true, out _direction);
            SoldierSide = SoldierSide.Left;
        }
        else if(transform.position.x > 0f) // Right Side
        {
            _movingDirection.TryGetValue(false, out _direction);
            SoldierSide = SoldierSide.Right;
        }
        else
        {
            Debug.LogError("Incorrect spawn position, object will be destroyed!");
            Destroy(this.gameObject);
        }

        CheckAndFlipCharacter();
    }

    void FixedUpdate()
    {
        if (_healthSystem.IsAlive())
        {
            _attackTimer += Time.deltaTime;
            CheckIfInEnemyBase();

            if (_soldierState == States.Attacking)
            {
                if (!CheckIsTargetArrayEmpty() /*|| CheckInRange()*/)
                {
                    if(_soldierType == SoldierType.Melee)
                    {
                        AttackTargets();
                    }
                    else if (_soldierType == SoldierType.AOEMelee)
                    {
                        AttackTargetsAOE();
                    }
                    else if (_soldierType == SoldierType.Ranged)
                    {
                        AttackTargetsProjectile();
                    }
                }
                else
                {
                    _soldierState = States.Searching;
                }
            } 
            else if (_soldierState == States.Searching)
            {
                // Move
                transform.Translate(_direction * _speed * Time.deltaTime, Space.World);

                DetectEnemies();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //bool CheckInRange()
    //{
        
    //}

    void DetectEnemies()
    {
        // Try to Detect enemy
        Vector2 currentPosition = transform.position;
        currentPosition.x += (SoldierSide == SoldierSide.Left ? _range : -_range) / 2;
        _boxCenter = currentPosition + (SoldierSide == SoldierSide.Left ? Vector2.right : Vector2.left) / 2;
        _boxHalfExtends = new Vector2(_range, .75f);

        _collisions = Physics2D.OverlapBoxAll(_boxCenter, _boxHalfExtends, 0);

        Dictionary<int, int> validTargetIndexes = new Dictionary<int, int>();

        // if enemy present switch to attack and perform the first
        if (_collisions.Length > 0)
        {
            //Debug.Log("Collision Detected, Array Length:" + collisions.Length);

            for (int i = 0; i < _collisions.Length; i++)
            {
                if (_collisions[i].gameObject.GetComponent<Soldier>().IsSoldierAlive() && _collisions[i].gameObject.GetComponent<Soldier>().SoldierSide != SoldierSide) // if enemy
                {
                    _validTargets.Add(_collisions[i].gameObject);
                }
            }

            if (_validTargets.Count > 0)
            {
                // Start Attacking
                _soldierState = States.Attacking;
            }
        }
    }

    void AttackTargets()
    {
        foreach (GameObject target in _validTargets)
        {
            if (target != null && target.TryGetComponent(out Soldier soldier))
            {
                if (soldier.IsSoldierAlive() && _attackTimer >= _attackRate)
                { // target is alive
                    
                    // Deal Damage
                    soldier.TakeDamageAndKnockback(_damage, _knockback);

                    #region Probably not necessary, distance check after knockback
                    //float distance = (target.transform.position.x - transform.position.x);
                    //if (distance > range)
                    //{ // Get closer if out of range
                    //    transform.Translate(direction * speed * Time.deltaTime, Space.World);
                    //}
                    #endregion
                    
                    _attackTimer = 0f;
                }
            }
        }
    }
    void AttackTargetsAOE()
    {
        if (_attackTimer >= _attackRate)
        {
            foreach (GameObject target in _validTargets)
            {
                if (target != null && target.TryGetComponent(out Soldier soldier))
                {
                    if (soldier.IsSoldierAlive())
                    { // target is alive
                      // Deal Damage

                        soldier.TakeDamageAndKnockback(_damage, _knockback);

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
            _attackTimer = 0f;
        }
    }

    void AttackTargetsProjectile()
    {
        foreach (GameObject target in _validTargets)
        {
            if (target != null && target.TryGetComponent(out Soldier soldier))
            {
                if (soldier.IsSoldierAlive() && _attackTimer >= _attackRate)
                {
                    Projectile projectile = Instantiate(_projectilePrefab, _projectileSpawnPosition.position, transform.rotation).GetComponent<Projectile>();
                    projectile.SetProjectileSide(_soldierSide);
                    _attackTimer = 0f;
                }
            }
        }
    }

    void CheckAndFlipCharacter()
    {
        if(_soldierSide == SoldierSide.Right)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    bool CheckIsTargetArrayEmpty()
    {
        int validCount = 0;

        foreach (GameObject target in _validTargets)
        {
            validCount += (target == null) ? 0 : 1;
        }

        return validCount > 0 ? false: true;
    }

    public bool IsSoldierAlive()
    {
        return _healthSystem.IsAlive();
    }

    void CheckIfInEnemyBase() // Positions ARE HARDCODED! 
    {
        _enemyBasePositionX.TryGetValue(SoldierSide, out float position);

        if (
            (SoldierSide == SoldierSide.Left && transform.position.x >= position) ||
            (SoldierSide == SoldierSide.Right && transform.position.x <= position)
            ) 
        {
            // Score point and destroy soldier
            _healthSystem.Kill();
        }
    }

    // Use this if knockback is implemented
    public void TakeDamageAndKnockback(int damage, float knockback = 0.2f)
    {
        _healthSystem.TakeDamage(damage);
        transform.position += new Vector3(SoldierSide == SoldierSide.Left ? -knockback : knockback, 0f, 0f); 

        //Debug.Log(gameObject.name + " Damage Taken: " + damage + " Current HP:" + healthSystem.GetHealth());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_boxCenter, _boxHalfExtends);
    }
}
