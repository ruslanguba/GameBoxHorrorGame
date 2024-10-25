using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class CharacterMovementState : MonoBehaviour
{
    // ≈—À» Õ≈ ”—œ≈ﬁ ›“Œ“  À¿—— ƒÀﬂ Œœ–≈ƒ≈À≈Õ»ﬂ —Œ—“ŒﬂÕ»ﬂ »√–Œ ¿ ¬ ¡”ƒ”Ÿ≈Ã ƒ»—“¿Õ÷»ﬂ Õ¿  Œ“Œ–… ≈√Œ ¡”ƒ”“ —À€ÿ¿“‹ ¬–¿√»
    public CharacterMoveType MoveType;
    [SerializeField] private float _checkTime = 0.2f;
    [SerializeField] private float _enemiesHearDistance = 3;
    [SerializeField] CharacterMovement _characterMovement;
    [SerializeField] private float _stateTogleSpeed;
    public Action<CharacterMoveType> OnMoveStateChanged;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _stateTogleSpeed = GameSettings.Instance.PlayerWalkSpeed;

    }

    IEnumerator CheckMovementState(float moveSpeed)
    {
        yield return new WaitForSeconds(_checkTime);
        ChangeState(moveSpeed);
    }
    
    private void ChangeState(float moveSpeed)
    {
        if (moveSpeed < 0.1f)
        {
            MoveType = CharacterMoveType.Stay;
        }
        else if (moveSpeed < _stateTogleSpeed)
        {
            MoveType = CharacterMoveType.Walk;
        }
        else if (moveSpeed > _stateTogleSpeed)
        {
            MoveType = CharacterMoveType.Run;
        }
        OnMoveStateChanged?.Invoke(MoveType);
        //StartCoroutine(CheckMovementState());

        //IEnumerator CheckMovementState()
        //{
        //    while (true)
        //    {
        //        yield return new WaitForSeconds(_checkTime);
        //        if (_characterController.velocity.magnitude < 0.1f)
        //        {
        //            MoveType = CharacterMoveType.Stay;
        //        }
        //        else if (_characterController.velocity.magnitude < _stateTogleSpeed)
        //        {
        //            MoveType = CharacterMoveType.Walk;
        //        }
        //        else if (_characterController.velocity.magnitude > _stateTogleSpeed)
        //        {
        //            MoveType = CharacterMoveType.Run;
        //        }
        //        OnMoveStateChanged?.Invoke(MoveType);
        //        Debug.Log("Character Movement State" + MoveType);
        //    }
        //}
    }
}
