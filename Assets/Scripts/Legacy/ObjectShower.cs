using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShower : MonoBehaviour
{
    public GameObject target;
    public float height;
    public int times = 1;

    private int _hitTimes = 0;

    public void Show()
    {
        if (!target) return;
        if (_hitTimes < times)
        {
            _hitTimes++;
            target.transform.position = transform.position;
            target.gameObject.SetActive(true);
            StartCoroutine(ShowAnimation());
        }
    }

    private IEnumerator ShowAnimation()
    {
        float length = 1f;
        float time = 0;
        while(time <= 1)
        {
            time += Time.deltaTime / length;
            target.transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * height, time);
            yield return null;
        }
        target.transform.position = transform.position + Vector3.up * height;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * height);
    }
}
