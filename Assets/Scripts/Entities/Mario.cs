using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Rigidbody2D rigidbody;

    [SerializeField] Object _input;
    [SerializeField] Object _mover;
    [SerializeField] Object _runMover;
    [SerializeField] Object _airMover;
    [SerializeField] Object _runAirMover;
    [SerializeField] Object _jumper;
    [SerializeField] Object _grounder;
    [SerializeField] Object _animator;
    [SerializeField] Object _hitbox;
    [SerializeField] Object _shooter;

    [Header("Gameplay properties")]
    public bool isBig = false;
    public bool isBullet = false;

    public IInput input => (IInput)_input;

    public IMover mover => (IMover)_mover;
    public IMover runMover => (IMover)_runMover;
    public IMover airMover => (IMover)_airMover;
    public IMover runAirMover => (IMover)_runAirMover;

    public IJumper jumper => (IJumper)_jumper;
    public IGrounder grounder => (IGrounder)_grounder;
    public IAnimator animator => (IAnimator)_animator;
    public IHitbox hitbox => (IHitbox)_hitbox;

    public IShooter shooter => (IShooter)_shooter;

    private bool _isDead = false;
    private bool _isRunning = false;
    private bool _isTransforming = false;
    private bool _isShooting = false;
    private float _lastGroundTime;
    private int _jumpChecks = 3;
    private int _currentChecks = 0;

    internal void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"));
    }

    internal void Update()
    {
        float horizontal = input.GetHorizontal();
        animator.speed = 1;

        if (hitbox.IsHit())
        {
            if (isBullet)
            {
                isBullet = false;
                SoundEvents.Play(Sounds.Pipe);
                StartCoroutine(TransformAnimation());
            }
            else
            if (isBig)
            {
                ShrinkDown();
            }
            else
            {
                Dead();
            }
        }
        if (_isDead || _isTransforming)
        {
            mover.Move(Vector2.zero);
            return;
        }

        var m = mover;
        if (grounder.IsGrounded())
        {
            _isRunning = false;
            if (input.IsCrouching() && isBig)
            {
                animator.Play(GetAnimation("Crouch"));
                horizontal = 0;
            }
            else
            {
                if (input.IsRunning())
                {
                    m = runMover;
                    animator.speed = 2;
                    _isRunning = true;
                }
                
                if (input.Jump())
                {
                    jumper.Jump();
                    SoundEvents.Play(Sounds.JumpSmall);
                }
                _lastGroundTime = Time.time;
                _currentChecks = 0;
                if (rigidbody.velocity.x > 0.4f)
                {
                    if (horizontal >= 0) animator.Play(GetAnimation("Run"));
                    else animator.Play(GetAnimation("Drag"));
                    renderer.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (rigidbody.velocity.x < -0.4f)
                {
                    if (horizontal <= 0) animator.Play(GetAnimation("Run"));
                    else animator.Play(GetAnimation("Drag"));
                    renderer.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    animator.Play(GetAnimation("Idle"));
                }
                if (isBullet && input.Shoot())
                {
                    if (shooter.Shoot(Mathf.RoundToInt(renderer.transform.localScale.x)))
                    {
                        SoundEvents.Play(Sounds.Fireball);
                        _isShooting = true;
                        StopCoroutine(RevertShooting());
                        StartCoroutine(RevertShooting());
                    }
                }
            }
        }
        else
        {
            animator.Play(GetAnimation("Jump"));
            m = airMover;
            if (_isRunning) m = runAirMover;
            if(Time.time - _lastGroundTime < 0.1f)
            {
                if (input.JumpHold())
                {
                    if(Time.time - _lastGroundTime > (_currentChecks+1) * 0.1f / _jumpChecks)
                    {
                        jumper.Jump();
                        if(_currentChecks == 1) SoundEvents.Play(Sounds.JumpSuper);
                        _currentChecks++;
                    }
                }
            }
        }
        m.Move(Vector2.right * horizontal);

        if (!isBig || input.IsCrouching())
        {
            hitbox.SetVSize(0.8f);
        }
        else
        {
            hitbox.SetVSize(1.3f);
        }
    }

    private string GetAnimation(string anim)
    {
        if (_isShooting) return "Shoot";
        if (isBig && isBullet) return $"Bullet {anim}";
        if (isBig) return $"Big {anim}";
        return anim;
    }


    private IEnumerator TransformAnimation()
    {
        var velocity = rigidbody.velocity;
        _isTransforming = true;
        animator.Play("Transform");
        rigidbody.bodyType = RigidbodyType2D.Static;
        hitbox.isInvincible = true;
        yield return new WaitForSeconds(1);
        _isTransforming = false;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        hitbox.isInvincible = false;
        rigidbody.velocity = velocity;
    }

    private IEnumerator RevertShooting()
    {
        yield return new WaitForSeconds(0.1f);
        _isShooting = false;
    }

    public void Dead()
    {
        _isDead = true;
        DisableControls();
        animator.Play("Dead");

        Game.Instance.GameOver();

        SoundEvents.Play(Sounds.Die);
        SoundEvents.StopBackground();
    }

    public void GrowUp()
    {
        if (isBig) return;

        isBig = true;
        StartCoroutine(TransformAnimation());
    }

    public void FireUp()
    {
        if (isBullet) return;
        if (!isBig)
        {
            GrowUp();
            return;
        }
        isBullet = true;
    }

    public void GrabPole()
    {
        animator.Play(GetAnimation("Pole"));
        DisableControls();
        Game.Instance.LevelComplete();
    }

    public void Finish()
    {
        animator.Play(GetAnimation("Finish"));
    }

    public void ShrinkDown()
    {
        isBig = false;
        SoundEvents.Play(Sounds.Pipe);
        StartCoroutine(TransformAnimation());
    }

    public void DisableControls()
    {
        this.enabled = false;
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        rigidbody.velocity = Vector2.zero;
        hitbox.isInvincible = true;
    }

    public void EnableControls()
    {
        this.enabled = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        hitbox.isInvincible = false;
    }

    public void Resurrect()
    {
        EnableControls();
        _isDead = false;
        isBig = false;
        isBullet = false;
    }

}
