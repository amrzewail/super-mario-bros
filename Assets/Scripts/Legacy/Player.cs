using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Player : MonoBehaviour
{
    public float speed;
    public float runSpeed;
    public float airSpeedRatio = 0.5f;
    public float jumpForce;
    public float jumpHoldDuration = 0.25f;
    public float acceleration = 0.1f;

    public OnTrigger groundTrigger;

    private bool _isDead = false;
    private bool _isRunning = false;
    private bool _isJumping;
    private float _jumpStartTime;
    private float _lastSpeed;
    private float _targetSpeed;
    private float _currentSpeed => _rigidBody.velocity.x;
    private Rigidbody2D _rigidBody;
    private Vector2 _lastPosition;

    private void Start()
    {
        _targetSpeed = 0;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isDead)
        {
            if (groundTrigger.isTriggering)
            {
                GroundControls();
                _lastSpeed = _currentSpeed;
            }
            else
            {
                AirControls();
            }


            if (Input.GetKeyDown(KeyCode.X))
            {
                _isRunning = true;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                _isRunning = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (groundTrigger.isTriggering)
                {
                    _isJumping = true;
                    _jumpStartTime = Time.time;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isJumping = false;
            }

        }

    }

    private void FixedUpdate()
    {
        JumpUpdate();
        MoveUpdate();
    }

    private void GroundControls()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _targetSpeed = _isRunning ? runSpeed : speed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _targetSpeed = _isRunning ? -runSpeed : -speed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (groundTrigger.isTriggering)
        {
            _targetSpeed = 0;
        }

    }

    private void AirControls()
    {
        _targetSpeed = _lastSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _targetSpeed += speed * airSpeedRatio * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _targetSpeed -= speed * airSpeedRatio * Time.deltaTime;
        }
        _lastSpeed = _targetSpeed;
    }

    private void MoveUpdate()
    {
        Vector2 velocity = _rigidBody.velocity;
        velocity.x = Mathf.MoveTowards(velocity.x, _targetSpeed, acceleration * Time.deltaTime);
        _rigidBody.velocity = velocity;

        _lastPosition = transform.position;
    }

    private void JumpUpdate()
    {
        if (_isJumping && (Time.time - _jumpStartTime) < jumpHoldDuration)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }
    }

    public void Dead()
    {
        _isDead = true;
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled=false);
    }
}
