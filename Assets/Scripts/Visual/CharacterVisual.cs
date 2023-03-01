using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private float _speed;
    [SerializeField] private AnimationCurve _fadeCurve;

    private bool _power;
    private bool _color;

    private void Start()
    {
        StartCoroutine(Set());
    }
    private IEnumerator Set()
    {


        float time = 1f;
        float curve;

        while (_speed > 0f)
        {
            _speed -= Time.deltaTime;
            curve = _fadeCurve.Evaluate(_speed);

            if (_skinnedMeshRenderer.material.GetFloat("_Clip") > 0)
            {
                float clip = Mathf.MoveTowards(_skinnedMeshRenderer.material.GetFloat("_Clip"), 0, curve);
                _skinnedMeshRenderer.material.SetFloat("_Clip", clip);
                if (_skinnedMeshRenderer.material.GetFloat("_Clip") == 0)
                {
                    _power = true;
                }
            }

            if (_power)
            {

                float power = Mathf.MoveTowards(_skinnedMeshRenderer.material.GetFloat("_FresnelPower"), 1, curve);
                _skinnedMeshRenderer.material.SetFloat("_FresnelPower", power);
                if (_skinnedMeshRenderer.material.GetFloat("_FresnelPower") == 1)
                {
                    _color = true;

                }
            }

            if (_color)
            {
                Color color = Color.LerpUnclamped(_skinnedMeshRenderer.material.GetColor("_FresnelColor"), Color.black, curve);
                _skinnedMeshRenderer.material.SetColor("_FresnelColor", color);


            }

            if (_skinnedMeshRenderer.material.GetColor("_FresnelColor") == Color.black)
            {
                yield return 0;
            }
            //_image.color = new Color(_colorOfFade.r, _colorOfFade.g, _colorOfFade.b, curve);
        }
    }

}
