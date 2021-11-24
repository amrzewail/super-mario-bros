using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimator
{
    public void Play(string animation);

    public float speed { get; set; }
}
