using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOnReflex : MonoBehaviour
{
    public float force = 1;

    public void Callback(Player player)
    {
        var rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
