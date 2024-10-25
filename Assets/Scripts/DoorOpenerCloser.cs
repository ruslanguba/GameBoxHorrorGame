using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenerCloser : MonoBehaviour, IInteractable
{
    [SerializeField] private LockSystem _lockSystem;
    [SerializeField] private float _openAngle = -90f;
    [SerializeField] private float _closeAngle = 0f;
    [SerializeField] private float _speed = 2f;
    private Coroutine _currentCoroutine;

    [SerializeField] private bool _isOpened = false;

    private void Awake()
    {
        if (GetComponent<LockSystem>() != null)
        {
            _lockSystem = GetComponent<LockSystem>();
        }
    }

    public void Use()
    {
        if (_lockSystem == null || !_lockSystem.IsLocked())
        {
            ToggleDoor();
        }
    }

    public void ToggleDoor()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        // Выбор целевого угла в зависимости от состояния двери
        float targetAngle = _isOpened ? _openAngle : _closeAngle;
        _currentCoroutine = StartCoroutine(RotateDoor(targetAngle));
        _isOpened = !_isOpened; // Переключаем состояние двери
    }

    private IEnumerator RotateDoor(float targetAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _speed);
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}

