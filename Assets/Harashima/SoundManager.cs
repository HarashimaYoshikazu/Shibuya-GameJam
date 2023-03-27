using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private SoundManager _instance;
    public SoundManager Instance => _instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if(_instance != null&&_instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        _audioSource= GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}

public enum AudioClipKind
{
    TitleBGM,
    StageBGM,
    GameClearBGM,
    GameOverBGM,
    UIClick,
    NameInputReaction,
    GameStart,
    DropObject,
    Enemy,
    Stun,
}
