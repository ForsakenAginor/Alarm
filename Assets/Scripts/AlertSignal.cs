using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class AlertSignal : MonoBehaviour
{
    [SerializeField] private AudioClip _alertSound;

    private AudioSource _alertSoundSorce;
    private float _targetVolume;

    private void Awake()
    {
        _alertSoundSorce = GetComponent<AudioSource>();
        _alertSoundSorce.clip = _alertSound;
        _alertSoundSorce.volume = 0;
    }

    public void RaiseAlertVolume()
    {
        _targetVolume = 1;

        if (_alertSoundSorce.isPlaying == false)
            _alertSoundSorce.Play();
    }

    public void ReduceAlertVolume()
    {
        _targetVolume = 0;
    }

    private void Update()
    {
        if (_alertSoundSorce.isPlaying)
        {
            _alertSoundSorce.volume = Mathf.MoveTowards(_alertSoundSorce.volume, _targetVolume, Time.deltaTime);

            if (_targetVolume == 0 && _alertSoundSorce.volume == 0 && _alertSoundSorce.isPlaying)
                _alertSoundSorce.Pause();
        }
    }
}
