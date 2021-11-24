using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour, IMover
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] float speed;
    [SerializeField] float acceleration;


    public void Move(Vector2 axis)
    {
        var v = rigidbody.velocity;
        float mul = 1;
        if ((v.x > 0 && axis.x < 0) || (v.x < 0 && axis.x > 0)) mul *= 2;
        v.x = Mathf.MoveTowards(v.x, axis.x * speed, mul * acceleration * Time.deltaTime);
        rigidbody.velocity = v;
    }
}
