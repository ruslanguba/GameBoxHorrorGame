using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionAndHearing : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _visionRange = 4; // ������������ ��������� ���������
    [SerializeField] private float _visionAngle = 45; // ���� ���������
    [SerializeField] private float _hearingRange = 3; // ��������� ����������
    private Transform _enemyTransform;

    private void Awake()
    {
        _enemyTransform = transform;
        _player = FindObjectOfType<CharacterController>().transform;
    }

    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (_player.transform.position - _enemyTransform.position).normalized;
        float angleToPlayer = Vector3.Angle(_enemyTransform.forward, directionToPlayer);

        // ��������, ��������� �� ����� � �������� ���� ���������
        if (angleToPlayer < _visionAngle / 2f && Vector3.Distance(_enemyTransform.position, _player.transform.position) <= _visionRange)
        {
            // �������� �� ������� ����������� ����� ������ � �������
            if (!Physics.Raycast(_enemyTransform.position, directionToPlayer, Vector3.Distance(_enemyTransform.position, _player.transform.position)))
            {
                return true; // ����� �����
            }
        }
        return false; // ����� �� �����
    }

    public bool CanHearPlayer()
    {
        // ��������, ��������� �� ����� � �������� ����������
        return Vector3.Distance(_enemyTransform.position, _player.transform.position) <= _hearingRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // ������ ������ ���������
        Vector3 direction = transform.forward * _visionRange;
        Gizmos.DrawRay(transform.position, direction);

        // �����������: ������ ������ ����������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _hearingRange);
    }
}
