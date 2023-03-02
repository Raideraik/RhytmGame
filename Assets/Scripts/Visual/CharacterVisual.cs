using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Color _targetColor;
    [SerializeField] private Color _startColor;
    [SerializeField] private float _targetClip;
    [SerializeField] private float _startClip;
    [SerializeField] private float _startFresnelPower;
    [SerializeField] private float _targetFresnelPower;


    [SerializeField] private SkinnedMeshRenderer[] _skinnedMeshRenderer;
    private bool _power;
    private bool _color;


    private void Start()
    {
        for (int i = 0; i < _skinnedMeshRenderer.Length; i++)
        {
            _skinnedMeshRenderer[i].material.SetFloat("_Clip", _startClip);
            _skinnedMeshRenderer[i].material.SetFloat("_FresnelPower", _startFresnelPower);
            _skinnedMeshRenderer[i].material.SetColor("_FresnelColor", _startColor);
        }

    }

    private void Update()
    {
        for (int i = 0; i < _skinnedMeshRenderer.Length; i++)
        {
            if (_skinnedMeshRenderer[i].material.GetFloat("_Clip") > _targetClip)
            {
                float clip = Mathf.MoveTowards(_skinnedMeshRenderer[i].material.GetFloat("_Clip"), _targetClip, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetFloat("_Clip", clip);
                if (_skinnedMeshRenderer[i].material.GetFloat("_Clip") == _targetClip)
                {
                    _power = true;
                }
            }

            if (_power)
            {

                float power = Mathf.MoveTowards(_skinnedMeshRenderer[i].material.GetFloat("_FresnelPower"), _targetFresnelPower, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetFloat("_FresnelPower", power);
                if (_skinnedMeshRenderer[i].material.GetFloat("_FresnelPower") == _targetFresnelPower)
                {
                    _color = true;

                }
            }

            if (_color)
            {
                //Color color = Color.LerpUnclamped(_skinnedMeshRenderer.material.GetColor("_FresnelColor"), _targetColor, Time.deltaTime * _speed);
                Color color = Vector4.MoveTowards(_skinnedMeshRenderer[i].material.GetColor("_FresnelColor"), _targetColor, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetColor("_FresnelColor", color);
            }
            if (_skinnedMeshRenderer[i].material.GetColor("_FresnelColor") == _targetColor)
            {
                enabled = false;
            }
        }



    }


}
