using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAttackAnimation(WeaponType type)
    {
        if (_animator != null)
        {
            if (type == WeaponType.Axe)
            {
                _animator.SetTrigger("AxeAttack");
            }
            else if (type == WeaponType.Knife)
            {
                _animator.SetTrigger("KnifeAttack");
            }
        }
    }
}
