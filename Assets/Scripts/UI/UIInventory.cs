using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Text batteryCountText;
    [SerializeField] private Text medKitCountText;

    private void Start()
    {
        Inventory.Instance.OnItemCountChanged.AddListener(UpdateUI);
        Debug.Log("Inventory.Instance.OnItemCountChanged.AddListener(UpdateUI);");
    }

    private void OnDisable()
    {
        Inventory.Instance.OnItemCountChanged.RemoveListener(UpdateUI);
    }

    // Метод для обновления UI
    private void UpdateUI(ItemType itemType, int count)
    {
        switch (itemType)
        {
            case ItemType.Battery:
                batteryCountText.text = count.ToString();
                break;
            case ItemType.Heal:
                medKitCountText.text = count.ToString();
                break;
        }
    }
}
