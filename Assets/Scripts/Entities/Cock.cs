using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cock : MonoBehaviour
{
    public Object _hitbox;

    IHitbox hitbox => (IHitbox)_hitbox;

    internal void Update()
    {
        if (hitbox.IsHit())
        {
            SoundEvents.Play(Sounds.Stomp);

            Game.Instance.score += 100;
            GetComponentInChildren<Animator>().Play("Dead");
            hitbox.isInvincible = true;
            GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled = false);
            GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            StartCoroutine(Fade());
        }
    }

    private IEnumerator Fade()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        for(float time=1f; time >= 0; time -= Time.deltaTime)
        {
            var col = renderer.color;
            col.a = time;
            renderer.color = col;
            yield return null;
        }
    }
}
