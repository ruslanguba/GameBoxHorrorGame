using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableInfoShow : MonoBehaviour
{
    private PlayerRaycast _playerRaycast;
    [SerializeField] private GameObject interactionPanel; // ������ ��������������

    private void Awake()
    {
        _playerRaycast = FindObjectOfType<PlayerRaycast>();
        _playerRaycast.OnRaycastHitChanged += HandleRaycastHitChanged; // �������� �� �������
    }

    private void OnDestroy()
    {
        _playerRaycast.OnRaycastHitChanged -= HandleRaycastHitChanged; // ������� �� �������
    }

    private void HandleRaycastHitChanged(GameObject hitObject)
    {
        if (hitObject != null)
        {
            interactionPanel.SetActive(true); // �������� ������, ���� ������ � ������
        }
        else
        {
            interactionPanel.SetActive(false); // ��������� ������, ���� ������ �� ������
        }
    }
}
