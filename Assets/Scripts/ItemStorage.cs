using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _amount = 1;
    public void Use()
    {
        for (int i = 0; i < _amount; i++)
        {
            Inventory.Instance.AddItem(_itemType, null);
        }
    }
}
