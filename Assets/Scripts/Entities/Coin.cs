using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Awake()
    {
        if(Game.Instance.levelTime < 1) gameObject.SetActive(false);
    }
    void OnEnable()
    {
        if (Game.Instance.levelTime < 1) return;

        SoundEvents.Play(Sounds.Coin);
        Game.Instance.coins++;
        Game.Instance.score += 200;
    }
}
