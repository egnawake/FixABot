using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactive : MonoBehaviour
{
    public bool isOn;

    [SerializeField] private InteractiveData interactiveData;

    private InteractionManager interactionManager;
    private PlayerInventory playerInventory;
    private List<Interactive> dependents;
    private List<Interactive> requirements;
    private Animator animator;
    private bool requirementsMet;
    private int interactionCount;

    public InteractiveData InteractiveData => interactiveData;
    public string InventoryName => interactiveData.inventoryName;
    public Sprite InventoryIcon => interactiveData.inventoryIcon;
    public UnityEvent OnInteracted => onInteracted;

    public string GetInteractionMessage()
    {
        if (IsType(InteractiveData.Type.Pickable)
            && !playerInventory.Contains(this)
            && requirementsMet)
        {
            return InteractionManager.Instance.PickMessage.Replace(
                "%", interactiveData.inventoryName);
        }
        else if (!requirementsMet)
        {
            if (PlayerHasRequirementSelected())
                return playerInventory.GetInteractionMessage();
            else
                return interactiveData.requirementsMessage;
        }
        else if (interactiveData.interactionMessages.Length > 0)
        {
            int messageCount = interactiveData.interactionMessages.Length;
            return interactiveData.interactionMessages[interactionCount % messageCount];
        }

        return null;
    }

    public void Interact()
    {
        if (requirementsMet)
            InteractSelf(true);
        else if (PlayerHasRequirementSelected())
            UseRequirement();
    }

    public void AddDependent(Interactive dependent)
    {
        dependents.Add(dependent);
    }

    public void AddRequirement(Interactive requirement)
    {
        requirements.Add(requirement);
    }

    public bool IsType(InteractiveData.Type type)
    {
        return interactiveData.type == type;
    }

    private bool PlayerHasRequirementSelected()
    {
        foreach (Interactive requirement in requirements)
        {
            if (playerInventory.IsSelected(requirement))
                return true;
        }

        return false;
    }

    private void InteractSelf(bool direct)
    {
        if (direct && IsType(InteractiveData.Type.Indirect))
            return;
        
        if (animator != null && !IsType(InteractiveData.Type.Pickable))
            animator.SetTrigger("Interact");

        if (IsType(InteractiveData.Type.Pickable))
        {
            playerInventory.Add(this);
            gameObject.SetActive(false);
        }
        else if (IsType(InteractiveData.Type.InteractOnce)
            || IsType(InteractiveData.Type.InteractMulti))
        {
            interactionCount++;
            UpdateDependents();
            InteractDependents();
        }

    }

    private void UpdateDependents()
    {
        foreach (Interactive dependent in dependents)
        {
            dependent.CheckRequirements();
        }
    }

    private void CheckRequirements()
    {
        foreach (Interactive requirement in requirements)
        {
            if (!requirement.requirementsMet
                || (!requirement.IsType(InteractiveData.Type.Indirect)
                    && requirement.interactionCount == 0))
            {
                requirementsMet = false;
                return;
            }
        }

        requirementsMet = true;

        if (animator != null)
            animator.SetTrigger("RequirementsMet");

        UpdateDependents();
    }

    private void InteractDependents()
    {
        foreach (Interactive dependent in dependents)
        {
            if (dependent.requirementsMet && dependent.IsType(InteractiveData.Type.Indirect))
                dependent.InteractSelf(false);
        }
    }

    private void UseRequirement()
    {
        Interactive requirement = playerInventory.GetSelected();
        playerInventory.Remove(requirement);
        requirement.interactionCount++;

        if (requirement.animator != null)
        {
            requirement.gameObject.SetActive(true);
            requirement.animator.SetTrigger("Interact");
        }

        CheckRequirements();
    }

    private void Awake()
    {
        interactionManager = InteractionManager.Instance;
        animator = GetComponent<Animator>();
        dependents = new List<Interactive>();
        requirements = new List<Interactive>();
        isOn = interactiveData.startsOn;
        requirementsMet = interactiveData.requirements.Length == 0;
        interactionCount = 0;
        playerInventory = interactionManager.PlayerInventory;
        onInteracted = new UnityEvent();

        interactionManager.RegisterInteractive(this);
    }

    private UnityEvent onInteracted;
}
