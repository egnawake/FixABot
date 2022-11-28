using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMachine : MonoBehaviour
{
    [SerializeField] private Interactive puzzle;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        puzzle.OnInteracted.AddListener(OnPuzzleSolved);
    }

    private void OnPuzzleSolved()
    {
        Debug.Log("Leg woooo");
        animator.SetTrigger("Dispense");
    }
}
