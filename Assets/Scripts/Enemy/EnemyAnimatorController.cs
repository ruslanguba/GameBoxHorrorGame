using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.applyRootMotion = false;
        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnHit.AddListener(PlayHitAnmation);
    }

    public void SetAnimation(EnemyAnimation animation)
    {
        // Устанавливаем анимацию на основе переданного имени
        _animator.SetBool("IsIdle", animation == EnemyAnimation.Idle);
        _animator.SetBool("IsWalking", animation == EnemyAnimation.Walk);
        _animator.SetBool("IsRunning", animation == EnemyAnimation.Run);
        _animator.SetBool("IsLooking", animation == EnemyAnimation.LookAround);
        _animator.SetBool("Attack", animation == EnemyAnimation.Attack);
        _animator.SetBool("IsEating", animation == EnemyAnimation.Eat);
        if (animation == EnemyAnimation.GetUp)
        {
            _animator.applyRootMotion = false;
        }
        if(animation == EnemyAnimation.Die)
        {
            _animator.applyRootMotion = true;
            DieAnimation();
        }
        // Добавьте другие состояния по мере необходимости
    }

    public void ResetAnimation()
    {
        // Сброс всех параметров анимации
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsLooking", false);
        _animator.SetBool("Attack", false);
        _animator.SetBool("IsEating", false);
        // Сбросьте другие состояния по мере необходимости
    }

    private void PlayHitAnmation()
    {
        _animator.SetTrigger("Hit");
    }

    private void DieAnimation()
    {
        int rnd = Random.Range(0, 2);
        if (rnd == 0) _animator.SetTrigger("Die");
        else _animator.SetTrigger("Die2");
    }
}
