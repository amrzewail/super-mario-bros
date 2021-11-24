using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireBullet : MonoBehaviour, IProjectile
{

    [SerializeField] float moveSpeed;
    [SerializeField] float hopVelocityY;

    public int direction { get; set; }

    private Rigidbody2D rb;

    internal void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();

        rb.velocity = direction * Vector2.right * moveSpeed;
    }


    public void Hop()
    {
        rb.velocity = new Vector2(rb.velocity.x, hopVelocityY);
    }
    
    public void Explode()
    {
        GetComponentInChildren<Damage>().enabled = false;
        GetComponentInChildren<Animator>().Play("Explode");
        GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled = false);
        Destroy(this.gameObject, 0.433f);
    }

}
