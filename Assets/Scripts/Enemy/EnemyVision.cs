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

        // Проверяем, находится ли игрок в пределах угла зрения
        if (angle <= visionAngle / 2)
        {
            RaycastHit hit;

            // Выполняем Raycast в сторону игрока
            if (Physics.Raycast(_raycastPoint.position, directionToPlayer.normalized, out hit, visionRange, playerLayer))
            {
                // Проверяем, попали ли мы в игрока
                if (hit.transform == player)
                {
                    // Дополнительно проверяем, не блокируется ли путь к игроку препятствием
                    if (!Physics.Raycast(_raycastPoint.position, directionToPlayer.normalized, distanceToPlayer, obstacleLayer))
                    {
                        return true; // Игрок видим
                    }
                }
            }
        }
        return false; // Игрок не видим
    }

    private void OnDrawGizmos()
    {
        // Отрисовка зоны видимости (конус)
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * visionRange;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * visionRange;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + leftBoundary);
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + rightBoundary);

        // Отрисовка линии, показывающей направление взгляда
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_raycastPoint.position, _raycastPoint.position + transform.forward * visionRange);
    }
}
