using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int slotCount;

    private int selectedSlot;
    private List<Interactive> inventory;

    public void Add(Interactive item)
    {
        inventory.Add(item);
        uiManager.SetInventoryIcon(inventory.Count - 1, item.InventoryIcon);

        if (inventory.Count == 1)
        {
            SelectSlot(0);
        }
    }

    public void Remove(Interactive item)
    {
        inventory.Remove(item);

        uiManager.ClearInventoryIcons();
        for (int i = 0; i < inventory.Count; i++)
        {
            uiManager.SetInventoryIcon(i, inventory[i].InventoryIcon);
        }

        if (selectedSlot == inventory.Count)
        {
            SelectSlot(selectedSlot - 1);
        }
    }

    public bool Contains(Interactive item)
    {
        return inventory.Contains(item);
    }

    public string GetInteractionMessage()
    {
        return inventory[selectedSlot].GetInteractionMessage();
    }

    public bool IsSelected(Interactive interactive)
    {
        return selectedSlot != -1 && interactive == inventory[selectedSlot];
    }

    public Interactive GetSelected()
    {
        return selectedSlot != -1 ? inventory[selectedSlot] : null;
    }

    private void SelectSlot(int index)
    {
        if (index < inventory.Count)
        {
            selectedSlot = index;
            uiManager.SetSelectedSlot(index);
        }
    }

    private void CheckForPlayerInput()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectSlot(i);
        }
    }

    private void Start()
    {
        inventory = new List<Interactive>();
        selectedSlot = -1;
    }

    private void Update()
    {
        CheckForPlayerInput();
    }
}
