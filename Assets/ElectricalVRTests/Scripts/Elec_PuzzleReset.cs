using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_PuzzleReset : MonoBehaviour
{
    ElecGridNodEManager PuzzleOrigin;
    public Elec_MegaTool Stapler;
    private void Start()
    {
        PuzzleOrigin = GetComponent<ElecGridNodEManager>();
    }
    public void RestartPuzzle()
    {
        foreach (Elec_GridNode node in PuzzleOrigin.Spawned_Nodes)
        {
            node.StartCoroutine(node.DisableTempor());
            if (!node.LockVoltage)
            {
                node.currentVoltage = 0;
            }
        }
        foreach (Elec_ToolWireRenderer Spool in Stapler.WireSpools) 
        {
            Spool.Voltage_Receive(0);
        }
    }
}
