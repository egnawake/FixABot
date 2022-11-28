using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingTilesPuzzleActivator : MonoBehaviour
{
    [SerializeField] private SlidingTilesPuzzle root;

    public void SlideRow(int index)
    {
        root.SlideRow(index);
    }

    public void SlideColumn(int index)
    {
        root.SlideColumn(index);
    }
}
