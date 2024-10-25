using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{   
    private GameInput _gameInput;
    private PlayerInteraction _playerInteraction;
    private CharacterMovement _characterMovement;
    private CharacterCrouch _characterCrouch;
    private CharacterJump _characterJump;
    private CharacterAttack _characterAttack;
    private WeaponSelector _weaponSelector;
    private Torch _torch;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        _characterMovement = GetComponent<CharacterMovement>();
        _characterCrouch = GetComponent<CharacterCrouch>();
        _characterJump = GetComponent<CharacterJump>();
        _playerInteraction = GetComponent<PlayerInteraction>();
        _characterAttack = GetComponent<CharacterAttack>();
        _weaponSelector = GetComponent<WeaponSelector>();
        _torch = GetComponent<Torch>();
    }

    private void OnEnable()
    {
        _gameInput.GamePlay.Crouch.performed += CrouchPerformed;
        _gameInput.GamePlay.Jump.performed += JumpPerformed;
        _gameInput.GamePlay.Interact.performed += InteractPerformed;
        _gameInput.GamePlay.Attack.performed += AttackPerformed;
        _gameInput.GamePlay.WeaponSelect.performed += WeaponSelectPerformed;
        _gameInput.GamePlay.Sprint.performed += SprintPerformed;
        _gameInput.GamePlay.Sprint.canceled += SprintCanceled;
        _gameInput.GamePlay.MouseScroll.performed += MouseScroll;
        _gameInput.GamePlay.ActiveteTorch.performed += ActiveteTorchPerformed;
        _gameInput.GamePlay.UseBattery.performed += UseBatteryPerformed;
    }

    private void ActiveteTorchPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _torch.ToggleTorch();
    }

    private void MouseScroll(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        float scrollValue = obj.ReadValue<float>();
        if (scrollValue > 0)
        {
            _torch.IncreaseRange();
        }
        else if (scrollValue < 0)
        {
           _torch.DecreaseRange();
        }
    }

    private void OnDisable()
    {
        _gameInput.GamePlay.Jump.performed -= JumpPerformed;
        _gameInput.GamePlay.Crouch.performed -= CrouchPerformed;
        _gameInput.GamePlay.Interact.performed -= InteractPerformed;
        _gameInput.GamePlay.Attack.performed -= AttackPerformed;
        _gameInput.GamePlay.WeaponSelect.performed -= WeaponSelectPerformed;
        _gameInput.GamePlay.Sprint.performed -= SprintPerformed;
        _gameInput.GamePlay.Sprint.canceled -= SprintCanceled;
        _gameInput.GamePlay.MouseScroll.performed -= MouseScroll;
        _gameInput.GamePlay.ActiveteTorch.performed -= ActiveteTorchPerformed;
        _gameInput.GamePlay.UseBattery.performed -= UseBatteryPerformed;
    }

    private void UseBatteryPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Inventory.Instance.UseItem(ItemType.Battery);
        Debug.Log("Input");
    }

    private void SprintPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _characterMovement.SetRunSpeed();
    }

    private void SprintCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _characterMovement.SetWalkSpeed();
    }

    private void WeaponSelectPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        string inputValue = obj.control.displayName;
        int weapnIndex = int.Parse(inputValue);
        _weaponSelector.ChangeWeapon(weapnIndex);
    }

    private void AttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _characterAttack.Attack();
    }

    private void InteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _playerInteraction.Interact();
    }

    private void JumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _characterJump.Jump();
    }

    private void CrouchPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _characterCrouch.ToggleCrouch();
        _characterMovement.ToggleCrouch();
    }

    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        _characterMovement.Move(_gameInput.GamePlay.Movement.ReadValue<Vector2>());
    }
}
