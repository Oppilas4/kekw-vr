using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_GridNode : MonoBehaviour
{

    public enum direction { up, down, left, right }
    public Elec_GridNode neighbour_up;
    public Elec_GridNode neighbour_down;
    public Elec_GridNode neighbour_left;
    public Elec_GridNode neighbour_right;
    public ElecGridNodEManager ourManager;
    public bool Available = false;
    public Collider currentlyTriggeredBy;
    public XRSocketInteractor ourXRSocketInteractor;
    public Elec_Voltage ourVoltage;
    public int StartWithVoltage = 0;
    public bool LockVoltage = false;
    public List<int> ReceivedVoltagesATM;

    private void Awake()
    {
        ourVoltage = new Elec_Voltage(StartWithVoltage);
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
        switch (setDirection)
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



    private void Start()
    {
        if (ourXRSocketInteractor == null)
        {
            ourXRSocketInteractor = GetComponent<XRSocketInteractor>();
        }
        ourXRSocketInteractor.onSelectEntered.AddListener(SomethingEnters);
        ourXRSocketInteractor.onHoverExited.AddListener(SomethingExits);
        UpdateAvailability(Available);
    }

    public void SomethingEnters(XRBaseInteractable ref_interactable)
    {
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            StartCoroutine(DelayOneFrame(foundIVoltage));
        }
    }
    IEnumerator DelayOneFrame(IVoltage foundIVoltage)
    {
        yield return null;
        ReceivedVoltagesATM.Add(foundIVoltage.Voltage_Send());
        SetNeighbourAvailability(true);
        UpdateVoltage();
    }

    public void SetNeighbourAvailability(bool locked)
    {
        neighbour_up?.UpdateAvailability(locked);
        neighbour_down?.UpdateAvailability(locked);
        neighbour_left?.UpdateAvailability(locked);
        neighbour_right?.UpdateAvailability(locked);
    }

    public void UpdateAvailability(bool state)
    {
        Available = state;
        ourXRSocketInteractor.allowSelect = Available;
    }

    public void SomethingExits(XRBaseInteractable ref_interactable)
    {
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            ReceivedVoltagesATM.Remove(foundIVoltage.Voltage_Send());
            UpdateVoltage();
        }
    }

    public void TakeNeighbourVoltage(int VoltageToReceive)
    {
        //if (Available == false) return;
        ReceivedVoltagesATM.Add(VoltageToReceive);
        UpdateVoltageFromNeighbour();
    }
    public void RemoveNeighbourVoltage(int VoltageToReceive)
    {
        ReceivedVoltagesATM.Remove(VoltageToReceive);
        UpdateVoltage();
    }
    public void UpdateVoltage()
    {
        if (!Available) return;
        int highestvoltage = 0;
        foreach (int foundVoltage in ReceivedVoltagesATM)
        {
            if (foundVoltage > highestvoltage)
            {
                highestvoltage = foundVoltage;
            }
           if(LockVoltage == false) ourVoltage.voltage = highestvoltage;
            UpdateAvailability(false);
            neighbour_up?.TakeNeighbourVoltage(ourVoltage.voltage);
            neighbour_down?.TakeNeighbourVoltage(ourVoltage.voltage);
            neighbour_left?.TakeNeighbourVoltage(ourVoltage.voltage);
            neighbour_right?.TakeNeighbourVoltage(ourVoltage.voltage);
        }
    }

    public void UpdateVoltageFromNeighbour()
    {
        int highestvoltage = 0;
        foreach (int foundVoltage in ReceivedVoltagesATM)
        {
            if (foundVoltage > highestvoltage)
            {
                highestvoltage = foundVoltage;
            }
            if (LockVoltage == false) ourVoltage.voltage = highestvoltage;
            UpdateAvailability(true);
        }
    }
}
