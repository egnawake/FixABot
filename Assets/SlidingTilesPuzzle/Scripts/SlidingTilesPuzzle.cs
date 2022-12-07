using UnityEngine;
using TMPro;

public class SlidingTilesPuzzle : MonoBehaviour
{
    [SerializeField] private TMP_Text[] tileObjects;
    private int[] layout;
    private int[] solution = { 1, 1, 1, 2, 2, 2, 3, 3, 3 };

    private Interactive interactive;

    public void SlideRow(int index)
    {
        int first = layout[index * 3];
        int second = layout[index * 3 + 1];
        int third = layout[index * 3 + 2];

        layout[index * 3] = third;
        layout[index * 3 + 1] = first;
        layout[index * 3 + 2] = second;

        UpdateDisplay();
        CheckCompleted();
    }

    public void SlideColumn(int index)
    {
        int first = layout[index];
        int second = layout[index + 3];
        int third = layout[index + 6];

        layout[index] = third;
        layout[index + 3] = first;
        layout[index + 6] = second;

        UpdateDisplay();
        CheckCompleted();
    }

    private void CheckCompleted()
    {
        for (int i = 0; i < layout.Length; i++)
        {
            if (layout[i] != solution[i])
            {
                return;
            }
        }

        Debug.Log("Win");

        interactive.SetRequirementsMet();
        interactive.Interact();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < tileObjects.Length; i++)
        {
            tileObjects[i].text = (layout[i] + 1).ToString();
        }
    }

    private void Start()
    {
        interactive = GetComponent<Interactive>();
        layout = new int[] { 3, 1, 1, 1, 2, 2, 2, 3, 3 };
        UpdateDisplay();
    }
}
