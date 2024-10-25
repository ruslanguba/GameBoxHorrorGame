using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : Item
{
    public string keyID;

    protected override void Collect()
    {
        Inventory.Instance.AddKey(this);
    }
}
