using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpHit : MonoBehaviour
{
    [SerializeField] UnityEvent<Player> OnHit;
    [SerializeField] float height;

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if(collision.transform.position.y <= transform.position.y + height)
            {
                OnHit?.Invoke(collision.GetComponent<Player>());
            }
        }
    }

    internal void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * height);
    }
}
