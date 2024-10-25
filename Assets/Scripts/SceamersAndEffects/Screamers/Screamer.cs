using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null)
        {
            return;
        }

        else
        {
            PerformScearAction();
        }
    }

    protected virtual void PerformScearAction()
    {
    }
}
