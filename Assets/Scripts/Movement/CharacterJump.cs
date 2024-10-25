using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3;
    [SerializeField] private float _checkGroundRadius;
    [SerializeField] private Transform _groundCheckerPivot;
    [SerializeField] private LayerMask _groundMask;
    
    private CharacterController _controller;
    private float _velocityY; 
    private bool _isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _isGrounded = isOnGround();
        if (_isGrounded && _velocityY < 0)
        {
            _velocityY = -1f; 
        }

        DoGravity();
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocityY = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
    }

    private void DoGravity()
    {
        _velocityY += _gravity * Time.deltaTime;
        _controller.Move(Vector3.up * _velocityY * Time.deltaTime);
    }

    private bool isOnGround()
    {
        return Physics.CheckSphere(_groundCheckerPivot.position, _checkGroundRadius, _groundMask); ;
    }
}
