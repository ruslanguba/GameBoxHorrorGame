using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    [SerializeField] private float _currentRange = 5;
    [SerializeField] private float _walkRange = 4;
    [SerializeField] private float _runRange = 6;
    [SerializeField] private float _crouchRange = 2;
    [SerializeField] private float _stayRange = 0.5f;
    [SerializeField] private CharacterMovement _characterMovement;

    private void Awake()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
    }

    private void Start()
    {
        _characterMovement.OnMoveStateChanged += HearingRange;
    }
    private void HearingRange(CharacterMoveType type)
    {
        switch (type)
        {
            case(CharacterMoveType.Walk) : 
                _currentRange = _walkRange; 
                break;
            case(CharacterMoveType.Run) :
                _currentRange = _runRange;
                break;
            case(CharacterMoveType.Crouch) :
                _currentRange = _crouchRange;
                break;
            case (CharacterMoveType.Stay):
                _currentRange = _stayRange;
                break;

        }
    }
    public bool CanHearPlayer(Transform player)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= _currentRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentRange);
    }
}
