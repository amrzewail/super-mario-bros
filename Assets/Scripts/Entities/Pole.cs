using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    public Transform flag;

    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            var mario = collision.GetComponentInChildren<Mario>();
            mario.GrabPole();

            var pos = mario.transform.position;
            pos.x = transform.position.x - 0.25f;
            mario.transform.position = pos;


            StartCoroutine(MoveDown(mario));
        }

        IEnumerator MoveDown(Mario mario)
        {
            SoundEvents.StopBackground();
            SoundEvents.Play(Sounds.Flagpole);

            float time = 0;
            float length = 2;
            float moveRate = 6f / 1.2f;
            Vector3 marioEndPos = transform.position + Vector3.up * 1;
            marioEndPos.x = mario.transform.position.x;
            Vector3 flagEndPos = transform.position + Vector3.up * 1;
            flagEndPos.x = flag.transform.position.x;
            while (time < 1)
            {
                time += Time.deltaTime / length;
                var rate = moveRate * Time.deltaTime;
                mario.transform.position = Vector3.MoveTowards(mario.transform.position, marioEndPos, rate);
                flag.transform.position = Vector3.MoveTowards(flag.transform.position, flagEndPos, rate);
                yield return null;
            }

            SoundEvents.Play(Sounds.StageClear);
            mario.Finish();
        }
    }
}
