using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    IEnumerator Start()
    {
        float val = 1;
        while(val >= 0)
        {
            yield return null;
            val -= Time.deltaTime * 4;
            var col = GetComponent<Image>().color;
            col.a = val;
            GetComponent<Image>().color = col;
        }
    }
}
