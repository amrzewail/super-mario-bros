using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationClip : MonoBehaviour
{
    public Animator animator;
    public void Play(string clipName)
    {
        animator.Play(clipName);
    }

}
