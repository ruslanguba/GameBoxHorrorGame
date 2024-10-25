using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpHeight;

    [Header("Player Interaction")]
    [SerializeField] private float _interactionDistance;

    private void Awake()
    {
        
    }
}
