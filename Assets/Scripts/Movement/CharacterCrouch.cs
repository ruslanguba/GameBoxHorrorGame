using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCrouch : MonoBehaviour
{
    [SerializeField] private float _normalHeight = 2;
    [SerializeField] private float _crouchHeight = 1.2f;
    [SerializeField] private float _crouchSpeed = 3;

    [SerializeField] private float _colliderCentreNormalHeight = 0;
    [SerializeField] private float _colliderCentreCrouchHeight = 0.6f;

    [SerializeField] private Transform _cameraTargetTransform;
    [SerializeField] private float _normalCameraHeight = 0.7f;
    [SerializeField] private float _crouchCameraHeight = 0.4f;

    [SerializeField] private Transform _topCheckerPivot;
    [SerializeField] private float _topCheckerRadius;

    private CharacterController _controller;
    private bool _isCrouching = false;
    private bool _isCrouchStateChanged = false;
    private float _targetCameraHeight;
    private float _targetHeight;
    private float _targetColliderCentreHeight;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (_isCrouchStateChanged)
        {
            Crouch();
            AdjustCameraHeight();
        }
    }
    public void ToggleCrouch()
    {
        if (_isCrouching && isObstructionAbove()) 
        {
            return;
        }
        _isCrouching = !_isCrouching;
        CheckCrouchState();
        _isCrouchStateChanged = true;
    }

    public void Crouch()
    {
        _controller.height = Mathf.MoveTowards(_controller.height, _targetHeight, Time.deltaTime * _crouchSpeed * 5);
        Vector3 center = _controller.center;
        center.y = Mathf.MoveTowards(center.y, _targetColliderCentreHeight, Time.deltaTime * _crouchSpeed);
        _controller.center = center;
        if (Mathf.Abs(_controller.height - _targetHeight) < 0.02f)
        {
            _controller.height = _targetHeight;
            _controller.center = center;
            _isCrouchStateChanged = false;
        }
    }

    public void AdjustCameraHeight()
    {
        Vector3 cameraPosition = _cameraTargetTransform.localPosition;
        cameraPosition.y = Mathf.MoveTowards(cameraPosition.y, _targetCameraHeight, Time.deltaTime * _crouchSpeed);
        _cameraTargetTransform.localPosition = cameraPosition;
    }

    private void CheckCrouchState()
    {
        _targetCameraHeight = _isCrouching ? _crouchCameraHeight : _normalCameraHeight;
        _targetHeight = _isCrouching ? _crouchHeight : _normalHeight;
        _targetColliderCentreHeight = _isCrouching ? _colliderCentreCrouchHeight : _colliderCentreNormalHeight;
    }

    private bool isObstructionAbove()
    {
        bool result = Physics.CheckSphere(_topCheckerPivot.position, _topCheckerRadius);
        return result;
    }
}

