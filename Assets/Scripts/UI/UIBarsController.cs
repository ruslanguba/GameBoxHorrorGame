using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarsController : MonoBehaviour
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Slider _batarieChargeBarSlider;
    [SerializeField] private Slider _staminaBarSlider;

    [SerializeField] private Torch _torch => FindObjectOfType<Torch>();
    [SerializeField] private CharacterHealth _characterHealth => FindObjectOfType<CharacterHealth>();

    private void Awake()
    {
        if (_torch != null)
        {
            _torch.OnConsumeBattarie += UpdateBatteryChargeSlider;
        }
        _characterHealth.OnHealthChange += UpdateHealthSlider;
    }

    private void UpdateBatteryChargeSlider(float amount)
    {
        _batarieChargeBarSlider.value = amount;
    }
    private void UpdateHealthSlider(float amount)
    {
        _healthBarSlider.value = amount;
    }

}
