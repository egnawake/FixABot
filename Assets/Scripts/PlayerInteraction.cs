using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float maxInteractionDistance;
    [SerializeField] private UIManager uiManager;

    private Transform cameraTransform;
    private Interactive currentInteractive;
    private bool refreshInteractive;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        UpdateCurrentInteractive();
        CheckForPlayerInput();
    }

    private void UpdateCurrentInteractive()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
            out hitInfo, maxInteractionDistance))
        {
            Interactive interactive = hitInfo.collider.GetComponent<Interactive>();

            if (interactive == null || !interactive.isOn)
            {
                ClearCurrentInteractive();
            }
            else if (interactive != currentInteractive || refreshInteractive)
            {
                SetCurrentInteractive(interactive);
            }
        }
        else if (currentInteractive != null)
        {
            ClearCurrentInteractive();
        }
    }

    private void CheckForPlayerInput()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractive != null)
        {
            currentInteractive.Interact();
            refreshInteractive = true;
        }
    }

    private void ClearCurrentInteractive()
    {
        SetCurrentInteractive(null);
        uiManager.HideInteractionPanel();
    }

    private void SetCurrentInteractive(Interactive interactive)
    {
        currentInteractive = interactive;
        refreshInteractive = false;

        if (interactive != null)
        {
            string interactionMessage = interactive.GetInteractionMessage();

            if (interactionMessage != null && interactionMessage.Length > 0)
                uiManager.ShowInteractionPanel(interactionMessage);
            else
            {
                uiManager.HideInteractionPanel();
            }
        }
    }
}
