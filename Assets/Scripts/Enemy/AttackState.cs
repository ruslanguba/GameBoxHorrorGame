using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy _enemy;
    private float _attackCooldown = 2f;
    private float _lastAttackTime = 0f;
    private float _attackRange = 2f;
    private float _damage;
    public AttackState(Enemy enemy)
    {
        _enemy = enemy;
        _damage = enemy.EnemySettings.GetDamage();
        _attackCooldown = enemy.EnemySettings.GetAttackSpeed();
        _attackRange = enemy.EnemySettings.GetAttackRadius();
    }

    public void OnEnter()
    {
        _lastAttackTime = Time.time;
        _enemy.SetAnimation(EnemyAnimation.Attack);
    }

    public void OnExit()
    {
        _enemy.ResetAnimation();
    }

    public void Tick()
    {
        Vector3 playerPosition = _enemy.GetPlayerPosition();

        if (Vector3.Distance(_enemy.transform.position, playerPosition) <= _attackRange)
        {
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                if (Time.time >= _lastAttackTime + 0.5f)
                {
                    PerformAttack();
                    _lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            _enemy.ChangeState(new ChaseState(_enemy));
        }
    }

    private void PerformAttack()
    {
        Transform playerTransform = _enemy.GetPlayerTransform();
        if (playerTransform != null && Vector3.Distance(_enemy.transform.position, playerTransform.position) <= _attackRange)
        {
            CharacterHealth characterHealth = playerTransform.GetComponent<CharacterHealth>();
            characterHealth.TakeDamage(_damage);
        }
    }
}
