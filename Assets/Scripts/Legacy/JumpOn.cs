using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpOn : MonoBehaviour
{
    [SerializeField] UnityEvent<Player> OnJump;
    [SerializeField] float height;

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if(collision.transform.position.y >= transform.position.y + height)
            {
                OnJump?.Invoke(collision.GetComponent<Player>());
            }
        }
    }

    internal void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * height);
    }
}
