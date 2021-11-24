using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    internal void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Camera"), LayerMask.NameToLayer("Enemy"));
        target = FindObjectOfType<Mario>().transform;

        var pos = transform.position;
        pos.x = target.position.x + 12;
        transform.position = pos;
    }

    internal void LateUpdate()
    {
        var pos = transform.position;
        if (target.position.x > pos.x)
        {
            pos.x = target.position.x;
            transform.position = pos;
        }
    }
}
