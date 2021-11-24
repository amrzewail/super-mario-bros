using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableCoin : MonoBehaviour
{

    private bool _isPicked = false;

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPicked) return;

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {

            SoundEvents.Play(Sounds.Coin);
            Game.Instance.coins++;
            Game.Instance.score += 200;

            _isPicked = true;

            gameObject.SetActive(false);
        }
    }

}
