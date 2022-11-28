using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TextMeshProUGUI interactionMessage;
    [SerializeField] private Image[] inventorySlots;
    [SerializeField] private Image[] inventoryIcons;

    public void HideInteractionPanel()
    {
        interactionPanel.SetActive(false);
    }

    public void ShowInteractionPanel(string message)
    {
        interactionPanel.SetActive(true);
        interactionMessage.text = message;
    }

    public void ClearInventoryIcons()
    {
        foreach (Image icon in inventoryIcons)
        {
            icon.enabled = false;
        }
    }

    public void SetInventoryIcon(int index, Sprite icon)
    {
        inventoryIcons[index].sprite = icon;
        inventoryIcons[index].enabled = true;
    }

    public void SetSelectedSlot(int index)
    {
        foreach (Image slotImage in inventorySlots)
        {
            Color color = slotImage.color;
            color.a = 0.3f;
            slotImage.color = color;
        }
        
        if (index >= 0)
        {
            Color color = inventorySlots[index].color;
            color.a = 1f;
            inventorySlots[index].color = color;
        }
    }

    private void Start()
    {
        HideInteractionPanel();
        ClearInventoryIcons();
        SetSelectedSlot(-1);
    }
}
