using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class Elec_GridNode : MonoBehaviour
{

    public enum direction { up, down, left, right }
    public Elec_GridNode neighbour_up;
    public Elec_GridNode neighbour_down;
    public Elec_GridNode neighbour_left;
    public Elec_GridNode neighbour_right;
    public ElecGridNodEManager ourManager;
    public Collider currentlyTriggeredBy;
    public XRSocketInteractor ourXRSocketInteractor;
    public Elec_Voltage ourVoltage;
    public int currentVoltage = 0;
    public int StartWithVoltage = 0;
    public bool LockVoltage = false;
    public Dictionary<GameObject,int> ReceivedVoltagesATM;
    public bool currentAvailability = false;
    Renderer ChildrenMaterial;
    public int goalVoltage = 0; //Elec_FinishOutlet checks this value
    private int resetVoltage = 0;
    public bool ElectricityIsOn = false;
    public UnityEvent Electricute;
    public AudioClip Pop;
    public bool NodesResetting = false;
    ParticleSystem Confetti;
    private void Awake()
    {
        ourVoltage = new Elec_Voltage(StartWithVoltage);
        currentVoltage = StartWithVoltage;
        resetVoltage = StartWithVoltage;
        ReceivedVoltagesATM = new Dictionary<GameObject,int>();
        if (ourXRSocketInteractor == null) ourXRSocketInteractor = GetComponent<XRSocketInteractor>();
        currentAvailability = ourXRSocketInteractor.socketActive;
        ChildrenMaterial = GetComponentInChildren<MeshRenderer>();
        if (!currentAvailability && GetComponent<Elec_FinishOutlet>() == null) ChildrenMaterial.material.color = Color.gray;
        else if(GetComponent<Elec_FinishOutlet>() != null) ChildrenMaterial.material.color = Color.blue;
        Confetti = GetComponent<ParticleSystem>();
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

    [ContextMenu("DEBUG_RESET")]
    public void Reset()
    {
        gameObject.SetActive(true);
        gameObject.GetNamedChild("Plane").SetActive(true);
        ourManager.PluggedNodes.Remove(this);

        ReceivedVoltagesATM.Clear();
        currentVoltage = resetVoltage;
        ourVoltage.voltage = resetVoltage;
        if (gameObject.activeSelf) StartCoroutine(DisableTempor());
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
        if (ElectricityIsOn)
        {
            Electricute.Invoke();
        }
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            if (LockVoltage)
            {
                ref_interactable.GetComponent<IVoltage>().Voltage_Receive(currentVoltage);    
            }       
            if (ReceivedVoltagesATM.ContainsKey(ref_interactable.gameObject) == false) ReceivedVoltagesATM.Add(ref_interactable.gameObject, foundIVoltage.Voltage_Send());
            UpdateVoltage(true);
        }
    }

    private void LateUpdate()
    {
        if (currentVoltage > 0) UpdateAvailability(true);
        else UpdateAvailability(false);
    }

    public void SomethingExits(XRBaseInteractable ref_interactable)
    {
        IVoltage foundIVoltage;
        if (ref_interactable.gameObject.TryGetComponent<IVoltage>(out foundIVoltage))
        {
            if(ReceivedVoltagesATM.ContainsKey(ref_interactable.gameObject)) ReceivedVoltagesATM.Remove(ref_interactable.gameObject);
            UpdateVoltage(true);
        }
        ref_interactable.GetComponent<Rigidbody>().isKinematic = false;
            
    }
    public void UpdateAvailability(bool state)
    {
        if (state == currentAvailability) return;
        ourXRSocketInteractor.socketActive = state;
        currentAvailability = state;
        if (ChildrenMaterial != null)
        {
            if (GetComponent<Elec_FinishOutlet>()) ChildrenMaterial.material.color = Color.blue;
            else if (state) ChildrenMaterial.material.color = Color.green;
            else if (!state) ChildrenMaterial.material.color = Color.gray;
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
        if(ReceivedVoltagesATM.ContainsKey(RemoveFrom))ReceivedVoltagesATM.Remove(RemoveFrom);
        UpdateVoltage(true);
    }

    public void RemoveVoltageFromNeighbours()
    {
        if (goalVoltage != 0) return;
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
        if (NodesResetting) return;
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
        if(SendToNeighbours && goalVoltage == 0)
        {
                neighbour_up?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
                neighbour_down?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
                neighbour_left?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);
                neighbour_right?.TakeNeighbourVoltage(gameObject, ourVoltage.voltage);    
        }
        currentVoltage = ourVoltage.voltage;
    }
    public IEnumerator DisableTempor()
    {
        ourXRSocketInteractor.enabled = false;
        yield return new WaitForSeconds(1);
        ourXRSocketInteractor.enabled = true;
    }
    public void StartExlosive(int time)
    {
        if(gameObject.activeSelf)StartCoroutine(Explode(time));
    }
    IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time / ourManager.Spawned_Nodes.Count);
        ourManager.PopSound.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
        ourManager.PopSound.PlayOneShot(Pop);
        Confetti.Play();
        ourXRSocketInteractor.enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
