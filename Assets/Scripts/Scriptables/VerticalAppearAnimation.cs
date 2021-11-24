using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Vertical Appear Animation", menuName = "Scriptable Objects/Vertical Appear Animation")]
public class VerticalAppearAnimation : AppearAnimation
{
    public float height = 1;

    public override IEnumerator Show(Transform target, Vector3 point)
    {
        SoundEvents.Play(Sounds.PowerUpAppears);

        float length = 1f;
        float time = 0;
        while (time <= 1)
        {
            time += Time.deltaTime / length;
            target.transform.position = Vector3.Lerp(point, point + Vector3.up * height, time);
            yield return null;
        }
        target.transform.position = point + Vector3.up * height;
    }
}
