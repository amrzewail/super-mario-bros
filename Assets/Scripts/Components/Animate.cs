using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour, IAnimator
{
    [SerializeField] Animator animator;

    public float speed { get => animator.speed; set => animator.speed = value; }

    public void Play(string animation)
    {
        animator.Play(animation);
    }
}
