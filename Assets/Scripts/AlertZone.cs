using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof(BoxCollider2D))]
public class AlertZone : MonoBehaviour
{
    [SerializeField] private AudioClip _alertSound;

    private AudioSource _alertSoundSorce;
    private float _targetVolume;

    private void Awake()
    {
        _alertSoundSorce = GetComponent<AudioSource>();
        _alertSoundSorce.clip = _alertSound;
        _alertSoundSorce.volume = 0;
        _alertSoundSorce.Play();
    }

    private void Update()
    {
        _alertSoundSorce.volume = Mathf.MoveTowards(_alertSoundSorce.volume, _targetVolume, Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))        
            _targetVolume = 1;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))        
            _targetVolume = 0;        
    }
}
