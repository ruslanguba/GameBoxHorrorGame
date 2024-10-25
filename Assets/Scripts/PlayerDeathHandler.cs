using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeathControll;
using UnityEngine.Events;
using System;

public class PlayerDeathHandler : DeathHandler
{
    public Action<bool> OnPlayerDead;
    protected override void HandleDeath()
    {
        OnPlayerDead?.Invoke(true);
        PlayerInputController _controller = GetComponent<PlayerInputController>();
        _controller.enabled = false;

    }
}
