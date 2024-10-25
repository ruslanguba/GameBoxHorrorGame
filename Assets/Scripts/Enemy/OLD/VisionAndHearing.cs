using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionAndHearing : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _visionRange = 4; // Максимальная дистанция видимости
    [SerializeField] private float _visionAngle = 45; // Угол видимости
    [SerializeField] private float _hearingRange = 3; // Дистанция слышимости
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

        // Проверка, находится ли игрок в пределах зоны видимости
        if (angleToPlayer < _visionAngle / 2f && Vector3.Distance(_enemyTransform.position, _player.transform.position) <= _visionRange)
        {
            // Проверка на наличие препятствий между врагом и игроком
            if (!Physics.Raycast(_enemyTransform.position, directionToPlayer, Vector3.Distance(_enemyTransform.position, _player.transform.position)))
            {
                return true; // Игрок видим
            }
        }
        return false; // Игрок не видим
    }

    public bool CanHearPlayer()
    {
        // Проверка, находится ли игрок в пределах слышимости
        return Vector3.Distance(_enemyTransform.position, _player.transform.position) <= _hearingRange;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Рисуем сектор видимости
        Vector3 direction = transform.forward * _visionRange;
        Gizmos.DrawRay(transform.position, direction);

        // Опционально: Рисуем радиус слышимости
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _hearingRange);
    }
}
