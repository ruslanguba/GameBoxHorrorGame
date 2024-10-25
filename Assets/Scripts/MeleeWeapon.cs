using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private Transform attackPoint; // ����� �����
    [SerializeField] private float attackRange = 1f; // ������ �����
    [SerializeField] private LayerMask enemyLayers; // ���� ������
    [SerializeField] private WeaponAnimator _weaponAnimator;
    [SerializeField] private float _damageDelayTime;
    [SerializeField] private float _newAttackDelayTime;
    [SerializeField] private WeaponSound _soundManager;
    private bool isAttacking = false;

    private void Awake()
    {
        _weaponAnimator = Camera.main.GetComponentInChildren<WeaponAnimator>();
        _soundManager = GetComponent<WeaponSound>();
    }

    public override void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        _weaponAnimator.PlayAttackAnimation(weaponType);
        _soundManager.PlaySwingSound();
        Debug.Log("Played In Weapon");
        yield return new WaitForSeconds(_damageDelayTime); // ��������� �� �����

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<HealthSystem>(out HealthSystem health))
            {
                health.TakeDamage(damage);
                _soundManager.PlayHitSound();
                Debug.Log("Played HIT In Weapon");
            }
        }

        yield return new WaitForSeconds(_newAttackDelayTime); // ���������� �����
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
