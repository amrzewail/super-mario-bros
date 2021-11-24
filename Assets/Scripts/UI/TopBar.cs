using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI time;
    public TextMeshProUGUI lives;

    IEnumerator Start()
    {
        while (true)
        {
            score.text = Game.Instance.score.ToString();
            coins.text = Game.Instance.coins.ToString();
            time.text = Game.Instance.time.ToString();
            lives.text = Game.Instance.lives.ToString();

            yield return new WaitForSeconds(0.2f);
        }
    }
}
 