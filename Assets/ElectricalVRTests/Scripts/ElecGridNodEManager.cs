using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ElecGridNodEManager : MonoBehaviour
{
    public UnityEvent ResetStuff;
    public List<Elec_GridNode> Spawned_Nodes;
    public ElecGridTes ourGridTest;
    public Elec_GridNode latestPluggedIn;
    public float SearchDistanceBetweenNodes = 1;
    public List<Elec_GridNode> PluggedNodes;
    public AudioSource PopSound;
    public AudioSource Completed;
    public int LinesCompleted = 0;
    public int LinesToComplete = 0;
    public bool finished = false;
    public bool Exploding = false;
    Elec_GridNode LastNode;
    private void Start()
    {
        StartCoroutine(SetupRoutine());        
    }
    private void Update()
    {
        if (Exploding && LastNode.gameObject.activeSelf == false)
        {
            Exploding = false;
        }
        if (LinesCompleted == LinesToComplete && !finished)
        {
            finished = true;
            Completed.Play();
            Elec_MegaTool Megan = FindObjectOfType<Elec_MegaTool>();
            Megan.ResetWireList();
            Elec_PuzzleCompletitionManager.Pointz++;
            Explosives();
        }
    }
    void SetTheLastNode()
    {
        LastNode = Spawned_Nodes[Spawned_Nodes.Count - 1];
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
        SetTheLastNode();
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
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            finished = false;
            Reset();
        }
    }
    [ContextMenu("DEBUG_RESET")]

    public void Reset()
    {
        if (!Exploding)
        {
            finished = false;
            ResetStuff.Invoke();
            LinesCompleted = 0;
            foreach (Elec_GridNode ourNodes in Spawned_Nodes)
            {
                ourNodes.NodesResetting = true;
            }

            foreach (Elec_GridNode ourNodes in Spawned_Nodes)
            {
                ourNodes.Reset();
            }
            foreach (Elec_GridNode ourNodes in Spawned_Nodes)
            {
                ourNodes.NodesResetting = false;
            }
        }
                  
    }
    public void Explosives()
    {
        Exploding = true;
        for(int i = 0; i < Spawned_Nodes.Count; i++)
        {
            Spawned_Nodes[i].StartExlosive(i);
        }
    }
    public void ElectricityTurnOn()
    {
        foreach (Elec_GridNode node in Spawned_Nodes)
        {
            node.ElectricityIsOn = true;
        }
    }
    public void ElectricityTurnOff()
    {
        foreach (Elec_GridNode node in Spawned_Nodes)
        {
            node.ElectricityIsOn = false;
        }
    }
}
