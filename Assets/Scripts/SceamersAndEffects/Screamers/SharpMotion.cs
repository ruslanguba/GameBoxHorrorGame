using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpMotion : Screamer
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private Transform _moveTarget;
    [SerializeField] private Transform _transformToMove;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isAction;

    protected override void PerformScearAction()
    {
        _audioSource.PlayOneShot(_clip);
        _isAction = true;
    }

    private void MoveObject()
    {
        if (_isAction)
        {
            _transformToMove.position = Vector3.MoveTowards(_transformToMove.position, _moveTarget.position, _moveSpeed * Time.deltaTime);
            if (Vector3.Distance(_transformToMove.position, _moveTarget.position) < 0.01f)
            {
                _isAction = false;
            }
        }
    }

    private void Update()
    {
        MoveObject();
    }
}
