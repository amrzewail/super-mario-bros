using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AppearAnimation : ScriptableObject
{
    public abstract IEnumerator Show(Transform target, Vector3 point);
}
