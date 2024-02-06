using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Elec_PuzzleReset : MonoBehaviour
{
    ElecGridNodEManager PuzzleOrigin;
    public Elec_MegaTool Stapler;
    public bool completed;
    private void Start()
    {
        PuzzleOrigin = GetComponent<ElecGridNodEManager>();
    }
    public void RestartPuzzle()
    {
        if (!completed)
        {
            foreach (Elec_GridNode node in PuzzleOrigin.Spawned_Nodes)
            {
                node.StartCoroutine(node.DisableTempor());
                if (!node.LockVoltage)
                {
                    node.RemoveVoltageFromNeighbours();
                    node.currentVoltage = 0;
                    node.currentAvailability = false;
                }
            }
            foreach (Elec_ToolWireRenderer Spool in Stapler.WireSpools)
            {
                Spool.Voltage_Receive(0);
            }

        }
       
    }
}
