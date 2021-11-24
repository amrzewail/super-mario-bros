using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Long V Appear Animation", menuName = "Scriptable Objects/Long V Appear Animation")]
public class LongVAppear : AppearAnimation
{
    public float height = 1;
    public float length = 1f;
    public bool disableAfterDone = false;

    public override IEnumerator Show(Transform target, Vector3 point)
    {
        float time = 0;
        while (time <= 1)
        {
            time += Time.deltaTime / (length * 0.5f);
            target.transform.position = Vector3.Lerp(point, point + Vector3.up * height, time);
            yield return null;
        } 
        while (time >= 0)
        {
            time -= Time.deltaTime / (length * 0.5f);
            target.transform.position = Vector3.Lerp(point, point + Vector3.up * height, time);
            yield return null;
        }
        target.transform.position = point;

        if (disableAfterDone) target.gameObject.SetActive(false);
    }
}
