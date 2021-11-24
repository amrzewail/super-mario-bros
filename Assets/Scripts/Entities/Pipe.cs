using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public string levelName;
    public string portalName;
    public Direction direction = Direction.Down;

    private bool _isTriggering = false;
    private bool _isLoading = false;
    private Transform _playerTransform;
    private int _playerRenderOrder;

    public enum Direction
    {
        Up = 273, 
        Right = 275, 
        Down = 274, 
        Left = 276
    };

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            _isTriggering = true;
            _playerTransform = collision.transform;
        }
    }
    internal void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            _isTriggering = false;
            _playerTransform = collision.transform;
        }
    }

    internal void Update()
    {
        if (!_isTriggering) return;

        if (Input.GetKey((KeyCode)direction) && !_isLoading)
        {
            _isLoading = true;
            StartCoroutine(StartPipeAnimation());
        }
    }

    private IEnumerator StartPipeAnimation()
    {
        SoundEvents.StopBackground();
        SoundEvents.Play(Sounds.Pipe);

        float length = 1f;
        float time = 0;
        Vector3 playerPos = _playerTransform.transform.position;
        _playerTransform.GetComponentInChildren<Mario>().DisableControls();
        var renderer = _playerTransform.GetComponentInChildren<SpriteRenderer>();
        _playerRenderOrder = renderer.sortingOrder;
        renderer.sortingOrder = -100;
        while(time < 1)
        {
            time += Time.deltaTime / length;
            _playerTransform.transform.position = Vector3.Lerp(playerPos, transform.position, time);
            yield return null;
        }
        Game.Instance.OnLevelLoadComplete += LevelLoadCompleteCallback;
        Game.Instance.LoadLevel(levelName, portalName);
    }

    private void LevelLoadCompleteCallback()
    {
        if (!_playerTransform) return;

        var renderer = _playerTransform.GetComponentInChildren<SpriteRenderer>();
        _playerTransform.GetComponentInChildren<Mario>().EnableControls();
        renderer.sortingOrder = _playerRenderOrder;

        Game.Instance.OnLevelLoadComplete -= LevelLoadCompleteCallback;
    }
}
