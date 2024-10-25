using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInfoShow : MonoBehaviour
{
    private PlayerRaycast _playerRaycast;
    [SerializeField] private GameObject interactionPanel; // Панель взаимодействия

    private void Awake()
    {
        _playerRaycast = FindObjectOfType<PlayerRaycast>();
        _playerRaycast.OnRaycastHitChanged += HandleRaycastHitChanged; // Подписка на событие
    }

    private void OnDestroy()
    {
        _playerRaycast.OnRaycastHitChanged -= HandleRaycastHitChanged; // Отписка от события
    }

    private void HandleRaycastHitChanged(GameObject hitObject)
    {
        if (hitObject != null)
        {
            interactionPanel.SetActive(true); // Включаем панель, если попали в объект
        }
        else
        {
            interactionPanel.SetActive(false); // Выключаем панель, если ничего не попали
        }
    }
}
