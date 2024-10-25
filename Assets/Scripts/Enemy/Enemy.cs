using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private IEnemyState _currentState;
    [SerializeField] private bool _isEating;
    private NavMeshAgent _navMeshAgent;
    private Transform _player; // ������ �� ������
    private float _hearingRange = 5f; // ��������� �����

    private EnemyAnimatorController _enemyAnimatorController;
    private PatrolSystem _patrolSystem;
    private EnemyVision _enemyVision;
    private EnemyHearing _enemyHearing;

    public EnemySettings EnemySettings { get; private set; }
    public float Speed { get; private set; } = 1.2f; // �������� �������� �����
    public float StoppingDistance { get; private set; } = 0.5f; // ����������, �� ������� ���� ���������������
    public float InvestigationRadius { get; private set; } = 5f; // ������ �������������

    private Vector3 _lastKnownPlayerPosition; // ������� ���������� ���������� �������������� ������

    private void Awake()
    {
        EnemySettings = GetComponent<EnemySettings>();
        _navMeshAgent = GetComponent<NavMeshAgent>(); // �������� ��������� NavMeshAgent
        _player = FindObjectOfType<Player>().transform; // �������� ������ �� ������
        _enemyVision = GetComponent<EnemyVision>();
        _enemyAnimatorController = GetComponent<EnemyAnimatorController>();
        _patrolSystem = GetComponent<PatrolSystem>();
        _enemyHearing = GetComponent<EnemyHearing>();
    }

    private void Start()
    {
        if (_isEating)
        {
            ChangeState(new EatingState(this));
        }
        else
        {
            ChangeState(new PatrolState(this, _patrolSystem.GetNextPatrolTarget())); // �������� � ��������� ��������������
        }
    }

    private void Update()
    {
        _currentState?.Tick(); // ��������� ������� ���������
    }

    public void ChangeState(IEnemyState newState)
    {
        _currentState?.OnExit(); // ����� �� �������� ���������
        _currentState = newState; // ������������� ����� ���������
        _currentState.OnEnter(); // ���� � ����� ���������S
    }

    public void SetAnimation(EnemyAnimation animation)
    {
        _enemyAnimatorController.SetAnimation(animation);
    }

    public void ResetAnimation()
    {
        _enemyAnimatorController.ResetAnimation();
    }
    public bool IsPlayerVisible()
    {
        if (_player == null)
        {
            return false;
        }
        return _enemyVision.CanSeePlayer(_player);
    }

    public bool IsPlayerHeard()
    {
        if (_player == null) return false;

        bool isCanHearPlayer = _enemyHearing.CanHearPlayer(_player);
        return isCanHearPlayer;
        // ���������, ������ �� ����� (��������, �� ����������)
        //float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        //_lastKnownPlayerPosition = _player.position; // ������� ����� ������ ��������� �����
        //return distanceToPlayer <= _hearingRange; // ���������, ������ �� �����
    }

    public Vector3 GetPlayerPosition()
    {
        return _player != null ? _player.position : Vector3.zero; // ���������� ������� ������
    }

    public Transform GetPlayerTransform()
    {
        return _player; // ���������� ��������� ������
    }

    public void SetLastKnownPlayerPosition(Vector3 position)
    {
        _lastKnownPlayerPosition = position; // ��������� ������� ���������� ���������� �������������� ������
    }

    public Vector3 GetPlayerLastKnownPosition()
    {
        return _lastKnownPlayerPosition; // ���������� ������� ���������� ���������� �������������� ������
    }

    public Vector3 GetPatrolTargetPoint()
    {
        return _patrolSystem.GetNextPatrolTarget();
    }
    public void MoveTowards(Vector3 targetPosition, float speed)
    {
        _navMeshAgent.SetDestination(targetPosition);
        _navMeshAgent.speed = speed; // ������������� �������� ��������
    }

    public void StopMovement()
    {
        _navMeshAgent.isStopped = true; // ������������� NavMeshAgent
    }

    public void ResumeMovement()
    {
        _navMeshAgent.isStopped = false; // ������������ ������ NavMeshAgent
    }
}
