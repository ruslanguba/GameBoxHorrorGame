using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private Light _torch;
    [SerializeField] private GameObject _torchBody;
    [SerializeField] private float _batteryConsumeTime = 5;
    [SerializeField] private float _currentBattarieCharge = 40;
    [SerializeField] private float _maxBattarieCharge;
    private bool _isFound = false;
    private bool _isActive = false;
    private Coroutine _consumeBattarie;    
    public Action<float> OnConsumeBattarie;
    [SerializeField] private float _minAngle = 20f;
    [SerializeField] private float _maxAngle = 90f;
    [SerializeField] private float _minRange = 8f;
    [SerializeField] private float _maxRange = 20f;

    private int _currentStep = 0;
    private int _maxSteps = 4;

    private float _angleStep => (_maxAngle - _minAngle) / _maxSteps;
    private float _rangeStep => (_maxRange - _minRange) / _maxSteps;

    private void Start()
    {
        Inventory.Instance.OnItemUsed.AddListener(UseBattery);
        Inventory.Instance.OnItemFound.AddListener(TorchFound);
        _torch.range = _minRange;
        _torch.spotAngle = _maxAngle;
        _torchBody.gameObject.SetActive(false);
    }

    public void IncreaseRange()
    {
        if (_currentStep < _maxSteps)
        {
            _currentStep++;
            UpdateFlashlight();
        }
    }

    private void TorchFound(Item item)
    {
        Debug.Log("Torch found");
        if (item.Type == ItemType.Torch)
        {
            _isFound = true;
            if (!_isActive)
            {
                Debug.Log("Torch Togled first");
                ToggleTorch();
            }
        } 
    }

    public void DecreaseRange()
    {
        if (_currentStep > 0)
        {
            _currentStep--;
            UpdateFlashlight();
        }
    }
    public void ToggleTorch()
    {
        if (_isFound)
        {
            _isActive = !_isActive;
            if (_isActive)
            {
                Debug.Log("IsAcctive");
                _consumeBattarie = StartCoroutine(BatterieConsum());
                _torchBody.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("IsInacctive");
                StopCoroutine(BatterieConsum());
                _torchBody.gameObject.SetActive(false);
            }
            _torch.enabled = _isActive;
        }
    }

    private void UpdateFlashlight()
    {
        float currentAngle = _maxAngle - _currentStep * _angleStep;
        float currentRange = _minRange + _currentStep * _rangeStep;

        _torch.spotAngle = currentAngle;
        _torch.range = currentRange;

    }

    private void UseBattery(ItemType item)
    {
        if (item == ItemType.Battery)
        {
            if (_currentBattarieCharge < _maxBattarieCharge)
            {
                _currentBattarieCharge = _maxBattarieCharge;
                _torch.intensity = _currentBattarieCharge / 100;
            }
            OnConsumeBattarie?.Invoke(_currentBattarieCharge);
        }
    }

    IEnumerator BatterieConsum()
    {
        while (_isActive)
        {
            if (_currentBattarieCharge > 0)
            {
                _currentBattarieCharge--;
                _torch.intensity = _currentBattarieCharge / 100;
                OnConsumeBattarie?.Invoke(_currentBattarieCharge);
            }
            yield return new WaitForSeconds(_batteryConsumeTime);
        }
    }
}
