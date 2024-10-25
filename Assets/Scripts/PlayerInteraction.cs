using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;  // Инвентарь игрока
    [SerializeField] private PlayerRaycast _playerRaycast;

    private void Awake()
    {
        _playerRaycast = GetComponent<PlayerRaycast>();
        _playerInventory = FindObjectOfType<Inventory>();
    }
    public void Interact()
    {
        if (_playerRaycast.GetRaycastTarget() != null)
        {
            GameObject targetObject = _playerRaycast.GetRaycastTarget();
            IInteractable interactable = targetObject.GetComponent<IInteractable>();
            interactable.Use();
        }
    }
}
