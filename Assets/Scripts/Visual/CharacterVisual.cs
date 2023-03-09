using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private float _fillSpeed = 0.5f;
    [SerializeField] private Color _startColor;
    [SerializeField] private Material[] _materialsToReturn;


    private SkinnedMeshRenderer[] _skinnedMeshRenderer;
    private float _startFresnelPower = 0;
    private float _targetFresnelPower = 1;
    private float _startClip = 1;
    private float _targetClip = 0;
    private Color _targetColor = Color.black;
    private bool _power;
    private bool _color;


    private void Start()
    {
        _skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();

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
                Color color = Vector4.MoveTowards(_skinnedMeshRenderer[i].material.GetColor("_FresnelColor"), _targetColor, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetColor("_FresnelColor", color);
            }
            if (_skinnedMeshRenderer[i].material.GetColor("_FresnelColor") == _targetColor)
            {
                for (int j = 0; j < _materialsToReturn.Length; j++)
                {
                    if (_skinnedMeshRenderer[i].material.mainTexture == _materialsToReturn[j].mainTexture)
                    {
                        _skinnedMeshRenderer[i].material = _materialsToReturn[j];
                    }
                }
                enabled = false;
            }
        }
    }

}
