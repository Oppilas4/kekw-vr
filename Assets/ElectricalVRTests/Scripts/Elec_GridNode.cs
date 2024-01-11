using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_GridNode : MonoBehaviour
{

    public enum direction { up,down,left,right}
    public Elec_GridNode neighbour_up;
    public Elec_GridNode neighbour_down;
    public Elec_GridNode neighbour_left;
    public Elec_GridNode neighbour_right;
    public ElecGridNodEManager ourManager;
    public bool occupied = false;
    public Collider currentlyTriggeredBy;
    public XRSocketInteractor ourXRSocketInteractor;

    public bool isOccupied()
    {
        return occupied;
    }

    public bool isThisNodeUseable(Elec_GridNode nodeToTry)
    {
        if (nodeToTry.occupied) return false;
        if ((returnNode(direction.up) == nodeToTry) || (returnNode(direction.down) == nodeToTry) || (returnNode(direction.left) == nodeToTry) || (returnNode(direction.right) == nodeToTry))
        {
            return true;
        }
        else return false;
    }

    public bool isThisNodeUseable(GameObject nodeToTry)
    {
        Elec_GridNode toTest = nodeToTry.GetComponent<Elec_GridNode>();
        if (toTest == null) return false;
        if (toTest.occupied) return false;
        if ((returnNode(direction.up) == toTest) || (returnNode(direction.down) == toTest) || (returnNode(direction.left) == toTest) || (returnNode(direction.right) == toTest))
        {
            return true;
        }
        else return false;
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
        Vector3 searchPosition = transform.localPosition;

        switch(setDirection)
        {
            case (direction.up):
                searchPosition = transform.TransformPoint(Vector3.up * distancetoNode);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode == this) continue;
                    if (Mathf.Abs(Mathf.Abs(foundNode.transform.position.x) - Mathf.Abs(transform.position.x)) > 0.1) continue;
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_up = foundNode;
                        break;
                    }
                }
                break;
            case (direction.down):
                searchPosition = transform.TransformPoint(Vector3.down * distancetoNode);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode == this) continue;
                    if (Mathf.Abs(Mathf.Abs(foundNode.transform.position.x) - Mathf.Abs(transform.position.x)) > 0.1) continue;
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_down = foundNode;
                        break;
                    }
                }
                break;
            case (direction.left):
                searchPosition = transform.TransformPoint(Vector3.left * distancetoNode);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode == this) continue;
                    if (Mathf.Abs(Mathf.Abs(foundNode.transform.position.y) - Mathf.Abs(transform.position.y)) > 0.1) continue;

                    if (foundNode.transform.position == searchPosition)
                    {

                        neighbour_left = foundNode;
                        break;
                    }
                }
                break;
            case (direction.right):
                searchPosition = transform.TransformPoint(Vector3.right * distancetoNode);
                foreach (Elec_GridNode foundNode in ourManager.Spawned_Nodes)
                {
                    if (foundNode == this) continue;
                    if (Mathf.Abs(Mathf.Abs(foundNode.transform.position.y) - Mathf.Abs(transform.position.y)) > 0.1) continue;
                    if (foundNode.transform.position == searchPosition)
                    {
                        neighbour_right = foundNode;
                        break;
                    }
                }
                break;
        }
    }


    public void Plug(bool _state)
    {
        occupied = _state;
        if (_state == false)
        {
            currentlyTriggeredBy = null;
        }
    }


    private void Start()
    {
        if (ourXRSocketInteractor == null) { 
            ourXRSocketInteractor = GetComponent<XRSocketInteractor>();
        }
        ourXRSocketInteractor.onSelectEntered.AddListener(SomethingEnters);
        ourXRSocketInteractor.onHoverExited.AddListener(SomethingExits);
    }

    public void SomethingEnters(XRBaseInteractable ref_interactable)
    {
        print("Something enters");
        print(ref_interactable.gameObject.name);
    }

    public void SomethingExits(XRBaseInteractable ref_interactable)
    {
        print("something exits");
    }

}
