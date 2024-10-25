using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _moveSpeed;

    public Action<CharacterMoveType> OnMoveStateChanged; // Событие для уведомления о типе перемещения
    private CharacterController _controller;
    private Vector3 _moveDirection;
    private CharacterMoveType _currentMoveType;

    private bool _isCrouching = false;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _walkSpeed = GameSettings.Instance.PlayerWalkSpeed;
        _runSpeed = GameSettings.Instance.PlayerRunSpeed;
        _crouchSpeed = GameSettings.Instance.PlayerCrouchSpeed;
        _moveSpeed = _walkSpeed;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        CheckMoveType();
    }

    public void Move(Vector3 direction)
    {
        var horizontal = direction.x * Time.deltaTime;
        var vertical = direction.y * Time.deltaTime;
        var moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        _moveDirection = moveDirection * _moveSpeed;
    }

    private void MoveCharacter()
    {
        _controller.Move(_moveDirection);
    }

    private void CheckMoveType()
    {
        // Определяем тип движения в зависимости от текущей скорости
        if (_controller.velocity.magnitude == 0)
        {
            SetMoveType(CharacterMoveType.Stay);
        }
        else if (_moveSpeed == _walkSpeed)
        {
            SetMoveType(CharacterMoveType.Walk);
        }
        else if (_moveSpeed == _runSpeed)
        {
            SetMoveType(CharacterMoveType.Run);
        }
        else if (_isCrouching)
        {
            SetMoveType(CharacterMoveType.Crouch);
        }
    }

    private void SetMoveType(CharacterMoveType moveType)
    {
        if (_currentMoveType != moveType)
        {
            _currentMoveType = moveType;
            OnMoveStateChanged?.Invoke(_currentMoveType); // Уведомляем подписчиков о смене состояния
        }
    }

    public void ToggleCrouch()
    {
        _isCrouching = !_isCrouching;
        _moveSpeed = _isCrouching ? _crouchSpeed : _walkSpeed;
    }

    public void SetRunSpeed()
    {
        if (!_isCrouching)
            _moveSpeed = _runSpeed;
    }

    public void SetWalkSpeed()
    {
        if (!_isCrouching)
            _moveSpeed = _walkSpeed;
    }
}
