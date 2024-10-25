using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void Move(Vector3 direction);
    void Jump();
}
