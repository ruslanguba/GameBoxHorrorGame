using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private float visionRange = 10f;
    [SerializeField] private float visionAngle = 45f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    public bool CanSeePlayer(Transform player)
    {
        Vector3 directionToPlayer = player.position - _raycastPoint.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        // ���������, ��������� �� ����� � �������� ���� ������
        if (angle <= visionAngle / 2)
        {
            RaycastHit hit;

            // ��������� Raycast � ������� ������
            if (Physics.Raycast(_raycastPoint.position, directionToPlayer.normalized, out hit, visionRange, playerLayer))
            {
                // ���������, ������ �� �� � ������
                if (hit.transform == player)
                {
                    // ������������� ���������, �� ����������� �� ���� � ������ ������������
                    if (!Physics.Raycast(_raycastPoint.position, directionToPlayer.normalized, distanceToPlayer, obstacleLayer))
                    {
                        return true; // ����� �����
                    }
                }
            }
        }
        return false; // ����� �� �����
    }

    private void OnDrawGizmos()
    {
        // ��������� ���� ��������� (�����)
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + leftBoundary);
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + rightBoundary);

        // ��������� �����, ������������ ����������� �������
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + transform.forward * visionRange);
    }
}
