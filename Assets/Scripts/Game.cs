using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public static Game Instance { get; private set; }

    public int score { get; set; }
    public int coins { get; set; }
    public int time { get; set; }
    public int lives { get; set; }

    public float gameTime { get; private set; } = 0;
    public float levelTime { get; private set; } = 0;


    [SerializeField] int _score;
    [SerializeField] int _coins;
    [SerializeField] int _time;
    [SerializeField] int _lives;

    private bool _isPlaying = false;
    private string _targetPortalName;

    public Action OnLevelLoadComplete;

    internal void Awake()
    {
        Cursor.visible = false;
        Application.targetFrameRate = 60;

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    internal IEnumerator Start()
    {
        ResetStats();

        SceneManager.sceneLoaded += SceneLoadedCallback;

        yield return StartCoroutine(OpenLoading());
        yield return StartCoroutine(Resurrect(true));
    }

    internal void Update()
    {
        if(coins >= 100)
        {
            coins -= 100;
            lives++;
        }
        if (_isPlaying)
        {
            gameTime += Time.deltaTime;
            levelTime += Time.deltaTime;
            int lastTime = time;
            time = _time - (int)gameTime;
            if (time <= 0)
            {
                FindObjectOfType<Mario>().Dead();
            }
            if (lastTime != time && time == 30) SoundEvents.Play(Sounds.OutOfTime);
        }

        

    }

    private void ResetStats()
    {
        score = _score;
        lives = _lives;
        coins = _coins;
        time = _time;
    }

    private void SceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Loading") return;

        Portal p = scene.GetRootGameObjects().ToList().Single(x => x.name.Equals(_targetPortalName)).GetComponent<Portal>();
        p.transform.position = (Vector2)p.transform.position;
        GameObject.FindObjectOfType<Mario>().transform.position = p.transform.position;
        OnLevelLoadComplete?.Invoke();
    }

    private IEnumerator GameOverCoroutine()
    {
        if (lives > 0) lives--;

        yield return new WaitForSeconds(3f);


        if (lives == 0)
        {
            yield return StartCoroutine(OpenLoading());

            ResetStats();
        }

        yield return StartCoroutine(OpenLoading());
        yield return StartCoroutine(Resurrect(true));
    }

    public void GameOver()
    {
        _isPlaying = false;
        gameTime = 0;
        StartCoroutine(GameOverCoroutine());
    }

    public void LoadLevel(string name, string portalName)
    {
        levelTime = 0;
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        _targetPortalName = portalName;
    }

    public IEnumerator OpenLoading()
    {
        SceneManager.LoadScene("Loading");

        yield return new WaitForSeconds(4);

    }

    public IEnumerator Resurrect(bool isDead)
    {
        LoadLevel("Level 1-1", "Portal 1");
        Mario mario = FindObjectOfType<Mario>();
        if (isDead)
        {
            mario.Resurrect();
        }
        else
        {
            mario.EnableControls();
        }
        _isPlaying = true;
        yield return null;
    }

    private IEnumerator LevelCompleteCoroutine()
    {
        yield return new WaitForSeconds(8);
        yield return StartCoroutine(OpenLoading());
        yield return StartCoroutine(Resurrect(false));
    }

    public void LevelComplete()
    {
        _isPlaying = false;
        gameTime = 0;
        StartCoroutine(LevelCompleteCoroutine());
    }

}
