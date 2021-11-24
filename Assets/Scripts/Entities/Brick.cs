using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public Object _hitbox;
    public Damage damage;
    public GameObject collider;
    public GameObject sprite;
    public GameObject shatter;

    IHitbox hitbox => (IHitbox)_hitbox;

    internal void Update()
    {
        if (hitbox.IsHit())
        {
            Mario mario = Object.FindObjectOfType<Mario>();
            if (mario.isBig)
            {
                hitbox.isInvincible = true;
                sprite.SetActive(false);
                StartCoroutine(DisableCollider());
                StartCoroutine(DisableDamage());
                damage.GetComponent<Collider2D>().enabled = true;
                shatter.SetActive(true);
                SoundEvents.Play(Sounds.BrickSmash);
            }
            else
            {
                GetComponent<Animator>().Play("BrickHit");
                SoundEvents.Play(Sounds.Bump);
                StartCoroutine(DisableDamage());
                damage.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
    private IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(0.2f);
        damage.GetComponent<Collider2D>().enabled = false;
    }
    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(0.2f);
        collider.SetActive(false);
    }

}
