using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathBox : MonoBehaviour
{
    private bool _isTriggered = false;

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered) return;
        Mario mario;
        if ((mario = collision.GetComponent<Mario>()))
        {
            _isTriggered = true;
            mario.Dead();
        }
    }
}
