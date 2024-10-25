using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public UnityEvent<WeaponType> OnNewWeaponFound;
    public UnityEvent<ItemType> OnItemUsed;
    public UnityEvent<Item> OnItemFound;
    public UnityEvent<ItemType, int> OnItemRemoved;

    [SerializeField] private List<KeyItem> keys = new List<KeyItem>();
    [SerializeField] private Dictionary<ItemType, int> items = new Dictionary<ItemType, int>();
    public UnityEvent<ItemType, int> OnItemCountChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void AddItem(ItemType itemType, Item item)
    {
        if (itemType == ItemType.Torch) 
        {
            OnItemFound?.Invoke(item);
        }

        if (items.ContainsKey(itemType))
        {
            items[itemType]++;
            Debug.Log(items);
        }
        else
        {
            items[itemType] = 1;
        }
        OnItemCountChanged.Invoke(itemType, items[itemType]);
    }

    public void AddWeapon(WeaponType type)
    {
        OnNewWeaponFound?.Invoke(type);
    }

    public void AddKey(KeyItem key)
    {
        keys.Add(key);
        Debug.Log(key.keyID);
    }

    public bool HasKey(string keyID)
    {
        foreach (KeyItem key in keys)
        {
            if (key.keyID == keyID)
            {
                return true;
            }
        }
        return false;
    }

    public void UseItem(ItemType itemType)
    {
        if (items.ContainsKey(itemType) && items[itemType] > 0)
        {
            items[itemType]--;
            OnItemUsed?.Invoke(itemType);
            OnItemCountChanged?.Invoke(itemType, items[itemType]);
            if (items[itemType] == 0)
            {
                items.Remove(itemType);
            }
        }
    }
}
