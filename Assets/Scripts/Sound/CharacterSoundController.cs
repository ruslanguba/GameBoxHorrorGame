using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private AudioClip[] _step;
    [SerializeField] private float _walkStepTime;
    [SerializeField] private float _runStepTime;
    [SerializeField] private float _crouchStepTime; // частота шагов при крадучемся состоянии
    [SerializeField] private float _minPitch = 0.9f;
    [SerializeField] private float _maxPitch = 1.1f;
    private float _stepTime;
    private Coroutine _stepSoundCoroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _movement = FindObjectOfType<CharacterMovement>();
        _movement.OnMoveStateChanged += CheckMoveState;
    }

    private void CheckMoveState(CharacterMoveType movementState)
    {
        if (movementState == CharacterMoveType.Stay)
        {
            StopStepCoroutine();
            return;
        }
        else if (movementState == CharacterMoveType.Walk)
        {
            _stepTime = _walkStepTime;
            _audioSource.volume = 0.5f;
        }
        else if (movementState == CharacterMoveType.Run)
        {
            _stepTime = _runStepTime;
            _audioSource.volume = 0.7f;
        }
        else if (movementState == CharacterMoveType.Crouch) // Добавляем проверку на крадущийся режим
        {
            _stepTime = _crouchStepTime;
            _audioSource.volume = 0.3f;
        }
        StartStepCoroutine();
    }

    private void StartStepCoroutine()
    {
        if (_stepSoundCoroutine == null)
        {
            _stepSoundCoroutine = StartCoroutine(FootStepSound());
        }
    }

    private void StopStepCoroutine()
    {
        if (_stepSoundCoroutine != null)
        {
            StopCoroutine(_stepSoundCoroutine);
            _stepSoundCoroutine = null;
        }
    }

    private IEnumerator FootStepSound()
    {
        while (true)
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.PlayOneShot(_step[Random.Range(0, _step.Length)]);
            yield return new WaitForSeconds(_stepTime);
        }
    }
}
