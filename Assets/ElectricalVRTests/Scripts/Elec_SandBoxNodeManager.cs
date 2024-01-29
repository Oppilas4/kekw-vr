using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_SandBoxNodeManager : MonoBehaviour
{
    public List<Elec_SandNode> Spawned_Nodes;
    public ElecGridTes ourGridTest;
    public Elec_SandNode latestPluggedIn;
    public float SearchDistanceBetweenNodes = 1;
    public List<Elec_SandNode> PluggedNodes;

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
                        Elec_SandNode foundGridNode = foundChild.GetComponent<Elec_SandNode>();
                        if (foundGridNode != null)
                        {
                            Spawned_Nodes.Add(foundGridNode);
                        }
                    }
                    yield return null;
                    foreach (Elec_SandNode foundNode in Spawned_Nodes)
                    {
                        foundNode.SetupNode(SearchDistanceBetweenNodes, this);
                    }
            }
                else {
                        foreach (Transform foundChild in ourGridTest.transform)
                        {
                            Elec_SandNode foundGridNode = foundChild.GetComponent<Elec_SandNode>();
                            if (foundGridNode != null)
                            {
                                Spawned_Nodes.Add(foundGridNode);
                            }
                        }
                        yield return null;
                        foreach (Elec_SandNode foundNode in Spawned_Nodes)
                        {
                            foundNode.SetupNode(ourGridTest.gridSpacing, this);
                        }
                    } }
        else
        {
            foreach (Transform foundChild in transform)
            {
                Elec_SandNode foundGridNode = foundChild.GetComponent<Elec_SandNode>();
                if (foundGridNode != null)
                {
                    Spawned_Nodes.Add(foundGridNode);
                }
            }
            yield return null;
            foreach (Elec_SandNode foundNode in Spawned_Nodes)
            {
                foundNode.SetupNode(SearchDistanceBetweenNodes, this);
            }
        }
    }



    //26.1 to disable previous neighbour nodes
    public void PluggingNode(Elec_SandNode toPlug)
    {
        if (latestPluggedIn) StartCoroutine(WaitOneFrameToRemoveVoltages(latestPluggedIn));
        if (PluggedNodes.Contains(toPlug) == false) PluggedNodes.Add(toPlug);
        latestPluggedIn = toPlug;
    }
    IEnumerator WaitOneFrameToRemoveVoltages(Elec_SandNode toUseAsRemoveSource)
    {
        yield return null;
        if (toUseAsRemoveSource != null) toUseAsRemoveSource.RemoveVoltageFromNeighbours();
    }

}
