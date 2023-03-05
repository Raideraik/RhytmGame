using NTC.Global.Cache;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Game : MonoCache
{
    public static event UnityAction OnGameStarted;
    public static Game Instance { get; private set; }


    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameoverScreen;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private float _additionalTimeForSpawn;
    private LoadSkin _loadSkin;


    protected override void OnEnabled()
    {
        _startScreen.StartButtonClick += OnStartButtonClick;
        _gameoverScreen.ExitButtonClick += ExitGame;
        _spawner.OnFinish += OnGameOver;
    }

    protected override void OnDisabled()
    {
        _startScreen.StartButtonClick -= OnStartButtonClick;
        _gameoverScreen.ExitButtonClick -= ExitGame;
        _spawner.OnFinish -= OnGameOver;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one Game!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Application.targetFrameRate = 60;
        _loadSkin = Get<LoadSkin>();
    }

    private void Start()
    {
        StartCoroutine(GameStart());
        Time.timeScale = 1;

    }
    private void ExitGame()
    {
        SceneFader.Instance.FadeTo(0);
        // SceneManager.LoadSceneAsync(0);
    }

    private void OnStartButtonClick()
    {
        _startScreen.gameObject.SetActive(false);
        Time.timeScale = 1;

        OnGameStarted?.Invoke();

        AudioFlow.Instance.StartFlow();
        _spawner.StartGame();
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        _gameoverScreen.gameObject.SetActive(true);
        _gameoverScreen.OpenGameOverScreen();
    }

    private IEnumerator GameStart()
    {
        _startScreen.gameObject.SetActive(true);
        // if (_loadSkin.GetChoosedSkin().GetSpawnEffect() != null)
        //  Instantiate(_loadSkin.GetChoosedSkin().GetSpawnEffect(), _characterSpawnPoint);



        yield return null;//new WaitForSeconds(_loadSkin.GetChoosedSkin().GetSpawnEffect().GetFloat("Duration") + _additionalTimeForSpawn);
        GameObject skin = Instantiate(_loadSkin.GetChoosedSkin().GetPrefab(), _characterSpawnPoint);
        //skin.GetComponent<CharacterAnimationController>().ChangeToIdle();
        _startScreen.OpenStart();

    }
}
