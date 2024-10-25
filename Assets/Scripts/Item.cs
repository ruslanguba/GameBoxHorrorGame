using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public string itemName;  // Имя предмета (например, "Ключ от двери A")
    public ItemType Type;
    public void Use()
    {
        Collect();
    }

    protected virtual void Collect()
    {
        Inventory.Instance.AddItem(this.Type, this);
        Destroy(gameObject, 0.1f);
    }
}
