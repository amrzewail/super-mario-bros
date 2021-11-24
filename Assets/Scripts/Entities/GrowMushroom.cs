using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowMushroom : MonoBehaviour
{
    private bool _isComplete = false;

    public void GrowPlayer()
    {
        Mario mario = FindObjectOfType<Mario>();
        if (!_isComplete && mario)
        {
            SoundEvents.Play(Sounds.PowerUp);

            mario.GrowUp();
            Game.Instance.score += 1000;
            gameObject.SetActive(false);
            _isComplete = true;
        }
    }
}
