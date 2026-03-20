using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private List<UI_InventorySlot> _slots;

    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChange += Refresh;
        Refresh();
    }

    private void Refresh()
    {
        if (InventoryManager.Instance.SlotCount == 0)
        {
            _group.alpha = 0f;
            return;
        }
        _group.alpha = 1f;

        for (int i = 0; i < _slots.Count; i++)
        {
            if (i < InventoryManager.Instance.SlotCount)
            {
                _slots[i].SetSlot(InventoryManager.Instance.GetItem(i));
            }
            else
            {
                _slots[i].SetSlot(null);
            }
        }
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null) 
        InventoryManager.Instance.OnInventoryChange -= Refresh;
    }
}