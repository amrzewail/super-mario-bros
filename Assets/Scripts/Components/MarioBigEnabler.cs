using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioBigEnabler : MonoBehaviour
{
    [SerializeField] Mario mario;
    [SerializeField] List<Collider2D> targets;
    [SerializeField] bool whenBig;

    private int _lastVal = 0;
    private bool _update = false;

    internal void FixedUpdate()
    {
        if(!mario.isBig && _lastVal != -1 || (mario.isBig && _lastVal != 1))
        {
            _lastVal = mario.isBig ? 1 : -1;
            _update = true;
        }

        if (_update)
        {

            if (mario.isBig == whenBig)
            {
                targets.ForEach(x => x.enabled = true);
            }
            else
            {
                targets.ForEach(x => x.enabled = false);
            }
        }
        _update = false;
    }

}
