using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _maxIntensity;
    [SerializeField] private float _minIntensity;
    [SerializeField] private float _maxFrequency;
    [SerializeField] private float _minFrequency;
    [SerializeField] private bool _isActive;

    private void Start()
    {
        _light = GetComponent<Light>();
        Activate();
    }
    public void Activate()
    {
        _isActive = true;
        _light.intensity = _maxIntensity;
        StartCoroutine(FlashingLightCorutine());
    }

    IEnumerator FlashingLightCorutine()
    {
        while (_isActive == true) 
        {
            yield return new WaitForSeconds(Random.Range(_minFrequency, _maxFrequency));
            _light.intensity = Random.Range(_minIntensity, _maxIntensity);
        }
    }

}
