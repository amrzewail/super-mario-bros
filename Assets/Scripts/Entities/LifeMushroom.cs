using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMushroom : MonoBehaviour
{
    private bool _isComplete = false;

    public void GiveLife()
    {
        if (!_isComplete)
        {
            SoundEvents.Play(Sounds.LiveUp);

            Game.Instance.lives++;
            gameObject.SetActive(false);
            _isComplete = true;
        }
    }
}
