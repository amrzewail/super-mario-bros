using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MysteryBox : MonoBehaviour
{
    [SerializeField] Object _hitbox;
    [SerializeField] int numberOfHits = 1;
    [SerializeField] Sprite exhaustedSprite;
    [SerializeField] GameObject spawnObject;
    [SerializeField] GameObject spawnObjectBig;
    [SerializeField] float spawnHeightOffset;
    [SerializeField] bool instantiateTarget = false;
    [SerializeField] float instantiateDelay = 0;
    [SerializeField] AppearAnimation appearAnimation;
    [SerializeField] UnityEvent OnHit;

    IHitbox hitbox => (IHitbox)_hitbox;

    private int _currentHits = 0;

    private void Start()
    {
        if (spawnObject) spawnObject.SetActive(false);
        if (spawnObjectBig) spawnObjectBig.SetActive(false);
    }

    private void Update()
    {
        if (hitbox.IsHit())
        {
            if (_currentHits < numberOfHits)
            {
                _currentHits++;
                OnHit?.Invoke();
                GetComponentInChildren<Damage>().GetComponent<Collider2D>().enabled = true;
                StartCoroutine(DisableDamage());
                var target = spawnObject;
                if (target)
                {
                    StartCoroutine(InstantiateTarget());
                }
                if (_currentHits == numberOfHits)
                {
                    GetComponentInChildren<SpriteRenderer>().sprite = exhaustedSprite;
                }
            }
        }
    }
    private IEnumerator DisableDamage()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<Damage>().GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator InstantiateTarget()
    {
        var target = spawnObject;
        if (spawnObjectBig && FindObjectOfType<Mario>().isBig) target = spawnObjectBig;
        yield return new WaitForSeconds(instantiateDelay);
        if (instantiateTarget)
        {
            target = Instantiate(spawnObject);
        }
        target.GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled = false);
        target.SetActive(true);
        target.transform.position = transform.position + Vector3.up * spawnHeightOffset;
        if (appearAnimation)
        {
            yield return StartCoroutine(appearAnimation.Show(target.transform, target.transform.position));
        }
        target.GetComponentsInChildren<Collider2D>().ToList().ForEach(x => x.enabled = true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * spawnHeightOffset);
    }
}
