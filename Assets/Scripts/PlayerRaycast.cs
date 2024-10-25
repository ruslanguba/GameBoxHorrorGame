using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 1f;         // ��������� ��� ��������
    [SerializeField] private LayerMask _interactableLayer;      // ������ ����� ��� ��������������
    [SerializeField] private Camera _playerCamera;              // ������ ������
    [SerializeField] private GameObject lastHitObject = null; // ������, �� ������� ����� ��������� ���
    public Color rayColor = Color.red;

    public event Action<GameObject> OnRaycastHitChanged;

    private void Awake()
    {
        _playerCamera = Camera.main;
    }
    void Update()
    {
        DoRaycast();
    }

    private void DoRaycast()
    {
        Ray ray = _playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction.normalized * _rayDistance, rayColor);

        if (Physics.Raycast(ray, out hit, _rayDistance, _interactableLayer))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != lastHitObject)
            {
                lastHitObject = hitObject;
                LogHitObject(hitObject); // �������� ������
                OnRaycastHitChanged?.Invoke(hitObject);
            }
        }
        else
        {
            OnRaycastHitChanged?.Invoke(null); // ���� ������ �� ������, �������� null
            lastHitObject = null;
        }
    }

    void LogHitObject(GameObject hitObject)
    {
    }

    public GameObject GetRaycastTarget()
    {
        return lastHitObject;
    }
}
