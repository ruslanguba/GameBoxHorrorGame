using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private Vector3[] _patroPointValues;
    private Vector3 _nextPatrolPoint;
    private int _currentPatrolIndex;

    private void Awake()
    {
        SetPatrolPointsValues();
        GetNextPatrolTarget();
    }

    private void SetPatrolPointsValues()
    {
        _patroPointValues = new Vector3[_patrolPoints.Length];
        for (int i = 0; i < _patrolPoints.Length; i++)
        {
            _patroPointValues[i] = _patrolPoints[i].transform.position;
        }
    }

    public Vector3 GetNextPatrolTarget()
    {
        if (_currentPatrolIndex >= _patrolPoints.Length)
        {
            _currentPatrolIndex = 0;
        }
        _nextPatrolPoint = _patroPointValues[_currentPatrolIndex];
        _currentPatrolIndex++;
        return _nextPatrolPoint;
    }
}
