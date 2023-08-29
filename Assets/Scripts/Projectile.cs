using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 _projectileSize;
    [SerializeField] float _speed;
    [SerializeField] float _knockback = 0;
    [SerializeField] int _damage;

    SoldierSide _projectileSide;

    Vector3 _velocity;
    Collider2D _collision;

    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        // movement
        _velocity = Vector3.right * Time.deltaTime * _speed * ((_projectileSide == SoldierSide.Left)? 1: -1); 
        transform.Translate(_velocity, Space.World);

        // Detecting target
        _collision = Physics2D.OverlapBox(transform.position, _projectileSize, 0);

        if (_collision != null )
        {
            _collision.gameObject.TryGetComponent(out Soldier _target);
            if (_target.SoldierSide != _projectileSide)
            {
                _target.TakeDamageAndKnockback(_damage, _knockback);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetProjectileSide(SoldierSide side)
    {
        _projectileSide = side;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _projectileSize);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collided: " + collision.gameObject.name);
    //    Debug.Log(projectileSide);

    //    if (collision.gameObject.GetComponent<Soldier>().P_SoldierSide != projectileSide)
    //    {
    //        collision.gameObject.GetComponent<Soldier>().TakeDamageAndKnockback(damage);
    //        Destroy(this.gameObject);
    //    }
    //}

}
