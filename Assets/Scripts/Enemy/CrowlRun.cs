using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowlRun : Screamer
{
    private Animator animator => GetComponentInChildren<Animator>();
    [SerializeField] private DoorOpenerCloser _closerOpenerCloser;
    [SerializeField] private float _animationTime;
    private float timer;
    private bool _isAction = false;
    protected override void PerformScearAction()
    {
        animator.SetTrigger("CrowlRun");
        _isAction = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformScearAction();
        }

        if(_isAction)
        {
            timer += Time.deltaTime;
            if (timer > _animationTime)
            {
                _isAction = false;
                _closerOpenerCloser.ToggleDoor();
            }
        }
    }
}
