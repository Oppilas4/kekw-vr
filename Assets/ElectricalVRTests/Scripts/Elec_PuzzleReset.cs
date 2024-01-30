using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_PuzzleReset : MonoBehaviour
{
    ElecGridNodEManager PuzzleOrigin;
   
    private void Start()
    {
        PuzzleOrigin = GetComponent<ElecGridNodEManager>();
    }
    public void RestartPuzzle()
    {
        foreach (Elec_GridNode node in PuzzleOrigin.Spawned_Nodes)
        {
            node.StartCoroutine(node.DisableTempor());
        }
    }
}
