using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager instance;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private string pickMessage;

    private List<Interactive> interactives;

    public static InteractionManager Instance => instance;

    public string PickMessage => pickMessage;
    public PlayerInventory PlayerInventory => playerInventory;

    public void RegisterInteractive(Interactive interactive)
    {
        interactives.Add(interactive);
    }

    public void ProcessDependencies()
    {
        foreach (Interactive interactive in interactives)
        {
            foreach (InteractiveData requirementData
                in interactive.InteractiveData.requirements)
            {
                Interactive requirement = FindInteractive(requirementData);
                requirement.AddDependent(interactive);
                interactive.AddRequirement(requirement);
            }
        }
    }

    public Interactive FindInteractive(InteractiveData interactiveData)
    {
        foreach (Interactive interactive in interactives)
        {
            if (interactive.InteractiveData == interactiveData)
                return interactive;
        }
        return null;
    }

    private InteractionManager()
    {
        instance = this;
        interactives = new List<Interactive>();
    }

    private void Start()
    {
        ProcessDependencies();
        interactives = null;
    }
}
