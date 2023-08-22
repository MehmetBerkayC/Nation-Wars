using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 projectileSize;
    [SerializeField] float speed;
    [SerializeField] int damage;

    int projectileSide;

    Vector3 velocity;
    Collider2D target;

    private void Start()
    {
        projectileSide = GetComponentInParent<Soldier>().P_SoldierSide == Soldier.SoldierSide.Left ? -1: 1;
        projectileSize = gameObject.GetComponentInChildren<Transform>().lossyScale;
        Destroy(this.gameObject, 7);
    }

    void FixedUpdate()
    {
        // movement
        velocity = Vector3.right * Time.deltaTime * speed * projectileSide; 
        transform.Translate(velocity, Space.World);

        // detecting target
        target = Physics2D.OverlapBox(transform.position, projectileSize, 0);
        Debug.Log(target);

        if(target != null)
        {
            if (target.gameObject.GetComponent<Soldier>().IsSoldierAlive())
            {
                target.gameObject.GetComponent<Soldier>().TakeDamageAndKnockback(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
