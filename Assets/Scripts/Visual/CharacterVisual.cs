using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterVisual : MonoBehaviour
{
    public static event UnityAction _onPower;
    public static event UnityAction _onColor;
    public static event UnityAction _onEnded;

    [SerializeField] private float _fillSpeed = 0.5f;
    [SerializeField] private Color _startColor;
    [SerializeField] private Material[] _spawnMaterials;



    private List<Material> _oldMaterials = new List<Material>();
    private SkinnedMeshRenderer[] _skinnedMeshRenderer;
    private float _startFresnelPower = 0;
    private float _targetFresnelPower = 1;
    private float _startClip = 1;
    private float _targetClip = 0;
    private Color _targetColor = Color.black;
    private bool _power;
    private bool _color;
    private bool _ended;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponentsInChildren<SkinnedMeshRenderer>();

    }

    private void Start()
    {

        for (int i = 0; i < _skinnedMeshRenderer.Length; i++)
        {
            _oldMaterials.Add(_skinnedMeshRenderer[i].material);

            for (int j = 0; j < _spawnMaterials.Length; j++)
            {
                if (_skinnedMeshRenderer[i].material.mainTexture == _spawnMaterials[j].GetTexture("_Albedo"))
                {
                    _skinnedMeshRenderer[i].material = _spawnMaterials[j];

                }
            }

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
                    _onPower?.Invoke();
                }
            }
            if (_power)
            {

                float power = Mathf.MoveTowards(_skinnedMeshRenderer[i].material.GetFloat("_FresnelPower"), _targetFresnelPower, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetFloat("_FresnelPower", power);
                if (_skinnedMeshRenderer[i].material.GetFloat("_FresnelPower") == _targetFresnelPower)
                {
                    _color = true;
                    _onColor?.Invoke();
                }
            }
            if (_color)
            {
                Color color = Vector4.MoveTowards(_skinnedMeshRenderer[i].material.GetColor("_FresnelColor"), _targetColor, Time.deltaTime * _fillSpeed);
                _skinnedMeshRenderer[i].material.SetColor("_FresnelColor", color);
            }
            if (_skinnedMeshRenderer[i].material.GetColor("_FresnelColor") == _targetColor)
            {

                _ended = true;
            }
        }

        if (_ended)
        {
            for (int i = 0; i < _skinnedMeshRenderer.Length; i++)
            {
                for (int j = 0; j < _oldMaterials.Count; j++)
                {
                    if (_skinnedMeshRenderer[i].material.HasTexture("_Albedo"))
                    {

                        if (_skinnedMeshRenderer[i].material.GetTexture("_Albedo") == _oldMaterials[j].mainTexture)
                        {
                            _skinnedMeshRenderer[i].material = _oldMaterials[j];
                        }
                    }
                }



            }
            _onEnded?.Invoke();
            enabled = false;
        }
    }


}
