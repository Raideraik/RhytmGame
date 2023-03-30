using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NTC.Global.Cache;

public class SceneFader : MonoCache
{
    public static SceneFader Instance { get; private set; }

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Color _colorOfFade;
    [SerializeField] private AnimationCurve _fadeCurve;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There more than one SceneFader!" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float time = 1f;
        float curve;

        while (time > 0f)
        {
            time -= Time.unscaledDeltaTime;
            curve = _fadeCurve.Evaluate(time);
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = curve;
            //_image.color = new Color(_colorOfFade.r, _colorOfFade.g, _colorOfFade.b, curve);
            yield return 0;
        }
    }
    private IEnumerator FadeOut(int scene)
    {

        float time = 0f;
        float curve;

        while (time < 1f)
        {
            time += Time.unscaledDeltaTime;
            curve = _fadeCurve.Evaluate(time);
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = curve;

            // _image.color = new Color(_colorOfFade.r, _colorOfFade.g, _colorOfFade.b, curve);

            yield return null;
        }

        SceneManager.LoadSceneAsync(scene);

    }

    public void FadeTo(int scene)
    {
        StartCoroutine(FadeOut(scene));
    }
}
