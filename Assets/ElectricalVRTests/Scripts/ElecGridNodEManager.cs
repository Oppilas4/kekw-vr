using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecGridNodEManager : MonoBehaviour
{
    public List<Elec_GridNode> Spawned_Nodes;
    public ElecGridTes ourGridTest;
    public Elec_GridNode latestPluggedIn;
    public float SearchDistanceBetweenNodes = 1;
    public List<Elec_GridNode> PluggedNodes;

    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }

    IEnumerator SetupRoutine()
    {
        yield return null;
        ourGridTest = GetComponent<ElecGridTes>();

        if (ourGridTest != null)
        {
            if (ourGridTest.enabled == false)
            {
                    foreach (Transform foundChild in transform)
                    {
                        Elec_GridNode foundGridNode = foundChild.GetComponent<Elec_GridNode>();
                        if (foundGridNode != null)
                        {
                            Spawned_Nodes.Add(foundGridNode);
                        }
                    }
                    yield return null;
                    foreach (Elec_GridNode foundNode in Spawned_Nodes)
                    {
                        foundNode.SetupNode(SearchDistanceBetweenNodes, this);
                    }
            }
                else {
                        foreach (Transform foundChild in ourGridTest.transform)
                        {
                            Elec_GridNode foundGridNode = foundChild.GetComponent<Elec_GridNode>();
                            if (foundGridNode != null)
                            {
                                Spawned_Nodes.Add(foundGridNode);
                            }
                        }
                        yield return null;
                        foreach (Elec_GridNode foundNode in Spawned_Nodes)
                        {
                            foundNode.SetupNode(ourGridTest.gridSpacing, this);
                        }
                    } }
        else
        {
            foreach (Transform foundChild in transform)
            {
                Elec_GridNode foundGridNode = foundChild.GetComponent<Elec_GridNode>();
                if (foundGridNode != null)
                {
                    Spawned_Nodes.Add(foundGridNode);
                }
            }
            yield return null;
            foreach (Elec_GridNode foundNode in Spawned_Nodes)
            {
                foundNode.SetupNode(SearchDistanceBetweenNodes, this);
            }
        }
    }



    //26.1 to disable previous neighbour nodes
    public void PluggingNode(Elec_GridNode toPlug)
    {
        if (latestPluggedIn) StartCoroutine(WaitOneFrameToRemoveVoltages(latestPluggedIn));
        if (PluggedNodes.Contains(toPlug) == false) PluggedNodes.Add(toPlug);
        latestPluggedIn = toPlug;
    }
    IEnumerator WaitOneFrameToRemoveVoltages(Elec_GridNode toUseAsRemoveSource)
    {
        yield return null;
        if (toUseAsRemoveSource != null) toUseAsRemoveSource.RemoveVoltageFromNeighbours();
    }

}
