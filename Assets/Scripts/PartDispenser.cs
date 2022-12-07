using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDispenser : MonoBehaviour
{
    [SerializeField] private GameObject part;
    [SerializeField] private Interactive puzzle;
    
    private void Start()
    {
        puzzle.OnInteracted.AddListener(Dispense);
    }
    
    private void Dispense()
    {
        Animator partAnimator = part.GetComponent<Animator>();
        partAnimator.SetTrigger("Dispense");
    }
}
