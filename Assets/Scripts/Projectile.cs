using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Vector2 projectileSize;
    [SerializeField] float speed;
    [SerializeField] int damage;

    SoldierSide projectileSide;

    Vector3 velocity;
    //Collider2D target;

    private void Start()
    {
        //projectileSize = GetComponentInChildren<Transform>().lossyScale;
        Destroy(this.gameObject, 7);
    }

    void FixedUpdate()
    {
        // movement
        velocity = Vector3.right * Time.deltaTime * speed * ((projectileSide == SoldierSide.Left)? 1: -1); 
        transform.Translate(velocity, Space.World);

        //// detecting target
        //target = Physics2D.OverlapBox(transform.position, projectileSize, 0);
        //Debug.Log(target);

        //if(target != null)
        //{
        //    if (target.gameObject.GetComponent<Soldier>().IsSoldierAlive())
        //    {
        //        target.gameObject.GetComponent<Soldier>().TakeDamageAndKnockback(damage);
        //        Destroy(this.gameObject);
        //    }
        //}
    }

    public void SetProjectileSide(SoldierSide side)
    {
        projectileSide = side;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided: " + collision.gameObject.name);
        Debug.Log(projectileSide);

        if (collision.gameObject.GetComponent<Soldier>().P_SoldierSide != projectileSide)
        {
            collision.gameObject.GetComponent<Soldier>().TakeDamageAndKnockback(damage);
            Destroy(this.gameObject);
        }
    }

}
