using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecGridNodEManager : MonoBehaviour
{
    public List<Elec_GridNode> Spawned_Nodes;
    public ElecGridTes ourGridTest;

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
        }


    }
}
