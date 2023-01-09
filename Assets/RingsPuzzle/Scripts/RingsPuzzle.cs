using UnityEngine;

public class RingsPuzzle : MonoBehaviour
{
    private const int STEPS = 12;
    
    [SerializeField] private Interactive[] ringObjects;
    [SerializeField] private Interactive[] buttons;

    private int[] ringStates;
    private Interactive interactive;

    private void Start()
    {
        ringStates = new int[] {8, 4, 6};

        RotateRing1();
        RotateRing2();
        RotateRing3();

        ringObjects[0].OnInteracted.AddListener(RotateRing1);
        ringObjects[1].OnInteracted.AddListener(RotateRing2);
        ringObjects[2].OnInteracted.AddListener(RotateRing3);

        interactive = GetComponent<Interactive>();
    }

    private bool CheckComplete()
    {
        foreach (int state in ringStates)
        {
            if (state != 0) return false;
        }

        return true;
    }

    private void RotateRing(int index)
    {
        ringStates[index] = (ringStates[index] + 1) % STEPS;

        Vector3 rotation = ringObjects[index].transform.localEulerAngles;
        rotation.y = ringStates[index] * (360 / STEPS);
        ringObjects[index].transform.localEulerAngles = rotation;

        if (CheckComplete())
        {
            interactive.Interact();
        }
    }

    private void RotateRing1()
    {
        RotateRing(0);
    }

    private void RotateRing2()
    {
        RotateRing(1);
    }

    private void RotateRing3()
    {
        RotateRing(2);
    }
}
