using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlower : MonoBehaviour
{
    private bool _isComplete = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Mario mario;
        if (!_isComplete && (mario = collision.GetComponent<Mario>()))
        {
            SoundEvents.Play(Sounds.PowerUp);

            mario.FireUp();
            Game.Instance.score += 1000;
            gameObject.SetActive(false);
            _isComplete = true;
        }
    }
}
