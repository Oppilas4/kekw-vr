using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_GridNode : MonoBehaviour
{

    public enum direction { up,down,left,right}
    public Elec_GridNode neighbour_up;
    public Elec_GridNode neighbour_down;
    public Elec_GridNode neighbour_left;
    public Elec_GridNode neighbour_right;
    public ElecGridNodEManager ourManager;

    public bool occupied = false;


    public bool isOccupied()
    {
        return occupied;
    }

    public Elec_GridNode returnNode(direction toRetrieve)
    {
        switch (toRetrieve)
            {
            case (direction.up):
                return neighbour_up;
            case (direction.left):
                return neighbour_left;
            case (direction.right):
                return neighbour_right;
            case (direction.down):
                return neighbour_down;
            default:
                return null;
        }
    }
    public void SetupNode(float distancetoNode, ElecGridNodEManager ourManagerReference)
    {
        ourManager = ourManagerReference;
        searchForNode(distancetoNode, direction.up);
        searchForNode(distancetoNode, direction.down);
        searchForNode(distancetoNode, direction.left);
        searchForNode(distancetoNode, direction.right);
    }

    private void searchForNode(float distancetoNode, direction setDirection)
    {
        Vector3 searchPosition = transform.position;

        switch(setDirection)
        {
            case (direction.up):
                searchPosition = new Vector3(searchPosition.x, searchPosition.y + distancetoNode, searchPosition.z);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_up = foundNode;
                        break;
                    }
                }
                break;
            case (direction.down):
                searchPosition = new Vector3(searchPosition.x, searchPosition.y - distancetoNode, searchPosition.z);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_down = foundNode;
                        break;
                    }
                }
                break;
            case (direction.left):
                searchPosition = new Vector3(searchPosition.x-distancetoNode, searchPosition.y, searchPosition.z);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_left = foundNode;
                        break;
                    }
                }
                break;
            case (direction.right):
                searchPosition = new Vector3(searchPosition.x + distancetoNode, searchPosition.y, searchPosition.z);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_right = foundNode;
                        break;
                    }
                }
                break;
        }
    }
}
