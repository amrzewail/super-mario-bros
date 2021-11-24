using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] RectTransform DefaultPanel;
    [SerializeField] RectTransform GameOverPanel;

    [SerializeField] Image character;
    [SerializeField] TextMeshProUGUI lives;

    [SerializeField] List<Sprite> characterImages;

    internal void Start()
    {
        if (Game.Instance.lives > 0)
        {
            DefaultPanel.gameObject.SetActive(true);
            GameOverPanel.gameObject.SetActive(false);

            Mario mario = FindObjectOfType<Mario>();
            int index = 0;
            if (mario.isBig) index++;
            if (mario.isBullet) index++;

            character.sprite = characterImages[index];
            lives.text = lives.text.Replace("%lives", Game.Instance.lives.ToString());
        }
        else
        {
            SoundEvents.Play(Sounds.GameOver);

            DefaultPanel.gameObject.SetActive(false);
            GameOverPanel.gameObject.SetActive(true);
        }
    }
}
