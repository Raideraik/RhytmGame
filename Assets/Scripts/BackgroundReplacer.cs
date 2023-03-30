using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BackgroundReplacer : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    //[SerializeField] private VideoClip[] _videoClips;

    private void Start()
    {
        _videoPlayer.clip = AudioFlow.Instance.GetSong().SongBackground;
        // _videoPlayer.clip = _videoClips[Random.Range(0, _videoClips.Length)];
    }
}
