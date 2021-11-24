using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turtle : MonoBehaviour
{

    public enum State
    {
        Walking,
        Shell,
        Shooting
    }

    public State state = State.Walking;
    public Hitbox jumpHitbox;
    public Hitbox bodyHitbox;
    public float shootSpeedMultiplier = 2;

    private Animator _animator;
    private Mover _mover;
    private Rigidbody2D _rb;
    private Damage _damage;

    private Vector2 _defaultMoveSpeed;
    private float _shootTime = 0;

    internal void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _mover = GetComponentInChildren<Mover>();
        _rb = GetComponentInChildren<Rigidbody2D>();
        _damage = GetComponentInChildren<Damage>();
    }

    internal void Update()
    {
        if (jumpHitbox.IsHit())
        {
            switch (state)
            {
                case State.Walking:
                    state = State.Shell;
                    _rb.bodyType = RigidbodyType2D.Static;
                    _damage.enabled = false;
                    _defaultMoveSpeed = _mover.velocity;
                    _animator.Play("Shell");

                    SoundEvents.Play(Sounds.Kick);

                    break;

                case State.Shell:
                    Dead();
                    break;

                case State.Shooting:
                    if (Time.time - _shootTime > 0.2f)
                    {
                        Dead();
                    }
                    break;
            }
        }

        if (bodyHitbox.IsHit())
        {
            Dead();
        }
    }

    private void Dead()
    {
        SoundEvents.Play(Sounds.Stomp);

        _rb.bodyType = RigidbodyType2D.Static;
        _damage.enabled = false;
        _animator.Play("Dead");
        GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled = false);
        jumpHitbox.isInvincible = true;
        Destroy(this.gameObject, 1);
    }

    public void ShootLeft()
    {
        if(state == State.Shell)
        {
            SoundEvents.Play(Sounds.Kick);

            state = State.Shooting;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _damage.enabled = true;
            _damage.generalDamage = true;
            _defaultMoveSpeed.x = -Mathf.Abs(_defaultMoveSpeed.x);
            _mover.velocity = _defaultMoveSpeed * shootSpeedMultiplier;
            jumpHitbox.isPlayer = false;
            GetComponentsInChildren<MoverSwitcher>().ToList().ForEach(x => x.switchOnEnemy = false);
            _shootTime = Time.time;
        }
    }

    public void ShootRight()
    {
        if (state == State.Shell)
        {
            SoundEvents.Play(Sounds.Kick);

            state = State.Shooting;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _damage.enabled = true;
            _damage.generalDamage = true;
            _defaultMoveSpeed.x = Mathf.Abs(_defaultMoveSpeed.x);
            _mover.velocity = _defaultMoveSpeed * shootSpeedMultiplier;
            jumpHitbox.isPlayer = false;
            GetComponentsInChildren<MoverSwitcher>().ToList().ForEach(x => x.switchOnEnemy = false);
            _shootTime = Time.time;
        }
    }

}
