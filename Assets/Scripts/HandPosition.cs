using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPosition : MonoBehaviour
{
    [SerializeField] private Transform _handHolder;
    private float _smooothnes = 2;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _handHolder.transform.position, _smooothnes);
        transform.rotation = Quaternion.Lerp(transform.rotation, _handHolder.transform.rotation, _smooothnes);
    }
}
