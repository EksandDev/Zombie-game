using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1;

    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        _audio.Play();
        Destroy(gameObject, _timeToDestroy);
    }
}
