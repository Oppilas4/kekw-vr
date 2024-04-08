using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_SandNode : MonoBehaviour
{

    public enum direction { up, down, left, right }
    public Elec_SandNode neighbour_up;
    public Elec_SandNode neighbour_down;
    public Elec_SandNode neighbour_left;
    public Elec_SandNode neighbour_right;
    public Elec_SandBoxNodeManager ourManager;
    public Collider currentlyTriggeredBy;
    public XRSocketInteractor ourXRSocketInteractor;
    public Elec_Voltage ourVoltage;
    public int currentVoltage = 0;
    public int StartWithVoltage = 0;
    public bool LockVoltage = false;
    public Dictionary<GameObject,int> ReceivedVoltagesATM;
    public bool currentAvailability = false;

    public int goalVoltage = 0;

    private void Awake()
    {
        ourVoltage = new Elec_Voltage(StartWithVoltage);
        currentVoltage = StartWithVoltage;
        ReceivedVoltagesATM = new Dictionary<GameObject,int>();
        if (ourXRSocketInteractor == null) ourXRSocketInteractor = GetComponent<XRSocketInteractor>();
        currentAvailability = ourXRSocketInteractor.socketActive;
    }
    public Elec_SandNode returnNode(direction toRetrieve)
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
    public void SetupNode(float distancetoNode, Elec_SandBoxNodeManager ourManagerReference)
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
                foreach (Elec_SandNode foundNode in ourManager.Spawned_Nodes)
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
                foreach (Elec_SandNode foundNode in ourManager.Spawned_Nodes)
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
                foreach (Elec_SandNode foundNode in ourManager.Spawned_Nodes)
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
                foreach (Elec_SandNode foundNode in ourManager.Spawned_Nodes)
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

    [System.Obsolete]
    private void Start()
    {
        if (ourXRSocketInteractor == null)
        {
            ourXRSocketInteractor = GetComponent<XRSocketInteractor>();
        }
        ourXRSocketInteractor.onSelectEntered.AddListener(SomethingEnters);
        ourXRSocketInteractor.onSelectExited.AddListener(SomethingExits);
    }
    public void SomethingEnters(XRBaseInteractable ref_interactable)
    {
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            if (LockVoltage && ref_interactable.GetComponent<IVoltage>().Voltage_Send() == 0 )
            {
                ref_interactable.GetComponent<IVoltage>().Voltage_Receive(currentVoltage);
            }       
            if (ReceivedVoltagesATM.ContainsKey(ref_interactable.gameObject) == false) ReceivedVoltagesATM.Add(ref_interactable.gameObject, foundIVoltage.Voltage_Send());
            UpdateVoltage(true);
        }
        ourManager.PluggingNode(this); //26.1 to disable previous neighbour nodes
    }

    private void Update()
    {
    }

    public void SomethingExits(XRBaseInteractable ref_interactable)
    {
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            if(ReceivedVoltagesATM.ContainsKey(ref_interactable.gameObject)) ReceivedVoltagesATM.Remove(ref_interactable.gameObject);
            UpdateVoltage(true);
            RemoveNeighbourVoltage(neighbour_up.gameObject);
            RemoveNeighbourVoltage(neighbour_down.gameObject);
            RemoveNeighbourVoltage(neighbour_left.gameObject);
            RemoveNeighbourVoltage(neighbour_right.gameObject);
        }
    }

    public void TakeNeighbourVoltage(GameObject toReceiveFrom, int VoltageToReceive)
    {
            if (ReceivedVoltagesATM.ContainsKey(toReceiveFrom) == false) ReceivedVoltagesATM.Add(toReceiveFrom,VoltageToReceive);
            else
            {
                if (ReceivedVoltagesATM[toReceiveFrom] < VoltageToReceive) ReceivedVoltagesATM[toReceiveFrom] = VoltageToReceive;
            }
            UpdateVoltage(false); 
    }
    public void RemoveNeighbourVoltage(GameObject RemoveFrom)
    {
        if(RemoveFrom == null) return;
        else if(ReceivedVoltagesATM.ContainsKey(RemoveFrom))ReceivedVoltagesATM.Remove(RemoveFrom);
        UpdateVoltage(true);
    }

    public void RemoveVoltageFromNeighbours()
    {
        neighbour_up?.RemoveVoltage();
        neighbour_down?.RemoveVoltage();
        neighbour_left?.RemoveVoltage();
        neighbour_right?.RemoveVoltage();
    }

    public void RemoveVoltage()
    {
        if (ourManager.PluggedNodes.Contains(this)) return;
        else
        {
            ReceivedVoltagesATM.Clear();
            UpdateVoltage(false);
        }    
    }
    public void UpdateVoltage(bool SendToNeighbours)
    {
        int highestvoltage = 0;

        if (ReceivedVoltagesATM.Count == 0) highestvoltage = 0;
        else
        {
            foreach (KeyValuePair<GameObject, int> foundValuePair in ReceivedVoltagesATM)
            {
                if (foundValuePair.Value > highestvoltage) { highestvoltage = foundValuePair.Value; }
            }
        }
        if (LockVoltage == false)
        {
            ourVoltage.voltage = highestvoltage;

        }
        if(SendToNeighbours)
        {
            neighbour_up?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
            neighbour_down?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
            neighbour_left?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
            neighbour_right?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
        }
        currentVoltage = ourVoltage.voltage;
    }
}
